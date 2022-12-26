using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_CallLandlordHandler : AMHandler<Actor_CallLandlord>
        {
            protected override async ETTask Run(Session session, Actor_CallLandlord message)
            {
                Scene scene = session.DomainScene();
                RoomEntity room = scene.GetComponent<RoomComponent>().Room;
                RoomUnitEntity unit = room.Get(message.Index);
                if(unit != null)
                {
                    if (message.CallLandlord)
                    {
                        unit.Status = ELandlordStatus.CallLandlord;
                    }
                    else
                    {
                        unit.Status = ELandlordStatus.NotCall;
                    }
                }

                EventSystem.Instance.Publish(scene, new RobLandlord() { UnitId = unit.Id });
                await ETTask.CompletedTask;
            }
        }
    }
}
