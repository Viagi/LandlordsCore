using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class Actor_TrustHandler : AMActorHandler<RoomUnitEntity, Actor_Trust>
        {
            protected override async ETTask Run(RoomUnitEntity unit, Actor_Trust message)
            {
                RoomEntity room = unit.GetParent<RoomEntity>();
                message.UnitId = unit.Id;
                await unit.Trust(message.IsTrust);
                room.Broadcast(message);
            }
        }
    }
}
