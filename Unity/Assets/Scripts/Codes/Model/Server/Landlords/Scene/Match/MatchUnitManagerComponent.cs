using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        [ComponentOf(typeof(Scene))]
        public class MatchUnitManagerComponent : Entity, IAwake, IUpdate
        {
            public MultiMap<long, MatchUnitEntity> Matchers = new MultiMap<long, MatchUnitEntity>();
            public Dictionary<long, ListComponent<MatchUnitEntity>> ReadyRooms = new Dictionary<long, ListComponent<MatchUnitEntity>>();
            public Dictionary<long, ListComponent<MatchUnitEntity>> PlayingRooms = new Dictionary<long, ListComponent<MatchUnitEntity>>();
        }
    }
}
