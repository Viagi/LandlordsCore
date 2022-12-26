using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    namespace Landlords
    {
        [ComponentOf]
        public class AccountComponent : Entity, IAwake<string, long>, ISerializeToEntity
        {
            [BsonElement]
            public string NickName { get; set; }
            [BsonElement]
            public long Money { get; set; }
            [BsonElement]
            public int Wins { get; set; }
            [BsonElement]
            public int Loses { get; set; }
        }
    }
}
