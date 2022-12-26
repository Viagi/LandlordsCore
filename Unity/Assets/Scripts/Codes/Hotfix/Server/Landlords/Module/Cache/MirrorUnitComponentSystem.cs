using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class MirrorUnitComponentAwakeSystem : AwakeSystem<MirrorUnitComponent, long>
        {
            protected override void Awake(MirrorUnitComponent self, long userId)
            {
                self.UserId = userId;
                self.ActorId = self.Parent.InstanceId;
                self.AddOnlineLocation();
            }
        }

        [ObjectSystem]
        public class MirrorUnitComponentDestroySystem : DestroySystem<MirrorUnitComponent>
        {
            protected override void Destroy(MirrorUnitComponent self)
            {
                self.RemoveOnlineLocation();
            }
        }

        [FriendOf(typeof(MirrorUnitComponent))]
        public static class MirrorUnitComponentSystem
        {
            public static void AddOnlineLocation(this MirrorUnitComponent self)
            {
                if (Root.Instance == null)
                {
                    return;
                }

                CacheHelper.Send(new Actor_CreateUnit() { UserId = self.UserId, UnitId = self.ActorId, SceneType = (int)self.DomainScene().SceneType });
            }

            public static void RemoveOnlineLocation(this MirrorUnitComponent self)
            {
                if (Root.Instance == null)
                {
                    return;
                }

                CacheHelper.Send(new Actor_RemoveUnit() { UserId = self.UserId, UnitId = self.ActorId, SceneType = (int)self.DomainScene().SceneType });
            }
        }
    }
}
