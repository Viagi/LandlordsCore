using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class GameEnd_RefreshRoom : AEvent<GameEnd>
        {
            protected override async ETTask Run(Scene scene, GameEnd args)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
                {
                    RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                    RoomEntity room = roomComponent?.Room;
                    RoomUnitEntity currentUnit = room?.GetCurrent();
                    UIComponent uiComponent = scene.GetComponent<UIComponent>();
                    UI ui = uiComponent.Get(UIType.Room);
                    UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                    if (uiRoomComponent != null && room != null)
                    {
                        uiRoomComponent.RefreshReady(roomComponent);
                        uiRoomComponent.GetComponent<EndComponent>().Show(currentUnit.Id, args.Results);
                        foreach (RoomUnitEntity unit in room.Children.Values)
                        {
                            uiRoomComponent.Get(unit.Id).Refresh(unit);
                        }
                    }
                }
            }
        }
    }
}
