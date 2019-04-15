using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

namespace ETHotfix
{
    [ObjectSystem]
    public class LandlordsLoginComponentAwakeSystem : AwakeSystem<LandlordsLoginComponent>
    {
        public override void Awake(LandlordsLoginComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 登录界面组件
    /// </summary>
    public class LandlordsLoginComponent : Component
    {
        //账号输入框
        private InputField account;
        //密码输入框
        private InputField password;
        //提示文本
        private Text prompt;
        //是否正在登录中（避免登录请求还没响应时连续点击登录）
        private bool isLogining;
        //是否正在注册中（避免登录请求还没响应时连续点击注册）
        private bool isRegistering;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            //热更测试
            Text hotfixPrompt = rc.Get<GameObject>("HotfixPrompt").GetComponent<Text>();
#if ILRuntime
            hotfixPrompt.text = "斗地主4.0.0 (ILRuntime模式)";
#else
            hotfixPrompt.text = "斗地主4.0.0 (Mono模式)";
#endif

            //绑定关联对象
            account = rc.Get<GameObject>("Account").GetComponent<InputField>();
            password = rc.Get<GameObject>("Password").GetComponent<InputField>();
            prompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();

            //添加事件
            rc.Get<GameObject>("LoginButton").GetComponent<Button>().onClick.Add(OnLogin);
            rc.Get<GameObject>("RegisterButton").GetComponent<Button>().onClick.Add(OnRegister);
        }

        /// <summary>
        /// 设置提示
        /// </summary>
        /// <param name="str"></param>
        public void SetPrompt(string str)
        {
            this.prompt.text = str;
        }

        /// <summary>
        /// 登录按钮事件
        /// </summary>
        public async void OnLogin()
        {
            if (isLogining || this.IsDisposed)
            {
                return;
            }
            NetOuterComponent netOuterComponent = Game.Scene.ModelScene.GetComponent<NetOuterComponent>();

            //设置登录中状态
            isLogining = true;
            Session sessionWrap = null;
            try
            {
                //创建登录服务器连接
                IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalConfigComponent.Instance.GlobalProto.Address);
                ETModel.Session session = netOuterComponent.Create(connetEndPoint);
                sessionWrap = ComponentFactory.Create<Session, ETModel.Session>(session);
                sessionWrap.session.GetComponent<SessionCallbackComponent>().DisposeCallback += s => 
                {
                    if (Game.Scene.GetComponent<UIComponent>()?.Get(UIType.LandlordsLogin) != null)
                    {
                        prompt.text = "断开连接";
                        isLogining = false;
                    }
                };

                //发送登录请求
                prompt.text = "正在登录中....";
                R2C_Login_Ack r2C_Login_Ack = await sessionWrap.Call(new C2R_Login_Req() { Account = account.text, Password = password.text }) as R2C_Login_Ack;
                prompt.text = "";

                if (this.IsDisposed)
                {
                    return;
                }

                if (r2C_Login_Ack.Error == ErrorCode.ERR_LoginError)
                {
                    prompt.text = "登录失败,账号或密码错误";
                    password.text = "";

                    //断开验证服务器的连接
                    sessionWrap.Dispose();
                    //设置登录处理完成状态
                    isLogining = false;
                    return;
                }

                //创建Gate服务器连接
                connetEndPoint = NetworkHelper.ToIPEndPoint(r2C_Login_Ack.Address);
                ETModel.Session gateSession = netOuterComponent.Create(connetEndPoint);
                Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);
                //SessionWeap添加连接断开组件，用于处理客户端连接断开
                Game.Scene.GetComponent<SessionComponent>().Session.AddComponent<SessionOfflineComponent>();
                Game.Scene.ModelScene.AddComponent<ETModel.SessionComponent>().Session = gateSession;

                //登录Gate服务器
                G2C_LoginGate_Ack g2C_LoginGate_Ack = await SessionComponent.Instance.Session.Call(new C2G_LoginGate_Req() { Key = r2C_Login_Ack.Key }) as G2C_LoginGate_Ack;
                if (g2C_LoginGate_Ack.Error == ErrorCode.ERR_ConnectGateKeyError)
                {
                    prompt.text = "连接网关服务器超时";
                    password.text = "";
                    Game.Scene.GetComponent<SessionComponent>().Session.Dispose();

                    //断开验证服务器的连接
                    sessionWrap.Dispose();
                    //设置登录处理完成状态
                    isLogining = false;
                    return;
                }

                Log.Info("登录成功");

                //保存本地玩家
                User user = ETModel.ComponentFactory.CreateWithId<User, long>(g2C_LoginGate_Ack.PlayerID, g2C_LoginGate_Ack.UserID);
                ClientComponent.Instance.LocalPlayer = user;

                //跳转到大厅界面
                Game.Scene.GetComponent<UIComponent>().Create(UIType.LandlordsLobby);
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.LandlordsLogin);

            }
            catch (Exception e)
            {
                prompt.text = "登录异常";
                Log.Error(e.ToStr());

                //断开验证服务器的连接
                sessionWrap.Dispose();
                //设置登录处理完成状态
                isLogining = false;
            }
        }

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        public async void OnRegister()
        {
            if (isRegistering || this.IsDisposed)
            {
                return;
            }

            //设置登录中状态
            isRegistering = true;
            Session sessionWrap = null;
            prompt.text = "";
            try
            {
                //创建登录服务器连接
                IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalConfigComponent.Instance.GlobalProto.Address);
                ETModel.Session session = Game.Scene.ModelScene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
                sessionWrap = ComponentFactory.Create<Session, ETModel.Session>(session);

                //发送注册请求
                prompt.text = "正在注册中....";
                R2C_Register_Ack r2C_Register_Ack = await sessionWrap.Call(new C2R_Register_Req() { Account = account.text, Password = password.text }) as R2C_Register_Ack;
                prompt.text = "";

                if (this.IsDisposed)
                {
                    return;
                }

                if (r2C_Register_Ack.Error == ErrorCode.ERR_AccountAlreadyRegister)
                {
                    prompt.text = "注册失败，账号已被注册";
                    account.text = "";
                    password.text = "";
                    return;
                }

                //注册成功自动登录
                OnLogin();
            }
            catch (Exception e)
            {
                prompt.text = "注册异常";
                Log.Error(e.ToStr());
            }
            finally
            {
                //断开验证服务器的连接
                sessionWrap?.Dispose();
                //设置注册处理完成状态
                isRegistering = false;
            }
        }
    }
}
