using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class UnitRobotComponentDestroySystem : DestroySystem<UnitRobotComponent>
        {
            protected override void Destroy(UnitRobotComponent self)
            {
                self.Robot.Dispose();
            }
        }

        [FriendOf(typeof(UnitRobotComponent))]
        public static class UnitRobotComponentSystem
        {
            public static async ETTask Init(this UnitRobotComponent self)
            {
                RoomUnitEntity unit = self.GetParent<RoomUnitEntity>();
                StartSceneConfig robotSceneConfig = AddressHelper.GetRobot();
                Scene robotScene = ServerSceneManagerComponent.Instance.Get(robotSceneConfig.Id);
                RobotManagerComponent robotManagerComponent = robotScene.GetComponent<RobotManagerComponent>();
                try
                {
                    self.Robot = await robotManagerComponent.NewUnitRobot(unit);
                }
                catch
                {
                    self.Dispose();
                }
            }
        }
    }
}
