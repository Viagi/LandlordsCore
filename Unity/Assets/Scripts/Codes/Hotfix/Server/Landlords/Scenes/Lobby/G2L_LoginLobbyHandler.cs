using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Lobby)]
        public class G2L_LoginLobbyHandler : AMActorRpcHandler<Scene, G2L_LoginLobby, L2G_LoginLobby>
        {
            protected override async ETTask Run(Scene scene, G2L_LoginLobby request, L2G_LoginLobby response, Action reply)
            {
                LobbyUnitEntity unit = scene.AddChildWithId<LobbyUnitEntity>(request.UserId);
                unit.AddComponent<MailBoxComponent>();
                unit.AddComponent(MongoHelper.Deserialize<UnitGateComponent>(request.Entity));
                unit.AddComponent<MirrorUnitComponent, long>(request.UserId);

                response.UnitId = unit.InstanceId;
                reply();

                await ETTask.CompletedTask;
            }
        }
    }
}
