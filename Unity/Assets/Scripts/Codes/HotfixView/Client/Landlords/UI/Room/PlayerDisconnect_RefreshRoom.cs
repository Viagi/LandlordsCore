using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class PlayerDisconnect_RefreshRoom : AEvent<PlayerDisconnect>
        {
            protected override async ETTask Run(Scene scene, PlayerDisconnect args)
            {
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                RoomUnitEntity unit = room?.GetChild<RoomUnitEntity>(args.UnitId);
                UIComponent uiComponent = scene.GetComponent<UIComponent>();
                UI ui = uiComponent.Get(UIType.Room);
                UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                UIRoomPlayer player = uiRoomComponent?.Get(args.UnitId);

                if (unit != null && player != null)
                {
                    player.Refresh(unit);
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
