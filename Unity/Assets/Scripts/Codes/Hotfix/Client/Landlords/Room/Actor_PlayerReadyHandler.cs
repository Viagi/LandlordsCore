using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_PlayerReadyHandler : AMHandler<Actor_PlayerReady>
        {
            protected override async ETTask Run(Session session, Actor_PlayerReady message)
            {
                Scene scene = session.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                if(room != null)
                {
                    RoomUnitEntity unit = room.GetChild<RoomUnitEntity>(message.UnitId);
                    unit.Status = ELandlordStatus.Ready;

                    await EventSystem.Instance.PublishAsync(scene, new PlayerReady() { UnitId = message.UnitId });
                }
            }
        }
    }
}
