using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Match)]
        public class Actor_RoomEndHandler : AMActorHandler<Scene, Actor_RoomEnd>
        {
            protected override async ETTask Run(Scene scene, Actor_RoomEnd message)
            {
                MatchUnitManagerComponent managerComponent = scene.GetComponent<MatchUnitManagerComponent>();
                managerComponent.RoomEnd(message.RoomId);

                await ETTask.CompletedTask;
            }
        }
    }
}
