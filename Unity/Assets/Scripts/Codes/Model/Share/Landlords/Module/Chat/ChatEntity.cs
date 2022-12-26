using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    namespace Landlords
    {
        [ChildOf]
        public class ChatEntity : Entity, IAwake<string, long, long>
        {
            [BsonElement]
            public string Content { get; set; }
            [BsonElement]
            public long Sender { get; set; }
            [BsonElement]
            public long Receiver { get; set; }
            [BsonElement]
            public long Time { get; set; }
        }
    }
}
