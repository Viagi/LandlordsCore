using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_RobLandlordHandler : AMHandler<Actor_RobLandlord>
        {
            protected override async ETTask Run(Session session, Actor_RobLandlord message)
            {
                Scene scene = session.DomainScene();
                RoomEntity room = scene.GetComponent<RoomComponent>().Room;
                RoomUnitEntity unit = room.Get(message.Index);
                if(unit != null)
                {
                    if (message.RobLandlord)
                    {
                        unit.Status = ELandlordStatus.RobLandlord;
                    }
                    else
                    {
                        unit.Status = ELandlordStatus.DontRob;
                    }
                }

                EventSystem.Instance.Publish(scene, new RobLandlord() { UnitId = unit.Id });
                await ETTask.CompletedTask;
            }
        }
    }
}
