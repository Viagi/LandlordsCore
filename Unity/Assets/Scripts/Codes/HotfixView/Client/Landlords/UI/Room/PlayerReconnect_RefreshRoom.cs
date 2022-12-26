using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class PlayerReconnect_RefreshRoom : AEvent<PlayerReconnect>
        {
            protected override async ETTask Run(Scene scene, PlayerReconnect arg)
            {
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                UIComponent uiComponent = scene.GetComponent<UIComponent>();
                UI ui = uiComponent.Get(UIType.Room);
                UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                if (uiRoomComponent != null && room != null)
                {
                    foreach (long unitId in arg.Units)
                    {
                        RoomUnitEntity unit = room.GetChild<RoomUnitEntity>(unitId);
                        UIRoomPlayer player = uiRoomComponent.Get(unitId);
                        player.Refresh(unit);
                    }
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
