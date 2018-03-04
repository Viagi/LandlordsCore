using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_PlayerEnterRoom_ReqHandler : AMActorRpcHandler<Room, Actor_PlayerEnterRoom_Req, Actor_PlayerEnterRoom_Ack>
    {
        protected override async Task Run(Room room, Actor_PlayerEnterRoom_Req message, Action<Actor_PlayerEnterRoom_Ack> reply)
        {
            Actor_PlayerEnterRoom_Ack response = new Actor_PlayerEnterRoom_Ack();
            try
            {
                Gamer gamer = room.Get(message.UserID);
                if (gamer == null)
                {
                    //创建房间玩家对象
                    gamer = GamerFactory.Create(message.PlayerID, message.UserID);
                    await gamer.AddComponent<ActorComponent>().AddLocation();
                    gamer.AddComponent<UnitGateComponent, long>(message.SessionID);

                    //加入到房间
                    room.Add(gamer);

                    Actor_GamerEnterRoom_Ntt broadcastMessage = new Actor_GamerEnterRoom_Ntt();
                    foreach (Gamer _gamer in room.GetAll())
                    {
                        if (_gamer == null)
                        {
                            //添加空位
                            broadcastMessage.Gamers.Add(null);
                            continue;
                        }

                        //添加玩家信息
                        GamerInfo info = new GamerInfo() { UserID = _gamer.UserID, IsReady = _gamer.IsReady };
                        broadcastMessage.Gamers.Add(info);
                    }

                    //广播房间内玩家消息
                    room.Broadcast(broadcastMessage);

                    Log.Info($"玩家{message.UserID}进入房间");
                }
                else
                {
                    //玩家重连
                    gamer.isOffline = false;
                    gamer.PlayerID = message.PlayerID;
                    gamer.GetComponent<UnitGateComponent>().GateSessionId = message.SessionID;

                    //玩家重连移除托管组件
                    gamer.RemoveComponent<TrusteeshipComponent>();

                    Actor_GamerEnterRoom_Ntt broadcastMessage = new Actor_GamerEnterRoom_Ntt();
                    foreach (Gamer _gamer in room.GetAll())
                    {
                        if (_gamer == null)
                        {
                            //添加空位
                            broadcastMessage.Gamers.Add(null);
                            continue;
                        }

                        //添加玩家信息
                        GamerInfo info = new GamerInfo() { UserID = _gamer.UserID, IsReady = _gamer.IsReady };
                        broadcastMessage.Gamers.Add(info);
                    }

                    //发送房间玩家信息
                    ActorProxy actorProxy = gamer.GetComponent<UnitGateComponent>().GetActorProxy();
                    actorProxy.Send(broadcastMessage);

                    Dictionary<long, Identity> gamersIdentity = new Dictionary<long, Identity>();
                    Dictionary<long, int> gamersCardsNum = new Dictionary<long, int>();
                    GameControllerComponent gameController = room.GetComponent<GameControllerComponent>();
                    OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
                    DeskCardsCacheComponent deskCardsCache = room.GetComponent<DeskCardsCacheComponent>();

                    foreach (Gamer _gamer in room.GetAll())
                    {
                        HandCardsComponent handCards = _gamer.GetComponent<HandCardsComponent>();
                        gamersCardsNum.Add(_gamer.UserID, handCards.CardsCount);
                        gamersIdentity.Add(_gamer.UserID, handCards.AccessIdentity);
                    }

                    //发送游戏开始消息
                    Actor_GameStart_Ntt gameStartNotice = new Actor_GameStart_Ntt()
                    {
                        GamerCards = gamer.GetComponent<HandCardsComponent>().GetAll(),
                        GamerCardsNum = gamersCardsNum
                    };
                    actorProxy.Send(gameStartNotice);

                    Card[] lordCards = null;
                    if (gamer.GetComponent<HandCardsComponent>().AccessIdentity == Identity.None)
                    {
                        //广播先手玩家
                        actorProxy.Send(new Actor_AuthorityGrabLandlord_Ntt() { UserID = orderController.CurrentAuthority });
                    }
                    else
                    {
                        if (gamer.UserID == orderController.CurrentAuthority)
                        {
                            //发送可以出牌消息
                            bool isFirst = gamer.UserID == orderController.Biggest;
                            actorProxy.Send(new Actor_AuthorityPlayCard_Ntt() { UserID = orderController.CurrentAuthority, IsFirst = isFirst });
                        }
                        lordCards = deskCardsCache.LordCards.ToArray();
                    }
                    //发送重连数据
                    Actor_GamerReconnect_Ntt reconnectNotice = new Actor_GamerReconnect_Ntt()
                    {
                        Multiples = room.GetComponent<GameControllerComponent>().Multiples,
                        GamersIdentity = gamersIdentity,
                        DeskCards = new KeyValuePair<long, Card[]>(orderController.Biggest, deskCardsCache.library.ToArray()),
                        LordCards = lordCards,
                        GamerGrabLandlordState = orderController.GamerLandlordState
                    };
                    actorProxy.Send(reconnectNotice);

                    Log.Info($"玩家{message.UserID}重连");
                }

                response.GamerID = gamer.Id;

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
