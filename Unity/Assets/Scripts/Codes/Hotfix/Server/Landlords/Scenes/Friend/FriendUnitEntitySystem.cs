using ET.Landlords;
using ET.Server.Landlords;

namespace ET.Server
{
    [ObjectSystem]
    public class FriendUnitEntityAwakeSystem : AwakeSystem<FriendUnitEntity>
    {
        protected override void Awake(FriendUnitEntity self)
        {
            
        }
    }

    [ObjectSystem]
    public class FriendUnitEntityDestroySystem : DestroySystem<FriendUnitEntity>
    {
        protected override void Destroy(FriendUnitEntity self)
        {
            if (Root.Instance == null)
            {
                return;
            }
        }
    }
}
