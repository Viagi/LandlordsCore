using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [FriendOf(typeof(FriendComponent))]
        public static class FriendComponentSystem
        {
            public static FriendEntity Get(this FriendComponent self, long userId)
            {
                return self.GetChild<FriendEntity>(userId);
            }

            public static async ETTask<FriendEntity> Add(this FriendComponent self, long userId)
            {
                FriendEntity entity = self.Get(userId);
                if (entity == null)
                {
                    entity = self.AddChildWithId<FriendEntity>(userId);
                    UserEntity selfUser = self.GetParent<UserEntity>();
                    await selfUser.SaveDB();
                }

                return entity;
            }

            public static async ETTask Remove(this FriendComponent self, long userId)
            {
                FriendEntity entity = self.Get(userId);
                if (entity != null)
                {
                    self.RemoveChild(userId);
                    UserEntity selfUser = self.GetParent<UserEntity>();
                    await selfUser.SaveDB();
                }
            }
        }
    }
}
