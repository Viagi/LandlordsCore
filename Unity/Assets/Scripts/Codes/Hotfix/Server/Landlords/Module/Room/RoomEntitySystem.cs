using ET.Landlords;
using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class RoomEntityAwakeSystem : AwakeSystem<RoomEntity, long, long, long>
        {
            protected override void Awake(RoomEntity self, long money, long rate, long timeout)
            {
                self.BaseMoney = money;
                self.BaseRate = rate;
                self.Timeout = timeout;
                self.Seats = ListComponent<long>.Create();
                self.Seats.Add(0);
                self.Seats.Add(0);
                self.Seats.Add(0);
                self.LandlordCards = ListComponent<HandCard>.Create();
            }
        }

        [ObjectSystem]
        public class RoomEntityDestroySystem : DestroySystem<RoomEntity>
        {
            protected override void Destroy(RoomEntity self)
            {
                self.Seats.Dispose();
                self.LandlordCards.Dispose();
                self.Seats = null;
                self.LandlordCards = null;
            }
        }

        [FriendOf(typeof(RoomEntity))]
        public static class RoomEntitySystem
        {
            public static void Broadcast(this RoomEntity self, IActorMessage message)
            {
                foreach (RoomUnitEntity unit in self.Children.Values)
                {
                    unit.GetComponent<UnitGateComponent>()?.SendToClient(message);
                    unit.GetComponent<UnitRobotComponent>()?.GetComponent<UnitGateComponent>()?.SendToClient(message);
                }
            }

            public static void Start(this RoomEntity self)
            {
                //通知匹配服
                MatchHelper.Send(new Actor_RoomStart() { RoomId = self.InstanceId });
                //洗牌发牌
                using (ListComponent<HandCard> library = RoomHelper.Shuffle())
                {
                    self.Status = ERoomStatus.None;
                    self.Current = RandomGenerator.RandomNumber(0, 3);
                    self.Active = -1;
                    self.Rate = 1;
                    self.LandlordCards.Clear();
                    MultiMap<long, HandCard> handCards = new MultiMap<long, HandCard>();
                    int target = self.Current;
                    for (int i = library.Count - 1; i > 2; i--)
                    {
                        handCards.Add(self.Seats[target % 3], library[i]);
                        target++;
                    }
                    foreach (var cards in handCards)
                    {
                        RoomUnitEntity unit = self.GetChild<RoomUnitEntity>(cards.Key);
                        unit.Reset();
                        unit.AddCards(cards.Value);
                    }
                    self.SetLandlordCards(library[0], library[1], library[2]);
                }

                //发送手牌
                List<byte[]> entitys = new List<byte[]>();
                foreach (RoomUnitEntity unit in self.Children.Values)
                {
                    entitys.Add(MongoHelper.Serialize(unit));
                }
                self.Broadcast(new Actor_GameStart() { Entitys = entitys });
                self.Next();
            }

            public static void Next(this RoomEntity self)
            {
                RoomUnitEntity activeUnit = self.GetActive();
                RoomUnitEntity beforeUnit = self.GetCurrent();
                RoomUnitEntity nextUnit = self.Get(self.Current + 1);

                //移除超时组件
                beforeUnit.RemoveComponent<LandlordTimeoutComponent>();
                switch (self.Status)
                {
                    case ERoomStatus.None:
                        {
                            self.Status++;
                            break;
                        }
                    case ERoomStatus.CallLandlord:
                        {
                            if (activeUnit == null && nextUnit.Status != ELandlordStatus.None)
                            {
                                //没人叫地主，重新发牌
                                self.Start();
                                return;
                            }
                            else if (activeUnit != null && nextUnit.Status != ELandlordStatus.None)
                            {
                                //只有一个叫地主
                                self.Status = ERoomStatus.PlayCard;
                                self.SetLandlord(activeUnit);
                            }
                            else if (activeUnit != null && nextUnit.Status == ELandlordStatus.None)
                            {
                                //有人叫地主，开始抢地主
                                self.Status++;
                                self.Current++;
                            }
                            else
                            {
                                self.Current++;
                            }
                            break;
                        }
                    case ERoomStatus.RobLandlord:
                        {
                            if (beforeUnit.Status == ELandlordStatus.RobAgain)
                            {
                                //抢地主成功
                                self.Status++;
                                self.SetLandlord(activeUnit);
                            }
                            else if (nextUnit == activeUnit)
                            {
                                //没人抢地主
                                self.Status++;
                                self.Current++;
                                self.SetLandlord(activeUnit);
                            }
                            else
                            {
                                //继续抢地主
                                self.Current++;
                            }
                            break;
                        }
                    case ERoomStatus.PlayCard:
                        {
                            nextUnit.Status = ELandlordStatus.None;
                            nextUnit.PlayCards.Clear();
                            if (activeUnit.HandCards.Count == 0)
                            {
                                //手牌出完胜利
                                self.End(activeUnit).Coroutine();
                                return;
                            }
                            self.Current++;
                            break;
                        }
                }

                //广播切换玩家
                self.Broadcast(new Actor_PlaySwitch()
                {
                    Status = self.Status,
                    Before = beforeUnit.Index,
                    Current = self.Current,
                    Active = self.Active,
                    Rate = self.Rate,
                    Time = TimeHelper.ServerFrameTime()
                });
                //添加超时组件
                self.GetCurrent().AddComponent<LandlordTimeoutComponent, long>(self.Timeout);
            }

            private static void SetLandlord(this RoomEntity self, RoomUnitEntity landlord)
            {
                foreach (RoomUnitEntity unit in self.Children.Values)
                {
                    if (unit == landlord)
                    {
                        unit.Identity = ELandlordIdentity.Landlord;
                        unit.AddCards(self.LandlordCards);
                    }
                    else
                    {
                        unit.Identity = ELandlordIdentity.Peasantry;
                    }
                    unit.Status = ELandlordStatus.None;
                }
                self.Broadcast(new Actor_SetLandlord() { Cards = self.LandlordCards, Index = self.Current });
            }

            private static async ETTask End(this RoomEntity self, RoomUnitEntity winner)
            {
                //通知匹配服
                MatchHelper.Send(new Actor_RoomEnd() { RoomId = self.InstanceId });
                //游戏结算
                List<long> results = new List<long>();
                List<byte[]> entitys = new List<byte[]>();
                using (ListComponent<ETTask<AccountComponent>> queryTasks = ListComponent<ETTask<AccountComponent>>.Create())
                using (ListComponent<ETTask> saveTasks = ListComponent<ETTask>.Create())
                {
                    //获取最新数据
                    for (int i = 0; i < self.Seats.Count; i++)
                    {
                        ETTask<AccountComponent> task = UserHelper.AccessUserComponent<AccountComponent>(self.Seats[i], true);
                        queryTasks.Add(task);
                    }
                    AccountComponent[] accountComponents = new AccountComponent[self.Seats.Count];
                    await ETTaskHelper.WaitAll(queryTasks, null, accountComponents);

                    long money = self.GetMoney();
                    long winMoney = 0;
                    for (int i = 0; i < self.Seats.Count; i++)
                    {
                        RoomUnitEntity unit = self.GetChild<RoomUnitEntity>(self.Seats[i]);
                        //缓存结算前数据
                        results.Add(unit.GetComponent<AccountComponent>().Money);

                        AccountComponent accountComponent = accountComponents[i];
                        unit.RemoveComponent<AccountComponent>();
                        unit.AddComponent(accountComponent);

                        if (unit.Identity != winner.Identity)
                        {
                            //胜者只能赢取败者的钱
                            if (accountComponent.Money > money)
                            {
                                accountComponent.Money -= money;
                                winMoney += money;
                            }
                            else
                            {
                                winMoney += accountComponent.Money;
                                accountComponent.Money = 0;
                            }
                            accountComponent.Loses += 1;
                            //保存结算数据
                            saveTasks.Add(UserHelper.ChangeUserComponent(unit.Id, accountComponent));
                        }
                    }
                    for (int i = 0; i < self.Seats.Count; i++)
                    {
                        RoomUnitEntity unit = self.GetChild<RoomUnitEntity>(self.Seats[i]);
                        AccountComponent accountComponent = unit.GetComponent<AccountComponent>();

                        if (unit.Identity == winner.Identity)
                        {
                            //胜者只能赢取败者的钱
                            if (unit.Identity == ELandlordIdentity.Landlord)
                            {
                                accountComponent.Money += winMoney;
                            }
                            else
                            {
                                accountComponent.Money += winMoney / 2;
                            }
                            accountComponent.Wins += 1;
                            //保存结算数据
                            saveTasks.Add(UserHelper.ChangeUserComponent(unit.Id, accountComponent));
                        }
                    }
                    await ETTaskHelper.WaitAll(saveTasks);
                }

                //广播游戏结束
                for (int i = 0; i < self.Seats.Count; i++)
                {
                    RoomUnitEntity unit = self.GetChild<RoomUnitEntity>(self.Seats[i]);
                    AccountComponent accountComponent = unit.GetComponent<AccountComponent>();
                    results[i] = accountComponent.Money - results[i];
                    entitys.Add(MongoHelper.Serialize(unit));
                }
                foreach (RoomUnitEntity unit in self.Children.Values)
                {
                    byte[] component = MongoHelper.Serialize(unit.GetComponent<AccountComponent>());
                    unit.GetComponent<UnitGateComponent>().SendToClient(new Actor_GameEnd() { Entitys = entitys, Results = results, Component = component });
                }
                //重置状态
                self.Status = ERoomStatus.None;
                self.LandlordCards.Clear();
                self.Current = -1;
                self.Active = -1;
                self.Rate = 1;
                foreach (RoomUnitEntity unit in self.Children.Values)
                {
                    unit.Reset();
                }
            }
        }
    }
}
