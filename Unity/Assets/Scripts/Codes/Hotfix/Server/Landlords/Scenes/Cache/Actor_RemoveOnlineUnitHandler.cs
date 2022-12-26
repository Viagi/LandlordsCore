using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Cache)]
        public class Actor_RemoveOnlineUnitHandler : AMActorHandler<Scene, Actor_RemoveOnlineUnit>
        {
            protected override async ETTask Run(Scene scene, Actor_RemoveOnlineUnit message)
            {
                OnlineUnitEntity unit = scene.GetChild<OnlineUnitEntity>(message.UserId);
                if (unit != null && unit.Check(message.GateId, message.GateKey))
                {
                    unit.GateId = 0;
                    unit.GateKey = 0;
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
