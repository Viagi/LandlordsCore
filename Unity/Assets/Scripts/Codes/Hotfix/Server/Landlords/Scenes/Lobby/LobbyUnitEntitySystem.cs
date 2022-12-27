using ET.Landlords;
using ET.Server.Landlords;

namespace ET.Server
{
    [ObjectSystem]
    public class LobbyUnitEntityAwakeSystem : AwakeSystem<LobbyUnitEntity>
    {
        protected override void Awake(LobbyUnitEntity self)
        {
            
        }
    }

    [ObjectSystem]
    public class LobbyUnitEntityDestroySystem : DestroySystem<LobbyUnitEntity>
    {
        protected override void Destroy(LobbyUnitEntity self)
        {
            if (Root.Instance.Scene.IsDisposed)
            {
                return;
            }
        }
    }
}
