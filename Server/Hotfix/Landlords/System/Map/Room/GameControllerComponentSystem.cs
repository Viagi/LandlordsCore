using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ObjectSystem]
    public class GameControllerComponentAwakeSystem : AwakeSystem<GameControllerComponent,RoomConfig>
    {
        public override void Awake(GameControllerComponent self, RoomConfig config)
        {
            self.Awake(config);
        }
    }

    public static class GameControllerComponentSystem
    {
        public static void Awake(this GameControllerComponent self, RoomConfig config)
        {
            self.Config = config;
            self.BasePointPerMatch = config.BasePointPerMatch;
            self.Multiples = config.Multiples;
            self.MinThreshold = config.MinThreshold;
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        /// <param name="self"></param>
        public static void DealCards(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();

            //牌库洗牌
            room.GetComponent<DeckComponent>().Shuffle();

            //玩家轮流发牌
            Gamer[] gamers = room.GetAll();
            int index = 0;
            for (int i = 0; i < 51; i++)
            {
                if (index == 3)
                {
                    index = 0;
                }

                self.DealTo(gamers[index].UserID);

                index++;
            }

            //发地主牌
            for (int i = 0; i < 3; i++)
            {
                self.DealTo(room.Id);
            }

            self.Multiples = self.Config.Multiples;
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <param name="id"></param>
        public static void DealTo(this GameControllerComponent self, long id)
        {
            Room room = self.GetParent<Room>();
            Card card = room.GetComponent<DeckComponent>().Deal();
            if (id == room.Id)
            {
                DeskCardsCacheComponent deskCardsCache = room.GetComponent<DeskCardsCacheComponent>();
                deskCardsCache.AddCard(card);
                deskCardsCache.LordCards.Add(card);
            }
            else
            {
                foreach (var gamer in room.GetAll())
                {
                    if (id == gamer.UserID)
                    {
                        gamer.GetComponent<HandCardsComponent>().AddCard(card);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 发地主牌
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void CardsOnTable(this GameControllerComponent self, long id)
        {
            Room room = self.GetParent<Room>();
            DeskCardsCacheComponent deskCardsCache = room.GetComponent<DeskCardsCacheComponent>();
            OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
            HandCardsComponent handCards = room.Get(id).GetComponent<HandCardsComponent>();

            orderController.Start(id);

            for (int i = 0; i < 3; i++)
            {
                Card card = deskCardsCache.Deal();
                handCards.AddCard(card);
            }

            //更新玩家身份
            foreach (var gamer in room.GetAll())
            {
                Identity gamerIdentity = gamer.UserID == id ? Identity.Landlord : Identity.Farmer;
                self.UpdateInIdentity(gamer, gamerIdentity);
            }

            //广播地主消息
            room.Broadcast(new Actor_SetLandlord_Ntt() { UserID = id, LordCards = deskCardsCache.LordCards.ToArray() });

            //广播地主先手出牌消息
            room.Broadcast(new Actor_AuthorityPlayCard_Ntt() { UserID = id, IsFirst = true });
        }

        /// <summary>
        /// 更新身份
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <param name="identity"></param>
        public static void UpdateInIdentity(this GameControllerComponent self, Gamer gamer, Identity identity)
        {
            gamer.GetComponent<HandCardsComponent>().AccessIdentity = identity;
        }

        /// <summary>
        /// 场上的所有牌回到牌库中
        /// </summary>
        /// <param name="self"></param>
        public static void BackToDeck(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            DeckComponent deckComponent = room.GetComponent<DeckComponent>();
            DeskCardsCacheComponent deskCardsCache = room.GetComponent<DeskCardsCacheComponent>();

            //回收牌桌卡牌
            deskCardsCache.Clear();
            deskCardsCache.LordCards.Clear();

            //回收玩家手牌
            foreach (var gamer in room.GetAll())
            {
                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                while (handCards.CardsCount > 0)
                {
                    Card card = handCards.library[handCards.CardsCount - 1];
                    handCards.PopCard(card);
                    deckComponent.AddCard(card);
                }
            }
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        /// <param name="self"></param>
        public static async void GameOver(this GameControllerComponent self, Dictionary<long, long> gamersSorce)
        {
            Room room = self.GetParent<Room>();

            //清理所有卡牌
            self.BackToDeck();
            room.GetComponent<DeskCardsCacheComponent>().Clear();

            //同步匹配服务器结束游戏
            room.State = RoomState.Ready;
            MapHelper.SendMessage(new MP2MH_SyncRoomState_Ntt() { RoomID = room.Id, State = room.State });

            Gamer[] gamers = room.GetAll();
            for (int i = 0; i < gamers.Length; i++)
            {
                long gamerMoney = await self.StatisticalIntegral(gamers[i], gamersSorce[gamers[i].UserID]);
                bool isKickOut = gamers[i].isOffline;

                //玩家余额低于最低门槛
                if (gamerMoney < self.MinThreshold)
                {
                    ActorProxy actorProxy = gamers[i].GetComponent<UnitGateComponent>().GetActorProxy();
                    actorProxy.Send(new Actor_GamerMoneyLess_Ntt() { UserID = gamers[i].UserID });
                    isKickOut = true;
                }

                //踢出玩家
                if (isKickOut)
                {
                    ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(gamers[i].Id);
                    await actorProxy.Call(new Actor_PlayerExitRoom_Req());
                }
            }
        }

        /// <summary>
        /// 计算玩家积分
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gamer"></param>
        /// <param name="winnerIdentity"></param>
        /// <returns></returns>
        public static long GetScore(this GameControllerComponent self, Gamer gamer, Identity winnerIdentity)
        {
            HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();

            //积分计算公式：全场底分 * 全场倍率 * 身份倍率
            long integration = self.BasePointPerMatch * self.Multiples * (int)handCards.AccessIdentity;
            //当玩家不是胜者，结算积分为负
            if (handCards.AccessIdentity != winnerIdentity)
                integration = -integration;
            return integration;
        }

        /// <summary>
        /// 结算用户余额
        /// </summary>
        public static async Task<long> StatisticalIntegral(this GameControllerComponent self, Gamer gamer, long sorce)
        {
            DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

            //结算用户余额
            UserInfo userInfo = await dbProxy.Query<UserInfo>(gamer.UserID, false);
            userInfo.Money = userInfo.Money + sorce < 0 ? 0 : userInfo.Money + sorce;

            //更新用户信息
            await dbProxy.Save(userInfo);

            return userInfo.Money;
        }

        /// <summary>
        /// 随机先手玩家
        /// </summary>
        public static void RandomFirstAuthority(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
            Gamer[] gamers = room.GetAll();

            int index = RandomHelper.RandomNumber(0, gamers.Length);
            long firstAuthority = gamers[index].UserID;
            orderController.Init(firstAuthority);

            //广播先手抢地主玩家
            room.Broadcast(new Actor_AuthorityGrabLandlord_Ntt() { UserID = firstAuthority });
        }
    }
}
