using System;
using System.Linq;

namespace ET.Server
{
    namespace Landlords
    {
        [FriendOf(typeof(RobotManagerComponent))]
        public static class RobotManagerComponentSystem
        {
            public static async ETTask<Scene> NewRobot(this RobotManagerComponent self, int zone)
            {
                Scene clientScene = null;
                string account = $"robot_{zone}";
                string password = $"{account}_password";
                try
                {
                    clientScene = await Client.SceneFactory.CreateClientScene(zone, "Robot");
                    int error = await Client.Landlords.LoginHelper.Login(clientScene, account, password);
                    if (error == ErrorCode.ERR_AccountOrPasswordError)
                    {
                        await Client.Landlords.LoginHelper.Register(clientScene, account, password);
                    }
                    Log.Debug($"create robot ok: {zone}");
                    return clientScene;
                }
                catch (Exception e)
                {
                    clientScene?.Dispose();
                    throw new Exception($"RobotSceneManagerComponent create robot fail, zone: {zone}", e);
                }
            }

            public static async ETTask<Scene> NewUnitRobot(this RobotManagerComponent self, Entity unit)
            {
                Scene clientScene = null;
                try
                {
                    clientScene = await Client.SceneFactory.CreateUnitScene(unit, "UnitRobot");
                    StartSceneConfig config = RealmGateAddressHelper.GetGate(unit.DomainZone());
                    int error = await Client.Landlords.LoginHelper.Connect(clientScene, config.InnerIPOutPort.ToString(), unit);
                    if (error != 0)
                    {
                        throw new Exception($"Connect failed error:{error}");
                    }

                    Log.Debug($"create unit robot ok: {unit.Id}");
                    return clientScene;
                }
                catch (Exception e)
                {
                    clientScene?.Dispose();
                    throw new Exception($"RobotSceneManagerComponent create robot fail, unitId: {unit.Id}", e);
                }
            }

            public static void RemoveAll(this RobotManagerComponent self)
            {
                foreach (Entity robot in self.Children.Values.ToArray())
                {
                    robot.Dispose();
                }
            }

            public static void Remove(this RobotManagerComponent self, long id)
            {
                self.GetChild<Scene>(id)?.Dispose();
            }

            public static void Clear(this RobotManagerComponent self)
            {
                foreach (Entity entity in self.Children.Values.ToArray())
                {
                    entity.Dispose();
                }
            }
        }
    }
}