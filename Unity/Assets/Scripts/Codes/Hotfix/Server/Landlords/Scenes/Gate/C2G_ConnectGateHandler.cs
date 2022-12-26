using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Gate)]
        public class C2G_ConnectGateHandler : AMRpcHandler<C2G_ConnectGate, G2C_ConnectGate>
        {
            protected override async ETTask Run(Session session, C2G_ConnectGate request, G2C_ConnectGate response, Action reply)
            {
                Scene scene = session.DomainScene();
                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                Player player = playerComponent.AddChild<Player, string>(request.UnitId.ToString());
                UnitGateComponent unitGateComponent = player.AddComponent<UnitGateComponent, long, long>(player.Id, session.InstanceId);
                Actor_ConnectRobotResponse connectRobotResponse = (Actor_ConnectRobotResponse)await MessageHelper.CallActor(request.ActorId, new Actor_ConnectRobotRequest() { UnitId = request.UnitId, Entity = MongoHelper.Serialize(unitGateComponent) });

                long userId = connectRobotResponse.UserId;
                OnlineUnitEntity onlineUnit = null;
                if (userId > 0)
                {
                    onlineUnit = await CacheHelper.GetOnlineUnit(userId);
                    if (onlineUnit != null)
                    {
                        LandlordsComponent landlordsComponent = player.AddComponent<LandlordsComponent, long, bool>(userId, true);
                        landlordsComponent.LobbyId = onlineUnit.LobbyId;
                        landlordsComponent.FriendId = onlineUnit.FriendId;
                        landlordsComponent.MatchId = onlineUnit.MatchId;
                        landlordsComponent.RoomId = onlineUnit.RoomId;
                        playerComponent.Add(player);
                        player.AddComponent<MailBoxComponent>();
                        session.AddComponent<SessionPlayerComponent, long, long, long>(userId, scene.InstanceId, 0).PlayerId = player.Id;
                        session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                        session.RemoveComponent<SessionAcceptTimeoutComponent>();
                    }
                }

                if (onlineUnit == null)
                {
                    player.Dispose();
                    response.Error = ErrorCode.ERR_RobotConnectFailed;
                }

                reply();
            }
        }
    }
}
