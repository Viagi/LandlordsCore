using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Friend)]
        public class G2F_OfflineHandler : AMActorHandler<FriendUnitEntity, Actor_Offline>
        {
            protected override async ETTask Run(FriendUnitEntity unit, Actor_Offline message)
            {
                unit.Dispose();
                await ETTask.CompletedTask;
            }
        }
    }
}
