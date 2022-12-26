using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class ChatEntityAwakeSystem : AwakeSystem<ChatEntity, string, long, long>
        {
            protected override void Awake(ChatEntity self, string content, long sender, long receiver)
            {
                self.Content = content;
                self.Sender = sender;
                self.Receiver = receiver;
                self.Time = TimeHelper.ServerNow();
            }
        }
    }
}
