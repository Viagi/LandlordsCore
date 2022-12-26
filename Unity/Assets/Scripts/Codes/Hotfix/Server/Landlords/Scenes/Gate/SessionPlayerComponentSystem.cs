using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class SessionPlayerComponentAwakeSystem : AwakeSystem<SessionPlayerComponent, long, long, long>
        {
            protected override void Awake(SessionPlayerComponent self, long userId, long gateId, long gateKey)
            {
                self.UserId = userId;
                self.GateId = gateId;
                self.GateKey = gateKey;
            }
        }

        [ObjectSystem]
        public class SessionPlayerComponentDestroySystem : DestroySystem<SessionPlayerComponent>
        {
            protected override void Destroy(SessionPlayerComponent self)
            {
                if (Root.Instance == null)
                {
                    return;
                }

                // 断线处理
                PlayerComponent playerComponent = self.DomainScene().GetComponent<PlayerComponent>();
                Player player = playerComponent.Get(self.PlayerId);
                if (player != null)
                {
                    CacheHelper.Send(new Actor_RemoveOnlineUnit() { UserId = self.UserId, GateId = self.GateId, GateKey = self.GateKey });
                    playerComponent.Remove(self.PlayerId);
                    player.Dispose();
                }
            }
        }

        [FriendOf(typeof(SessionPlayerComponent))]
        public static class SessionPlayerComponentSystem
        {
            public static Player GetPlayer(this SessionPlayerComponent self)
            {
                return self.DomainScene().GetComponent<PlayerComponent>().Get(self.PlayerId);
            }
        }
    }
}
