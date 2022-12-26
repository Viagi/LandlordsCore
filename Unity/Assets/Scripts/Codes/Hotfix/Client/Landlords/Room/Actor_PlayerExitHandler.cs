using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_PlayerExitHandler : AMHandler<Actor_PlayerExit>
        {
            protected override async ETTask Run(Session session, Actor_PlayerExit message)
            {
                Scene scene = session.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                if(room != null)
                {
                    room.RemoveChild(message.UnitId);
                    room.Remove(message.UnitId);
                    await EventSystem.Instance.PublishAsync(scene, new PlayerExit() { UnitId = message.UnitId });
                }
            }
        }
    }
}
