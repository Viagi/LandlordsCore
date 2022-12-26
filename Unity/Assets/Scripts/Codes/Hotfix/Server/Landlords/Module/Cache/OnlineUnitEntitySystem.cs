namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class OnlineUnitEntityAwakeSystem : AwakeSystem<OnlineUnitEntity, long, long>
        {
            protected override void Awake(OnlineUnitEntity self, long gateId, long gateKey)
            {
                self.GateId = gateId;
                self.GateKey = gateKey;
            }
        }

        [FriendOf(typeof(OnlineUnitEntity))]
        public static class OnlineUnitEntitySystem
        {
            public static async ETTask StartTimeout(this OnlineUnitEntity self)
            {
                self.StopTimeout();
                self.TimeoutToken = new ETCancellationToken();
                await TimerComponent.Instance.WaitAsync(20000, self.TimeoutToken);
                self.TimeoutToken = null;
                self.Dispose();
            }

            public static void StopTimeout(this OnlineUnitEntity self)
            {
                if (self.TimeoutToken != null)
                {
                    self.TimeoutToken.Cancel();
                    self.TimeoutToken = null;
                }
            }

            public static bool Check(this OnlineUnitEntity self, long gateId, long gateKey)
            {
                return self.GateId == gateId && self.GateKey == gateKey;
            }
        }
    }
}
