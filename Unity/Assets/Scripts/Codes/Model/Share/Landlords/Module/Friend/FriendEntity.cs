using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ET
{
    namespace Landlords
    {
        [ChildOf(typeof(FriendComponent))]
        public class FriendEntity : Entity, IAwake, ISerializeToEntity
        {
            [BsonElement]
            public List<long> Messages = new List<long>();
        }
    }
}
