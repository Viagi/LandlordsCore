using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ET
{
    namespace Landlords
    {
        [ChildOf]
        public class RoomEntity : Entity, IAwake<long, long, long>, IDestroy
        {

            [BsonElement]
            public ListComponent<long> Seats { get; set; }
            [BsonElement]
            public ListComponent<HandCard> LandlordCards { get; set; }
            [BsonElement]
            public long Timeout { get; set; }
            [BsonElement]
            public long BaseMoney { get; set; }
            [BsonElement]
            public long BaseRate { get; set; }

            [BsonElement]
            public ERoomStatus Status { get; set; }
            [BsonElement]
            public int Current { get; set; } = -1;
            [BsonElement]
            public int Active { get; set; } = -1;
            [BsonElement]
            public long Rate { get; set; } = 1;
        }
    }
}
