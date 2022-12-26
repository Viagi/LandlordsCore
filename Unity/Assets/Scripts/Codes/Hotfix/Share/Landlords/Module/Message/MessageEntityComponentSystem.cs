namespace ET
{
    namespace Landlords
    {
        [FriendOf(typeof(MessageEntityComponent))]
        public static class MessageEntityComponentSystem
        {
            public class MessageEntityComponentAwakeSystem : AwakeSystem<MessageEntityComponent>
            {
                protected override void Awake(MessageEntityComponent self)
                {
                    MessageEntityComponent.Instance = self;
                }
            }

            public static MessageEntity Deserialize(this MessageEntityComponent self, byte[] bytes)
            {
                MessageEntity entity = MongoHelper.Deserialize<MessageEntity>(bytes);
                entity.RawData = bytes;
                self.RemoveChild(entity.Id);
                self.AddChild(entity);

                return entity;
            }

            public static byte[] Serialize(this MessageEntityComponent self, MessageEntity entity)
            {
                byte[] bytes = MongoHelper.Serialize(entity);

                return bytes;
            }

            public static MessageEntity Create(this MessageEntityComponent self, long? id = null)
            {
                if (id != null)
                {
                    self.RemoveChild(id.Value);
                    return self.AddChildWithId<MessageEntity>(id.Value);
                }
                else
                {
                    return self.AddChild<MessageEntity>();
                }
            }

            public static void Clear(this MessageEntityComponent self)
            {
                using (ListComponent<long> ids = ListComponent<long>.Create())
                {
                    foreach (var child in self.Children)
                    {
                        if (child.Value is MessageEntity)
                        {
                            ids.Add(child.Key);
                        }
                    }
                    foreach (long id in ids)
                    {
                        self.RemoveChild(id);
                    }
                }
            }
        }
    }
}
