using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Match)]
        public class Actor_RoomStartHandler : AMActorHandler<Scene, Actor_RoomStart>
        {
            protected override async ETTask Run(Scene scene, Actor_RoomStart message)
            {
                MatchUnitManagerComponent managerComponent = scene.GetComponent<MatchUnitManagerComponent>();
                managerComponent.RoomStart(message.RoomId);

                await ETTask.CompletedTask;
            }
        }
    }
}
