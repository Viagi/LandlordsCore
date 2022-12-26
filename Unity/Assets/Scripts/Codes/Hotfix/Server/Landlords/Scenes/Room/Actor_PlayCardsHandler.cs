using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class Actor_PlayCardsHandler : AMActorHandler<RoomUnitEntity, Actor_PlayCards>
        {
            protected override async ETTask Run(RoomUnitEntity unit, Actor_PlayCards message)
            {
                RoomEntity room = unit.GetParent<RoomEntity>();
                RoomUnitEntity currentUnit = room.GetCurrent();
                CardGroupType type = CardGroupType.None;
                bool check = false;

                if (currentUnit.Id == unit.Id && message.ShowCards && message.Cards != null)
                {
                    check = room.CheckPlayCards(unit, message.Cards, out type);
                }
                else if (currentUnit.Id == unit.Id && !message.ShowCards)
                {
                    check = true;
                }

                if (check)
                {
                    message.Index = room.Current;
                    if (message.ShowCards)
                    {
                        unit.Status = ELandlordStatus.ShowCards;
                        room.Active = room.Current;
                        if (type == CardGroupType.Bomb)
                        {
                            room.Rate *= 2;
                        }
                        else if (type == CardGroupType.JokerBomb)
                        {
                            room.Rate *= 4;
                        }
                    }
                    else
                    {
                        unit.Status = ELandlordStatus.DontShow;
                    }
                    unit.ShowCards(message.Cards);
                    room.Broadcast(message);
                    room.Next();
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
