using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Lobby)]
        public class G2L_OfflineHandler : AMActorHandler<LobbyUnitEntity, Actor_Offline>
        {
            protected override async ETTask Run(LobbyUnitEntity unit, Actor_Offline message)
            {
                unit.Dispose();
                await ETTask.CompletedTask;
            }
        }
    }
}
