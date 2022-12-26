using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class Actor_PlayerReadyHandler : AMActorHandler<RoomUnitEntity, Actor_PlayerReady>
        {
            protected override async ETTask Run(RoomUnitEntity unit, Actor_PlayerReady message)
            {
                RoomEntity room = unit.GetParent<RoomEntity>();
                if (room.Status == ERoomStatus.None)
                {
                    unit.Status = ELandlordStatus.Ready;
                    message.UnitId = unit.Id;
                    room.Broadcast(message);

                    bool isStart = true;
                    foreach (RoomUnitEntity entity in room.Children.Values)
                    {
                        if (entity.Status != ELandlordStatus.Ready)
                        {
                            isStart = false;
                            break;
                        }
                    }
                    if (isStart)
                    {
                        room.Start();
                    }
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
