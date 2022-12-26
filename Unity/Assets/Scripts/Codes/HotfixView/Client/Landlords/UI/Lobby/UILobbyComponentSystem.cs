using ET.EventType.Landlords;
using ET.Landlords;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ObjectSystem]
        public class UILobbyComponentAwakeSystem : AwakeSystem<UILobbyComponent>
        {
            protected override void Awake(UILobbyComponent self)
            {
                ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
                self.matchBtn = rc.Get<GameObject>("StartMatch").GetComponent<Button>();
                self.matchBtn.onClick.AddListener(() => { self.OnMatch(); });
                self.nickName = rc.Get<GameObject>("NickName").GetComponent<Text>();
                self.money = rc.Get<GameObject>("Money").GetComponent<Text>();
                self.win = rc.Get<GameObject>("Win").GetComponent<Text>();
                self.lose = rc.Get<GameObject>("Lose").GetComponent<Text>();

                self.Request().Coroutine();
            }
        }

        [FriendOf(typeof(UILobbyComponent))]
        public static class UILobbyComponentSystem
        {
            public static async ETTask Request(this UILobbyComponent self)
            {
                Scene scene = self.ClientScene();
                await LobbyHelper.GetLobby(scene);
                self.Refresh();
            }

            public static void Refresh(this UILobbyComponent self)
            {
                Scene scene = self.DomainScene();
                AccountComponent accountComponent = scene.GetComponent<AccountComponent>();
                self.nickName.text = accountComponent.NickName;
                self.money.text = accountComponent.Money.ToString();
                self.win.text = accountComponent.Wins.ToString();
                self.lose.text = accountComponent.Loses.ToString();
            }

            public static void OnMatch(this UILobbyComponent self)
            {
                self.InnerMatch().Coroutine();
            }

            public static async ETTask InnerMatch(this UILobbyComponent self)
            {
                Scene scene = self.DomainScene();
                int error = await LobbyHelper.EnterMatch(scene);
                if (error == 0)
                {
                    EventSystem.Instance.Publish(scene, new StartMatch());
                    Wait_JoinRoom wait_JoinRoom = await scene.GetComponent<ObjectWait>().Wait<Wait_JoinRoom>();
                    if (wait_JoinRoom.Error == WaitTypeError.Success)
                    {
                        EventSystem.Instance.Publish(scene, new EnterRoom());
                    }
                }
                else
                {
                    self.OnFailed(error);
                }
            }

            private static void OnFailed(this UILobbyComponent self, int errorCode)
            {
                switch (errorCode)
                {
                    case ErrorCode.ERR_MoneyNotEnough:
                        Log.Error("余额不足，无法匹配");
                        break;
                }
            }
        }
    }
}
