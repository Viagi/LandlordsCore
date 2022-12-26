using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class Actor_RobLandlordHandler : AMActorHandler<RoomUnitEntity, Actor_RobLandlord>
        {
            protected override async ETTask Run(RoomUnitEntity unit, Actor_RobLandlord message)
            {
                RoomEntity room = unit.GetParent<RoomEntity>();
                if (room.GetCurrent() == unit)
                {
                    message.Index = room.Current;
                    if (message.RobLandlord)
                    {
                        room.Active = room.Current;
                        room.Rate *= 2;
                        if (unit.Status == ELandlordStatus.CallLandlord || unit.Status == ELandlordStatus.RobLandlord)
                            unit.Status = ELandlordStatus.RobAgain;
                        else
                            unit.Status = ELandlordStatus.RobLandlord;
                    }
                    else
                    {
                        unit.Status = ELandlordStatus.DontRob;
                    }
                    room.Broadcast(message);
                    room.Next();
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
