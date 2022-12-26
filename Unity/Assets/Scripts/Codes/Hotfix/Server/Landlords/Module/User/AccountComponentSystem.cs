using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class AccountComponentAwakeSystem : AwakeSystem<AccountComponent, string, long>
        {
            protected override void Awake(AccountComponent self, string nickName, long money)
            {
                self.NickName = nickName;
                self.Money = money;
            }
        }
    }
}
