using ET.EventType.Landlords;
using ET.Landlords;
using UnityEngine;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class PlayerSwitch_RefreshRoom : AEvent<PlayerSwitch>
        {
            protected override async ETTask Run(Scene scene, PlayerSwitch args)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
                {
                    RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                    RoomEntity room = roomComponent?.Room;
                    UIComponent uiComponent = scene.GetComponent<UIComponent>();
                    UI ui = uiComponent.Get(UIType.Room);
                    UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                    if (uiRoomComponent != null && roomComponent != null)
                    {
                        uiRoomComponent.GetComponent<InteractionComponent>().Refresh(roomComponent);
                        uiRoomComponent.RefreshRate(roomComponent);
                        foreach (RoomUnitEntity unit in room.Children.Values)
                        {
                            UIRoomPlayer player = uiRoomComponent.Get(unit.Id);
                            player.Refresh(unit);
                        }
                    }
                }
            }
        }
    }
}
