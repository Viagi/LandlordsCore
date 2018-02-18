using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_GamerReady_NttHandler : AMActorHandler<Gamer, Actor_GamerReady_Ntt>
    {
        protected override async Task Run(Gamer gamer, Actor_GamerReady_Ntt message)
        {
            gamer.IsReady = true;

            Room room = Game.Scene.GetComponent<RoomComponent>().Get(gamer.RoomID);
            Gamer[] gamers = room.GetAll();

            //转发玩家准备消息
            message.UserID = gamer.UserID;
            room.Broadcast(message);
            Log.Info($"玩家{gamer.UserID}准备");

            //房间内有3名玩家且全部准备则开始游戏
            if (room.Count == 3 && gamers.Where(g => g.IsReady).Count() == 3)
            {
                //同步匹配服务器开始游戏
                room.State = RoomState.Game;
                MapHelper.SendMessage(new MP2MH_SyncRoomState_Ntt() { RoomID = room.Id, State = room.State });

                //初始玩家开始状态
                foreach (var _gamer in gamers)
                {
                    if (_gamer.GetComponent<HandCardsComponent>() == null)
                    {
                        _gamer.AddComponent<HandCardsComponent>();
                    }
                    _gamer.IsReady = false;
                }

                GameControllerComponent gameController = room.GetComponent<GameControllerComponent>();
                //洗牌发牌
                gameController.DealCards();

                Dictionary<long, int> gamerCardsNum = new Dictionary<long, int>();
                Array.ForEach(gamers, (g) =>
                {
                    HandCardsComponent handCards = g.GetComponent<HandCardsComponent>();
                    //重置玩家身份
                    handCards.AccessIdentity = Identity.None;
                    //记录玩家手牌数
                    gamerCardsNum.Add(g.UserID, handCards.CardsCount);
                });

                //发送玩家手牌和其他玩家手牌数
                foreach (var _gamer in gamers)
                {
                    ActorProxy actorProxy = _gamer.GetComponent<UnitGateComponent>().GetActorProxy();
                    actorProxy.Send(new Actor_GameStart_Ntt()
                    {
                        GamerCards = _gamer.GetComponent<HandCardsComponent>().GetAll(),
                        GamerCardsNum = gamerCardsNum
                    });
                }

                //随机先手玩家
                gameController.RandomFirstAuthority();

                Log.Info($"房间{room.Id}开始游戏");
            }

            await Task.CompletedTask;
        }
    }
}
