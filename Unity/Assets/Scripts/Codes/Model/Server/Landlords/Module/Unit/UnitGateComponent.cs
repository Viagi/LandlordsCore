using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    namespace Landlords
    {
        [ComponentOf]
        public class UnitGateComponent : Entity, IAwake<long, long>
        {
            [BsonElement]
            public long UnitId { get; set; }

            [BsonElement]
            public long GateSessionId { get; set; }
        }
    }
}
