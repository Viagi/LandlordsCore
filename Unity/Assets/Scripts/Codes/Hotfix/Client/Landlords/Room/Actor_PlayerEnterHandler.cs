using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_PlayerEnterHandler : AMHandler<Actor_PlayerEnter>
        {
            protected override async ETTask Run(Session session, Actor_PlayerEnter message)
            {
                Scene scene = session.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                if(room != null)
                {
                    using (ListComponent<long> units = ListComponent<long>.Create())
                    {
                        foreach (byte[] bytes in message.Entitys)
                        {
                            RoomUnitEntity unit = MongoHelper.Deserialize<RoomUnitEntity>(bytes);
                            room.AddChild(unit);
                            room.Add(unit);
                            units.Add(unit.Id);
                        }

                        await EventSystem.Instance.PublishAsync(scene, new PlayerEnter() { Units = units });
                    }
                }
            }
        }
    }
}
