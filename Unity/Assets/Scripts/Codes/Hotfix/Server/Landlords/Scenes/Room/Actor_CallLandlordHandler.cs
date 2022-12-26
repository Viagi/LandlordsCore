using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class Actor_CallLandlordHandler : AMActorHandler<RoomUnitEntity, Actor_CallLandlord>
        {
            protected override async ETTask Run(RoomUnitEntity unit, Actor_CallLandlord message)
            {
                RoomEntity room = unit.GetParent<RoomEntity>();
                if(room.GetCurrent() == unit)
                {
                    message.Index = room.Current;
                    if (message.CallLandlord)
                    {
                        unit.Status = ELandlordStatus.CallLandlord;
                        room.Active = room.Current;
                    }
                    else
                    {
                        unit.Status = ELandlordStatus.NotCall;
                    }
                    room.Broadcast(message);
                    room.Next();
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
