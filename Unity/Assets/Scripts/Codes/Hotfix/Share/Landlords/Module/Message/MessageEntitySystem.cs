namespace ET
{
    namespace Landlords
    {
        [ObjectSystem]
        public class MessageEntityDestroySystem : DestroySystem<MessageEntity>
        {
            protected override void Destroy(MessageEntity self)
            {
                self.RawData = null;
            }
        }

        [FriendOf(typeof(MessageEntity))]
        public static class MessageEntitySystem
        {
            public static void Add(this MessageEntity self, Entity entity)
            {
                byte[] bytes = MongoHelper.Serialize(entity);
                Entity cloneEntity = MongoHelper.Deserialize<Entity>(bytes);
                self.AddComponent(cloneEntity);
            }

            public static T Get<T>(this MessageEntity self) where T : Entity
            {
                Entity entity = self.GetComponent(typeof(T));
                return entity as T;
            }
        }
    }
}
