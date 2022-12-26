using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_TrustHandler : AMHandler<Actor_Trust>
        {
            protected override async ETTask Run(Session session, Actor_Trust message)
            {
                Scene scene = session.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                RoomUnitEntity unit = room.GetChild<RoomUnitEntity>(message.UnitId);
                if(unit != null)
                {
                    unit.IsTrust = message.IsTrust;
                    EventSystem.Instance.Publish(scene, new PlayerTrust() { UnitId = message.UnitId });
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
