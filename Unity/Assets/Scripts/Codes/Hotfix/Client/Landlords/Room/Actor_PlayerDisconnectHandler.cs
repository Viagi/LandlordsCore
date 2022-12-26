using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_PlayerDisconnectHandler : AMHandler<Actor_PlayerDisconnect>
        {
            protected override async ETTask Run(Session session, Actor_PlayerDisconnect message)
            {
                Scene scene = session.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                RoomUnitEntity unit = room.GetChild<RoomUnitEntity>(message.UnitId);
                if (room != null && unit != null)
                {
                    unit.IsOffline = true;
                    unit.IsTrust = true;

                    EventSystem.Instance.Publish(scene, new PlayerDisconnect() { UnitId = message.UnitId });
                }
            }
        }
    }
}
