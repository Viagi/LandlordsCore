using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ET
{
    namespace Landlords
    {
        [ChildOf(typeof(RoomEntity))]
        public class RoomUnitEntity : Entity, IAwake, IDestroy, ISerializeToEntity
        {
            [BsonElement]
            public int Index { get; set; }
            [BsonElement]
            public bool IsOffline { get; set; }
            [BsonElement]
            public bool IsTrust { get; set; }

            [BsonElement]
            public ListComponent<HandCard> HandCards { get; set; }
            [BsonElement]
            public ListComponent<HandCard> PlayCards { get; set; }

            [BsonElement]
            public ELandlordIdentity Identity { get; set; }
            [BsonElement]
            public ELandlordStatus Status { get; set; }
        }
    }
}
