using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class G2C_EnterRoomHandler : AMHandler<G2C_EnterRoom>
        {
            protected override async ETTask Run(Session session, G2C_EnterRoom message)
            {
                Scene scene = session.DomainScene();
                RoomEntity room = MongoHelper.Deserialize<RoomEntity>(message.Entity);
                scene.AddComponent<RoomComponent, RoomEntity, long>(room, message.UnitId);

                scene.GetComponent<ObjectWait>().Notify(new Wait_JoinRoom());

                await ETTask.CompletedTask;
            }
        }
    }
}
