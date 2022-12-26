namespace ET.Server
{
    namespace Landlords
    {
        [ComponentOf(typeof(Session))]
        public class SessionPlayerComponent : Entity, IAwake<long, long, long>, IDestroy
        {
            public long PlayerId { get; set; }
            public long UserId { get; set; }
            public long GateId { get; set; }
            public long GateKey { get; set; }
        }
    }
}
