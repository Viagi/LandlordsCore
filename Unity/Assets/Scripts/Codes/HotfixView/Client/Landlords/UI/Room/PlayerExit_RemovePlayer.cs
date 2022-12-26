using ET.EventType.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class PlayerExit_RemovePlayer : AEvent<PlayerExit>
        {
            protected override async ETTask Run(Scene scene, PlayerExit arg)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
                {
                    UIComponent uiComponent = scene.GetComponent<UIComponent>();
                    UI ui = uiComponent.Get(UIType.Room);
                    UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                    if(uiRoomComponent != null)
                    {
                        uiRoomComponent.Remove(arg.UnitId);
                    }
                }
            }
        }
    }
}
