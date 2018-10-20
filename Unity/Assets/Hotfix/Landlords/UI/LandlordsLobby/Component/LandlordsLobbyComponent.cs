using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class LandlordsLobbyComponentAwakeSystem : AwakeSystem<LandlordsLobbyComponent>
    {
        public override void Awake(LandlordsLobbyComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 大厅界面组件
    /// </summary>
    public class LandlordsLobbyComponent : Component
    {
        public void Awake()
        {
            Init();
        }

        /// <summary>
        /// 开始匹配按钮事件
        /// </summary>
        public async void OnStartMatch()
        {
            try
            {
                //发送开始匹配消息
                C2G_StartMatch_Req c2G_StartMatch_Req = new C2G_StartMatch_Req();
                G2C_StartMatch_Ack g2C_StartMatch_Ack = await SessionComponent.Instance.Session.Call(c2G_StartMatch_Req) as G2C_StartMatch_Ack;

                if (g2C_StartMatch_Ack.Error == ErrorCode.ERR_UserMoneyLessError)
                {
                    Log.Error("余额不足");
                    return;
                }

                //切换到房间界面
                UI room = Game.Scene.GetComponent<UIComponent>().Create(UIType.LandlordsRoom);
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.LandlordsLobby);

                //将房间设为匹配状态
                room.GetComponent<LandlordsRoomComponent>().Matching = true;
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private async void Init()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            //添加事件
            rc.Get<GameObject>("StartMatch").GetComponent<Button>().onClick.Add(OnStartMatch);

            //获取玩家数据
            long userId = ClientComponent.Instance.LocalPlayer.UserID;
            C2G_GetUserInfo_Req c2G_GetUserInfo_Req = new C2G_GetUserInfo_Req() { UserID = userId };
            G2C_GetUserInfo_Ack g2C_GetUserInfo_Ack = await SessionComponent.Instance.Session.Call(c2G_GetUserInfo_Req) as G2C_GetUserInfo_Ack;

            //显示用户信息
            rc.Get<GameObject>("NickName").GetComponent<Text>().text = g2C_GetUserInfo_Ack.NickName;
            rc.Get<GameObject>("Money").GetComponent<Text>().text = g2C_GetUserInfo_Ack.Money.ToString();
        }
    }
}
