namespace ET.Server
{
    namespace Landlords
    {
        [ComponentOf]
        public class MirrorUnitComponent : Entity, IAwake<long>, IDestroy
        {
            public long UserId { get; set; }
            public long ActorId { get; set; }
        }
    }
}
