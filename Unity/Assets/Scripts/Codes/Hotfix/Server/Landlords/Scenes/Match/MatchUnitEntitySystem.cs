using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class MatchUnitEntityAwakeSystem : AwakeSystem<MatchUnitEntity>
        {
            protected override void Awake(MatchUnitEntity self)
            {
                
            }
        }

        [ObjectSystem]
        public class MatchUnitEntityDestroySystem : DestroySystem<MatchUnitEntity>
        {
            protected override void Destroy(MatchUnitEntity self)
            {
                if (Root.Instance == null)
                {
                    return;
                }
            }
        }

        [FriendOf(typeof(MatchUnitEntity))]
        public static class MatchUnitEntitySystem
        {
            public static long GetRank(this MatchUnitEntity entity)
            {
                AccountComponent accountComponent = entity.GetComponent<AccountComponent>();
                //排名=胜率(胜场+负场/胜场) * 余额
                return (long)(((float)accountComponent.Wins + accountComponent.Loses) / accountComponent.Wins) * accountComponent.Money;
            }
        }
    }
}
