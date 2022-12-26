using System.Collections.Generic;

namespace ET
{
    namespace Landlords
    {
        [ComponentOf]
        public class ChatComponent : Entity
        {
            public Dictionary<KeyValuePair<long, long>, SortedList<long, ChatEntity>> Chats = new Dictionary<KeyValuePair<long, long>, SortedList<long, ChatEntity>>();
        }
    }
}
