using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [ComponentOf(typeof(Scene))]
        public class RoomComponent : Entity, IAwake<RoomEntity, long>
        {
            public RoomEntity Room { get; set; }

            public long MyId { get; set; }
        }
    }
}
