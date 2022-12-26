using ET.EventType.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class PlayerReady_RefreshPlayer : AEvent<PlayerReady>
        {
            protected override async ETTask Run(Scene scene, PlayerReady arg)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
                {
                    UIComponent uiComponent = scene.GetComponent<UIComponent>();
                    UI ui = uiComponent.Get(UIType.Room);
                    UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                    UIRoomPlayer player = uiRoomComponent?.Get(arg.UnitId);
                    if (player != null)
                    {
                        player.Ready();
                    }
                }
            }
        }
    }
}
