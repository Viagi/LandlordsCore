using ET.Landlords;
using System;


namespace ET.Server
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Gate)]
        public class C2G_LoginGateHandler : AMRpcHandler<ET.Landlords.C2G_LoginGate, ET.Landlords.G2C_LoginGate>
        {
            protected override async ETTask Run(Session session, ET.Landlords.C2G_LoginGate request, ET.Landlords.G2C_LoginGate response, Action reply)
            {
                Scene scene = session.DomainScene();
                //验证令牌
                string account = scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
                if (account == null)
                {
                    replyError(response, reply);
                    return;
                }

                //验证登录
                long userId = long.Parse(account);
                OnlineUnitEntity onlineUnit = await CacheHelper.GetOnlineUnit(userId);
                if (onlineUnit != null && !onlineUnit.Check(scene.InstanceId, request.Key))
                {
                    replyError(response, reply);
                    return;
                }

                session.RemoveComponent<SessionAcceptTimeoutComponent>();

                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                Player player = playerComponent.AddChild<Player, string>(account);
                LandlordsComponent landlordsComponent = player.AddComponent<LandlordsComponent, long, bool>(userId, false);
                landlordsComponent.LobbyId = onlineUnit.LobbyId;
                landlordsComponent.FriendId = onlineUnit.FriendId;
                landlordsComponent.MatchId = onlineUnit.MatchId;
                landlordsComponent.RoomId = onlineUnit.RoomId;
                player.AddComponent<UnitGateComponent, long, long>(player.InstanceId, session.InstanceId);
                player.AddComponent<MailBoxComponent>();
                player.AddComponent<MirrorUnitComponent, long>(userId);
                playerComponent.Add(player);
                session.AddComponent<SessionPlayerComponent, long, long, long>(userId, scene.InstanceId, request.Key).PlayerId = player.Id;
                session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);

                byte[] unitGateComponent = MongoHelper.Serialize(player.GetComponent<UnitGateComponent>());

                if (landlordsComponent.LobbyId == 0)
                {
                    //分配大厅服
                    StartSceneConfig config = AddressHelper.GetLobby(session.DomainZone());
                    //登录大厅
                    L2G_LoginLobby loginResponse = (L2G_LoginLobby)await ActorMessageSenderComponent.Instance.Call(config.InstanceId, new G2L_LoginLobby() { UserId = landlordsComponent.UserId, Entity = unitGateComponent });

                    //缓存大厅镜像
                    landlordsComponent.LobbyId = loginResponse.UnitId;
                }

                if (landlordsComponent.FriendId == 0)
                {
                    //分配好友服
                    StartSceneConfig config = AddressHelper.GetFriend(session.DomainZone());
                    //登录好友
                    F2G_LoginFriend loginResponse = (F2G_LoginFriend)await ActorMessageSenderComponent.Instance.Call(config.InstanceId, new G2F_LoginFriend() { UserId = landlordsComponent.UserId, Entity = unitGateComponent });

                    //缓存好友镜像
                    landlordsComponent.FriendId = loginResponse.UnitId;
                }

                reply();
            }

            private void replyError(ET.Landlords.G2C_LoginGate response, Action reply)
            {
                response.Error = ErrorCore.ERR_ConnectGateKeyError;
                response.Message = "Gate key验证失败!";
                reply();
            }
        }
    }
}