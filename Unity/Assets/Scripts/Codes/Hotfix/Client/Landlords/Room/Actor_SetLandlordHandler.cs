using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_SetLandlordHandler : AMHandler<Actor_SetLandlord>
        {
            protected override async ETTask Run(Session session, Actor_SetLandlord message)
            {
                Scene scene = session.DomainScene();
                RoomEntity room = scene.GetComponent<RoomComponent>().Room;
                RoomUnitEntity landlordUnit = room.Get(message.Index);
                room.SetLandlordCards(message.Cards[0], message.Cards[1], message.Cards[2]);
                foreach (RoomUnitEntity unit in room.Children.Values)
                {
                    if (unit.Id == landlordUnit.Id)
                    {
                        unit.AddCards(message.Cards);
                        unit.Identity = ELandlordIdentity.Landlord;
                    }
                    else
                    {
                        unit.Identity = ELandlordIdentity.Peasantry;
                    }
                    unit.Status = ELandlordStatus.None;
                }

                EventSystem.Instance.Publish(scene, new SetLandlord() { UnitId = landlordUnit.Id, Cards = message.Cards });
                await ETTask.CompletedTask;
            }
        }
    }
}
