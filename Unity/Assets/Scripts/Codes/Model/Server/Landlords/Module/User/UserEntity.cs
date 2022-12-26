using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    namespace Landlords
    {
        [ChildOf(typeof(UserManagerComponent))]
        public class UserEntity : Entity, IAwake, ISerializeToEntity
        {
            [BsonElement]
            [BsonIgnoreIfNull]
            public string Account { get; set; }
            [BsonElement]
            [BsonIgnoreIfNull]
            public string Password { get; set; }

            [BsonIgnore]
            public ETCancellationToken Saver { get; set; }
        }
    }
}
