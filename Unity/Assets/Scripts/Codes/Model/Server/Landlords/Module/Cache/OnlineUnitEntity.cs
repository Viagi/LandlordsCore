using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    namespace Landlords
    {
        [ChildOf(typeof(Scene))]
        public class OnlineUnitEntity : Entity, IAwake<long, long>
        {
            [BsonElement]
            public long PlayerId { get; set; }
            [BsonElement]
            public long GateId { get; set; }
            [BsonElement]
            public long GateKey { get; set; }
            [BsonElement]
            public long LobbyId { get; set; }
            [BsonElement]
            public long FriendId { get; set; }
            [BsonElement]
            public long RoomId { get; set; }
            [BsonElement]
            public long MatchId { get; set; }
            [BsonIgnore]
            public ETCancellationToken TimeoutToken { get; set; }
        }
    }
}
