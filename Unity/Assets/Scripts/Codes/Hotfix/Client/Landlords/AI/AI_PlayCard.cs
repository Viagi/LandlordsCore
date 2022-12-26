using ET.Landlords;
using ET.Server;
using System.Collections.Generic;

namespace ET.Client
{
    namespace Landlords
    {
        public class AI_PlayCard : AAIHandler
        {
            public override int Check(AIComponent aiComponent, AIConfig aiConfig)
            {
                Scene scene = aiComponent.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                RoomUnitEntity currentUnit = room?.GetCurrent();

                if (room != null && room.Status == ERoomStatus.PlayCard
                    && currentUnit.Id == roomComponent.MyId)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
            {
                await TimerComponent.Instance.WaitAsync(1000, cancellationToken);
                if (cancellationToken.IsCancel()) return;
                Scene scene = aiComponent.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent.Room;
                RoomUnitEntity myUnit = roomComponent.GetMyUnit();
                RoomUnitEntity activeUnit = room.GetActive();

                if (activeUnit != myUnit && activeUnit.Identity == myUnit.Identity)
                {
                    RoomHelper.PlayCards(scene);
                }
                else
                {
                    using (ListComponent<HandCard> cards = ET.Landlords.RoomHelper.SearchCards(activeUnit.PlayCards, myUnit.HandCards))
                    {
                        RoomHelper.PlayCards(scene, new List<HandCard>(cards));
                    }
                }
            }
        }
    }
}
