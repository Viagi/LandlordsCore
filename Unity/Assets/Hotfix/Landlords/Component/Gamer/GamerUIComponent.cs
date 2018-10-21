using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class GamerUIComponentStartSystem : StartSystem<GamerUIComponent>
    {
        public override void Start(GamerUIComponent self)
        {
            self.Start();
        }
    }

    /// <summary>
    /// 玩家UI组件
    /// </summary>
    public class GamerUIComponent : Component
    {
        //UI面板
        public GameObject Panel { get; private set; }

        //玩家昵称
        public string NickName { get { return name.text; } }

        private Image headPhoto;
        private Text prompt;
        private Text name;
        private Text money;

        public void Start()
        {
            if (this.GetParent<Gamer>().IsReady)
            {
                SetReady();
            }
        }

        /// <summary>
        /// 重置面板
        /// </summary>
        public void ResetPanel()
        {
            ResetPrompt();
            this.headPhoto.gameObject.SetActive(false);
            this.name.text = "空位";
            this.money.text = "";

            this.Panel = null;
            this.prompt = null;
            this.name = null;
            this.money = null;
            this.headPhoto = null;
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="panel"></param>
        public void SetPanel(GameObject panel)
        {
            this.Panel = panel;

            //绑定关联
            this.prompt = this.Panel.Get<GameObject>("Prompt").GetComponent<Text>();
            this.name = this.Panel.Get<GameObject>("Name").GetComponent<Text>();
            this.money = this.Panel.Get<GameObject>("Money").GetComponent<Text>();
            this.headPhoto = this.Panel.Get<GameObject>("HeadPhoto").GetComponent<Image>();

            UpdatePanel();
        }

        /// <summary>
        /// 更新面板
        /// </summary>
        public void UpdatePanel()
        {
            if (this.Panel != null)
            {
                SetUserInfo();
                headPhoto.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 设置玩家身份
        /// </summary>
        /// <param name="identity"></param>
        public void SetIdentity(Identity identity)
        {
            if (identity == Identity.None)
                return;

            string spriteName = $"Identity_{Enum.GetName(typeof(Identity), identity)}";
            Sprite headSprite = CardHelper.GetCardSprite(spriteName);
            headPhoto.sprite = headSprite;
            headPhoto.gameObject.SetActive(true);
        }

        /// <summary>
        /// 玩家准备
        /// </summary>
        public void SetReady()
        {
            prompt.text = "准备！";
        }

        /// <summary>
        /// 出牌错误
        /// </summary>
        public void SetPlayCardsError()
        {
            prompt.text = "您出的牌不符合规则！";
        }

        /// <summary>
        /// 玩家不出
        /// </summary>
        public void SetDiscard()
        {
            prompt.text = "不出";
        }

        /// <summary>
        /// 玩家抢地主
        /// </summary>
        public void SetGrab(GrabLandlordState state)
        {
            switch (state)
            {
                case GrabLandlordState.Not:
                    break;
                case GrabLandlordState.Grab:
                    prompt.text = "抢地主";
                    break;
                case GrabLandlordState.UnGrab:
                    prompt.text = "不抢";
                    break;
            }
        }

        /// <summary>
        /// 重置提示
        /// </summary>
        public void ResetPrompt()
        {
            prompt.text = "";
        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        public void GameStart()
        {
            ResetPrompt();
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="id"></param>
        private async void SetUserInfo()
        {
            G2C_GetUserInfo_Ack g2C_GetUserInfo_Ack = await SessionComponent.Instance.Session.Call(new C2G_GetUserInfo_Req() { UserID = this.GetParent<Gamer>().UserID }) as G2C_GetUserInfo_Ack;

            if (this.Panel != null)
            {
                name.text = g2C_GetUserInfo_Ack.NickName;
                money.text = g2C_GetUserInfo_Ack.Money.ToString();
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            //重置玩家UI
            ResetPanel();
        }
    }
}
