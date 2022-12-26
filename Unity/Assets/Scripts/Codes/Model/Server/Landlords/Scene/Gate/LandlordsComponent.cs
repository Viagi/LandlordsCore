namespace ET.Server
{
    namespace Landlords
    {
        [ComponentOf(typeof(Player))]
        public class LandlordsComponent : Entity, IAwake<long, bool>, IDestroy
        {
            public bool IsRobot { get; set; }
            public long UserId { get; set; }
            public long LobbyId { get; set; }
            public long FriendId { get; set; }
            public long MatchId { get; set; }
            public long RoomId { get; set; }
        }
    }
}
