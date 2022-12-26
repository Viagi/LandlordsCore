namespace ET.Server
{
    namespace Landlords
    {
        [ChildOf(typeof(MatchUnitManagerComponent))]
        public class MatchUnitEntity : Entity, IAwake, IDestroy
        {
            public long RoomId { get; set; }
            public long MatchLevel { get; set; }
            public long MatchTime { get; set; }
            public bool IsPlaying { get; set; }
            public bool IsOffline { get; set; }
        }
    }
}
