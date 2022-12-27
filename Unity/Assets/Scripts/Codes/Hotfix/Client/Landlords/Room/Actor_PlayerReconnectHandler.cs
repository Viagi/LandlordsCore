using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_PlayerReconnectHandler : AMHandler<Actor_PlayerReconnect>
        {
            protected override async ETTask Run(Session session, Actor_PlayerReconnect message)
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
                            room.RemoveChild(unit.Id);
                            room.AddChild(unit);
                            units.Add(unit.Id);
                        }
                        EventSystem.Instance.Publish(scene, new PlayerReconnect() { Units = units });
                    }
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
