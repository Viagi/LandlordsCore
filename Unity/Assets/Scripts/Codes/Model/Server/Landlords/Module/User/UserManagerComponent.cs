using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        [ComponentOf(typeof(Scene))]
        public class UserManagerComponent : Entity, IAwake
        {
            public readonly Dictionary<long, UserEntity> Users = new Dictionary<long, UserEntity>();
            public readonly Dictionary<KeyValuePair<string, string>, long> AccountPasswordDict = new Dictionary<KeyValuePair<string, string>, long>();
            public readonly Dictionary<string, long> AccountDict = new Dictionary<string, long>();
        }
    }
}
