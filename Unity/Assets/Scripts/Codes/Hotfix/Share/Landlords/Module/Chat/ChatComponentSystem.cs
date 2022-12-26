using SharpCompress.Common;
using System.Collections;
using System.Collections.Generic;

namespace ET
{
    namespace Landlords
    {
        [FriendOf(typeof(ChatComponent))]
        public static class ChatComponentSystem
        {
            public static SortedList<long, ChatEntity> Get(this ChatComponent self, long sender, long receiver)
            {
                SortedList<long, ChatEntity> entitys;
                self.Chats.TryGetValue(new KeyValuePair<long, long>(sender, receiver), out entitys);

                return entitys;
            }

            public static SortedList<long, ChatEntity> Add(this ChatComponent self, ChatEntity entity)
            {
                if (self.GetChild<ChatEntity>(entity.Id) == null)
                {
                    self.AddChild(entity);
                }

                SortedList<long, ChatEntity> entitys = self.Get(entity.Sender, entity.Receiver);
                if (entitys == null)
                {
                    entitys = ObjectPool.Instance.Fetch<SortedList<long, ChatEntity>>();
                    self.Chats.Add(new KeyValuePair<long, long>(entity.Sender, entity.Receiver), entitys);
                    self.Chats.Add(new KeyValuePair<long, long>(entity.Receiver, entity.Sender), entitys);
                }

                entitys.Add(entity.Time, entity);

                return entitys;
            }

            public static void Remove(ChatComponent self, long sender, long receiver)
            {
                SortedList<long, ChatEntity> entitys = self.Get(sender, receiver);
                if (entitys != null)
                {
                    self.Chats.Remove(new KeyValuePair<long, long>(sender, receiver));
                    self.Chats.Remove(new KeyValuePair<long, long>(receiver, sender));
                    foreach (ChatEntity entity in entitys.Values)
                    {
                        self.RemoveChild(entity.Id);
                    }
                    entitys.Clear();
                    ObjectPool.Instance.Recycle(entitys);
                }
            }
        }
    }
}
