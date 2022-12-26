using ET.EventType.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class EnterRoom_InitRoomUI : AEvent<EnterRoom>
        {
            protected override async ETTask Run(Scene scene, EnterRoom args)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
                {
                    UIComponent uiComponent = scene.GetComponent<UIComponent>();
                    UI ui = uiComponent.Get(UIType.Room);
                    RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                    UIRoomComponent uiRoomComponent = ui.GetComponent<UIRoomComponent>();
                    if (uiRoomComponent != null && roomComponent != null)
                    {
                        uiRoomComponent.Init(roomComponent);
                        uiRoomComponent.GetComponent<InteractionComponent>().Refresh(roomComponent);
                    }
                }
            }
        }
    }
}
