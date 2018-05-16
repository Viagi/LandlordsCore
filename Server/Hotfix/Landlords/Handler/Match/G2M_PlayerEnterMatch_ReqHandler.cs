using System;
using ETModel;
using System.Threading.Tasks;

namespace ETHotfix
{
    [MessageHandler(AppType.Match)]
    public class G2M_PlayerEnterMatch_ReqHandler : AMRpcHandler<G2M_PlayerEnterMatch_Req, M2G_PlayerEnterMatch_Ack>
    {
        protected override async void Run(Session session, G2M_PlayerEnterMatch_Req message, Action<M2G_PlayerEnterMatch_Ack> reply)
        {
            M2G_PlayerEnterMatch_Ack response = new M2G_PlayerEnterMatch_Ack();
            try
            {
                MatchComponent matchComponent = Game.Scene.GetComponent<MatchComponent>();
                ActorMessageSenderComponent actorProxyComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();

                if (matchComponent.Playing.ContainsKey(message.UserID))
                {
                    MatchRoomComponent matchRoomComponent = Game.Scene.GetComponent<MatchRoomComponent>();
                    long roomId = matchComponent.Playing[message.UserID];
                    Room room = matchRoomComponent.Get(roomId);
                    Gamer gamer = room.Get(message.UserID);

                    //重置GateActorID
                    gamer.PlayerID = message.PlayerID;

                    //重连房间
                    ActorMessageSender actorProxy = actorProxyComponent.Get(roomId);
                    await actorProxy.Call(new Actor_PlayerEnterRoom_Req()
                    {
                        PlayerID = message.PlayerID,
                        UserID = message.UserID,
                        SessionID = message.SessionID
                    });

                    //向玩家发送匹配成功消息
                    ActorMessageSender gamerActorProxy = actorProxyComponent.Get(gamer.PlayerID);
                    gamerActorProxy.Send(new Actor_MatchSucess_Ntt() { GamerID = gamer.Id });
                }
                else
                {
                    //创建匹配玩家
                    Matcher matcher = MatcherFactory.Create(message.PlayerID, message.UserID, message.SessionID);
                }

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
