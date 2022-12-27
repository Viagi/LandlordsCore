using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class G2R_ConnectRobotRequestHandler : AMActorRpcHandler<Scene, Actor_ConnectRobotRequest, Actor_ConnectRobotResponse>
        {
            protected override async ETTask Run(Scene scene, Actor_ConnectRobotRequest request, Actor_ConnectRobotResponse response, Action reply)
            {
                RoomUnitEntity unit = Root.Instance.Get(request.UnitId) as RoomUnitEntity;
                UnitRobotComponent robotComponent = unit?.GetComponent<UnitRobotComponent>();
                if (robotComponent != null)
                {
                    robotComponent.AddComponent(MongoHelper.Deserialize<UnitGateComponent>(request.Entity));
                    response.UserId = unit.Id;
                }

                reply();
                await ETTask.CompletedTask;
            }
        }
    }
}
