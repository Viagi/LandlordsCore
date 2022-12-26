using ET.Landlords;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class Actor_ExitRoomRequestHandler : AMActorLocationRpcHandler<RoomUnitEntity, Actor_ExitRoomRequest, Actor_ExitRoomResponse>
        {
            protected override async ETTask Run(RoomUnitEntity unit, Actor_ExitRoomRequest request, Actor_ExitRoomResponse response, Action reply)
            {
                RoomEntity room = unit.GetParent<RoomEntity>();
                if (room.Status != ERoomStatus.None)
                {
                    await unit.Offline();
                }
                else
                {
                    long unitId = unit.Id;
                    room.Remove(unit.Id);
                    unit.Dispose();
                    room.Broadcast(new Actor_PlayerExit() { UnitId = unitId });
                }

                reply();
            }
        }
    }
}
