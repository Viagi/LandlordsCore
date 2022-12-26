using ET.EventType.Landlords;
using ET.Landlords;
using UnityEngine;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class PlayerTrust_RefreshRoom : AEvent<PlayerTrust>
        {
            protected override async ETTask Run(Scene scene, PlayerTrust args)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
                {
                    RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                    RoomEntity room = roomComponent?.Room;
                    RoomUnitEntity unit = room.GetChild<RoomUnitEntity>(args.UnitId);
                    UIComponent uiComponent = scene.GetComponent<UIComponent>();
                    UI ui = uiComponent.Get(UIType.Room);
                    UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                    UIRoomPlayer player = uiRoomComponent?.Get(args.UnitId);
                    if (uiRoomComponent != null && roomComponent != null && unit != null)
                    {
                        player.Refresh(unit);
                        uiRoomComponent.GetComponent<InteractionComponent>().Refresh(roomComponent);
                    }
                }
            }
        }
    }
}
