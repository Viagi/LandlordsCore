using ET.EventType.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class PlayerEnter_AddPlayer : AEvent<PlayerEnter>
        {
            protected override async ETTask Run(Scene scene, PlayerEnter arg)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
                {
                    UIComponent uiComponent = scene.GetComponent<UIComponent>();
                    UI ui = uiComponent.Get(UIType.Room);
                    UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                    if (uiRoomComponent != null)
                    {
                        foreach (long unitId in arg.Units)
                        {
                            uiRoomComponent.Add(unitId);
                        }
                    }
                }
            }
        }
    }
}
