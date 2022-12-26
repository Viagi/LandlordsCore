using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_PlayCardsHandler : AMHandler<Actor_PlayCards>
        {
            protected override async ETTask Run(Session session, Actor_PlayCards message)
            {
                Scene scene = session.DomainScene();
                RoomEntity room = scene.GetComponent<RoomComponent>().Room;
                RoomUnitEntity unit = room.Get(message.Index);
                if (unit != null)
                {
                    unit.ShowCards(message.Cards);
                    if (message.Cards != null && message.Cards.Count > 0)
                    {
                        unit.Status = ELandlordStatus.ShowCards;
                    }
                    else
                    {
                        unit.Status = ELandlordStatus.DontShow;
                    }
                    EventSystem.Instance.Publish(scene, new PlayCards() { UnitId = unit.Id, Cards = message.Cards });
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
