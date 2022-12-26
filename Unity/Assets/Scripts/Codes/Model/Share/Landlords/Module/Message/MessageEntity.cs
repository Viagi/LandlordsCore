using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    namespace Landlords
    {
        [ChildOf(typeof(MessageEntityComponent))]
        public class MessageEntity : Entity, IAwake, IDestroy
        {
            [BsonIgnore]
            public byte[] RawData { get; set; }
        }
    }
}
