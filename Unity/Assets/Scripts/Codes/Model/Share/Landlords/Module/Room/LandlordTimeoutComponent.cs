using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ET
{
    namespace Landlords
    {
        [ComponentOf(typeof(RoomUnitEntity))]
        public class LandlordTimeoutComponent : Entity, IAwake<long>, IDestroy, ISerializeToEntity
        {
            [BsonIgnore]
            public long Timer;
            [BsonElement]
            public long StartTime { get; set; }
            [BsonElement]
            public long EndTime { get; set; }
        }
    }
}
