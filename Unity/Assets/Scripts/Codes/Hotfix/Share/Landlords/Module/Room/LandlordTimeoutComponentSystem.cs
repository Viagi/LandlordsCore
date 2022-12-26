using ET.EventType.Landlords;
using System;

namespace ET
{
    namespace Landlords
    {
        [Invoke(TimerInvokeType.LandlordTimeout)]
        public class LandlordTimeout : ATimer<LandlordTimeoutComponent>
        {
            protected override void Run(LandlordTimeoutComponent self)
            {
                try
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new PlayerTimeout() { Unit = self.GetParent<RoomUnitEntity>() });
                    self.Dispose();
                }
                catch (Exception e)
                {
                    Log.Error($"landlord timer error: {self.Id}\n{e}");
                }
            }
        }

        [ObjectSystem]
        public class LandlordTimeoutComponentAwakeSystem : AwakeSystem<LandlordTimeoutComponent, long>
        {
            protected override void Awake(LandlordTimeoutComponent self, long timeout)
            {
                self.StartTime = TimeHelper.ServerFrameTime();
                self.EndTime = self.StartTime + timeout;
                self.Timer = TimerComponent.Instance.NewOnceTimer(self.EndTime, TimerInvokeType.LandlordTimeout, self);
            }
        }

        [ObjectSystem]
        public class LandlordTimeoutComponentDestroySystem : DestroySystem<LandlordTimeoutComponent>
        {
            protected override void Destroy(LandlordTimeoutComponent self)
            {
                TimerComponent.Instance?.Remove(ref self.Timer);
            }
        }

        [FriendOf(typeof(LandlordTimeoutComponent))]
        public static class LandlordTimeoutComponentSystem
        {
            public static long GetTime(this LandlordTimeoutComponent self)
            {
                long remain = self.EndTime - TimeHelper.ServerFrameTime();
                if (remain < 0) remain = 0;

                return remain / 1000;
            }
        }
    }
}
