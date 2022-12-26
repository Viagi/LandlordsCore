using ET.EventType.Landlords;
using ET.Landlords;
using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        [Event(SceneType.Room)]
        public class PlayerTimeout_TurnNext : AEvent<PlayerTimeout>
        {
            protected override async ETTask Run(Scene scene, PlayerTimeout args)
            {
                RoomUnitEntity unit = args.Unit;
                RoomEntity room = unit.GetParent<RoomEntity>();
                IActorMessage message = null;
                switch (room.Status)
                {
                    case ERoomStatus.CallLandlord:
                        message = new Actor_CallLandlord();
                        break;
                    case ERoomStatus.RobLandlord:
                        message = new Actor_RobLandlord();
                        break;
                    case ERoomStatus.PlayCard:
                        Actor_PlayCards playCards = new Actor_PlayCards();
                        if (room.GetActive() == unit)
                        {
                            using (ListComponent<HandCard> cards = ET.Landlords.RoomHelper.SearchCards(unit.PlayCards, unit.HandCards))
                            {
                                playCards.Cards = new List<HandCard>(cards);
                            }
                        }
                        message = playCards;
                        break;
                }

                NetInnerComponent.Instance.HandleMessage(unit.InstanceId, message);
                await ETTask.CompletedTask;
            }
        }
    }
}
