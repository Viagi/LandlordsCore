using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_GameStartHandler : AMHandler<Actor_GameStart>
        {
            protected override async ETTask Run(Session session, Actor_GameStart message)
            {
                Scene scene = session.DomainScene();
                RoomEntity room = scene.GetComponent<RoomComponent>()?.Room;
                if(room != null)
                {
                    foreach (byte[] bytes in message.Entitys)
                    {
                        RoomUnitEntity unit = MongoHelper.Deserialize<RoomUnitEntity>(bytes);
                        room.RemoveChild(unit.Id);
                        room.AddChild(unit);
                    }
                    room.Status = ERoomStatus.CallLandlord;

                    EventSystem.Instance.Publish(scene, new GameStart());
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
