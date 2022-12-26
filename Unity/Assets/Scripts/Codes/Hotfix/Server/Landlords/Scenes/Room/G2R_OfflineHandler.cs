using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class G2R_OfflineHandler : AMActorHandler<RoomUnitEntity, Actor_Offline>
        {
            protected override async ETTask Run(RoomUnitEntity unit, Actor_Offline message)
            {
                RoomEntity room = unit.GetParent<RoomEntity>();
                if (room.Status != ERoomStatus.None)
                {
                    await unit.Offline();
                    room.Broadcast(new Actor_PlayerDisconnect() { UnitId = unit.Id });
                }
                else
                {
                    long unitId = unit.Id;
                    room.Remove(unit.Id);
                    unit.Dispose();
                    room.Broadcast(new Actor_PlayerExit() { UnitId = unitId });
                }
            }
        }
    }
}
