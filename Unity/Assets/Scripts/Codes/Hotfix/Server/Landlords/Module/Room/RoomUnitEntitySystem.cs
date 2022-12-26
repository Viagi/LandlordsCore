using ET.Landlords;
using System.Numerics;

namespace ET.Server
{
    namespace Landlords
    {
        [FriendOf(typeof(RoomUnitEntity))]
        public static class RoomUnitEntitySystem
        {
            public static async ETTask Offline(this RoomUnitEntity self)
            {
                self.IsOffline = true;
                self.RemoveComponent<UnitGateComponent>();
                await self.Trust(true);
            }

            public static async ETTask Online(this RoomUnitEntity self, UnitGateComponent unitGateComponent)
            {
                self.RemoveComponent<UnitGateComponent>();
                self.AddComponent(unitGateComponent);
                self.IsOffline = false;
                await self.Trust(false);
            }

            public static async ETTask Trust(this RoomUnitEntity self, bool isTrust)
            {
                UnitRobotComponent robotComponent = self.GetComponent<UnitRobotComponent>();
                self.IsTrust = isTrust;
                if (isTrust && robotComponent == null)
                {
                    robotComponent = self.AddComponent<UnitRobotComponent>();
                    await robotComponent.Init();
                    robotComponent.Robot.AddComponent<Client.Landlords.RoomComponent, RoomEntity, long>(MongoHelper.Clone(self.GetParent<RoomEntity>()), self.Id);
                    robotComponent.Robot.AddComponent<AIComponent, int>(4);
                }
                else if (!isTrust && robotComponent != null)
                {
                    self.RemoveComponent<UnitRobotComponent>();
                }
            }
        }
    }
}
