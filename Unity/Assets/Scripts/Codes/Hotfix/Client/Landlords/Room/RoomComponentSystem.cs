using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [ObjectSystem]
        public class RoomComponentAwakeSystem : AwakeSystem<RoomComponent, RoomEntity, long>
        {
            protected override void Awake(RoomComponent self, RoomEntity room, long unitId)
            {
                self.AddChild(room);
                self.Room = room;
                self.MyId = unitId;
            }
        }

        [FriendOf(typeof(RoomComponent))]
        public static class RoomComponentSystem
        {
            public static RoomUnitEntity GetMyUnit(this RoomComponent self)
            {
                return self.Room.GetChild<RoomUnitEntity>(self.MyId);
            }
        }
    }
}
