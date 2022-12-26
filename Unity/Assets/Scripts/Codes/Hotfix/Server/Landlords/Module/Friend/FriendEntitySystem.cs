using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [FriendOf(typeof(FriendEntity))]
        public static class FriendEntitySystem
        {
            public static void AddMessage(this FriendEntity self, long messageId)
            {
                if (!self.Messages.Contains(messageId))
                {
                    self.Messages.Add(messageId);
                }
            }

            public static void RemoveMessage(this FriendEntity self, long messageId)
            {
                self.Messages.Remove(messageId);
            }
        }
    }
}
