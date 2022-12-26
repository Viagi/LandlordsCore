using UnityEngine;
using UnityEngine.UI;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [FriendOf(typeof(UILoginComponent))]
        public static class UILoginComponentSystem
        {
            [ObjectSystem]
            public class UILoginComponentAwakeSystem : AwakeSystem<UILoginComponent>
            {
                protected override void Awake(UILoginComponent self)
                {
                    ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
                    self.loginBtn = rc.Get<GameObject>("LoginButton").GetComponent<Button>();
                    self.loginBtn.onClick.AddListener(() => { self.OnLogin(); });
                    self.registerBtn = rc.Get<GameObject>("RegisterButton").GetComponent<Button>();
                    self.registerBtn.onClick.AddListener(() => { self.OnRegister(); });
                    self.account = rc.Get<GameObject>("Account").GetComponent<InputField>();
                    self.password = rc.Get<GameObject>("Password").GetComponent<InputField>();
                    self.prompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();
                    self.hotfixPrompt = rc.Get<GameObject>("HotfixPrompt").GetComponent<Text>();

                    self.prompt.text = "ET斗地主7.2";
                }
            }

            private static bool Check(this UILoginComponent self)
            {
                if (string.IsNullOrEmpty(self.account.text) || string.IsNullOrEmpty(self.password.text))
                {
                    self.hotfixPrompt.text = "账号或密码不能为空！";
                    return false;
                }

                return true;
            }

            public static void OnLogin(this UILoginComponent self)
            {
                if(!self.Check())
                {
                    return;
                }

                self.loginBtn.gameObject.SetActive(false);
                self.registerBtn.gameObject.SetActive(false);
                self.hotfixPrompt.text = "正在登录中...";
                self.InnerLogin().Coroutine();
            }

            private static async ETTask InnerLogin(this UILoginComponent self)
            {
                Scene scene = self.DomainScene();
                int error = await LoginHelper.Login(self.DomainScene(),
                    self.account.text,
                    self.password.text);

                if(error == 0)
                {
                    await EventSystem.Instance.PublishAsync(scene, new EventType.Landlords.LoginFinish());
                }
                else
                {
                    self.OnFailed(error);
                }
            }

            public static void OnRegister(this UILoginComponent self)
            {
                if (!self.Check())
                {
                    return;
                }

                self.loginBtn.gameObject.SetActive(false);
                self.registerBtn.gameObject.SetActive(false);
                self.hotfixPrompt.text = "正在注册中...";
                self.InnerRegister().Coroutine();
            }

            private static async ETTask InnerRegister(this UILoginComponent self)
            {
                Scene scene = self.DomainScene();
                int error = await LoginHelper.Register(scene,
                    self.account.text,
                    self.password.text);

                if (error == 0)
                {
                    await EventSystem.Instance.PublishAsync(scene, new EventType.Landlords.LoginFinish());
                }
                else
                {
                    self.OnFailed(error);
                }
            }

            public static void OnFailed(this UILoginComponent self, int code)
            {
                self.loginBtn.gameObject.SetActive(true);
                self.registerBtn.gameObject.SetActive(true);
                switch (code)
                {
                    case ErrorCode.ERR_AccountOrPasswordError:
                        self.hotfixPrompt.text = $"登录失败\n账号或密码不正确";
                        break;
                    case ErrorCode.ERR_AccountAlreadyExist:
                        self.hotfixPrompt.text = $"注册失败\n账号已存在";
                        break;
                    default:
                        self.hotfixPrompt.text = $"请求失败,网络异常";
                        break;
                }
            }
        }
    }
}
