using UnityEngine;
using UnityEngine.UI;
using ETModel;
using System;

namespace ETHotfix
{
    [ObjectSystem]
    public class LandlordsEndComponentAwakeSystem : AwakeSystem<LandlordsEndComponent, bool>
    {
        public override void Awake(LandlordsEndComponent self, bool isWin)
        {
            self.Awake(isWin);
        }
    }

    public class LandlordsEndComponent : Component
    {
        //玩家信息面板预设名称
        public const string CONTENT_NAME = "Content";

        private GameObject contentPrefab;
        private GameObject gamerContent;

        public void Awake(bool isWin)
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.LoadBundle($"{CONTENT_NAME}.unity3d");

            if (isWin)
            {
                rc.Get<GameObject>("Lose").SetActive(false);
            }
            else
            {
                rc.Get<GameObject>("Win").SetActive(false);
            }

            gamerContent = rc.Get<GameObject>("GamerContent");
            Button continueButton = rc.Get<GameObject>("ContinueButton").GetComponent<Button>();
            continueButton.onClick.Add(OnContinue);
            contentPrefab = (GameObject)resourcesComponent.GetAsset($"{CONTENT_NAME}.unity3d", CONTENT_NAME);
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent?.UnloadBundle($"{CONTENT_NAME}.unity3d");
            resourcesComponent?.UnloadBundle($"{UIType.LandlordsEnd}.unity3d");
        }

        /// <summary>
        /// 创建玩家结算信息
        /// </summary>
        /// <returns></returns>
        public GameObject CreateGamerContent(Gamer gamer, Identity winnerIdentity, long baseScore, int multiples, long score)
        {
            GameObject newContent = UnityEngine.Object.Instantiate(contentPrefab);
            newContent.transform.SetParent(gamerContent.transform, false);

            Identity gamerIdentity = gamer.GetComponent<HandCardsComponent>().AccessIdentity;
            Sprite identitySprite = CardHelper.GetCardSprite($"Identity_{Enum.GetName(typeof(Identity), gamerIdentity)}");
            newContent.Get<GameObject>("Identity").GetComponent<Image>().sprite = identitySprite;

            string nickName = gamer.GetComponent<GamerUIComponent>().NickName;
            Text nickNameText = newContent.Get<GameObject>("NickName").GetComponent<Text>();
            Text baseScoreText = newContent.Get<GameObject>("BaseScore").GetComponent<Text>();
            Text multiplesText = newContent.Get<GameObject>("Multiples").GetComponent<Text>();
            Text scoreText = newContent.Get<GameObject>("Score").GetComponent<Text>();
            nickNameText.text = nickName;
            baseScoreText.text = baseScore.ToString();
            multiplesText.text = multiples.ToString();
            scoreText.text = score.ToString();

            if (gamer.UserID == this.GetParent<UI>().GetParent<UI>().GetComponent<GamerComponent>().LocalGamer.UserID)
            {
                nickNameText.color = Color.red;
                baseScoreText.color = Color.red;
                multiplesText.color = Color.red;
                scoreText.color = Color.red;
            }

            return newContent;
        }

        /// <summary>
        /// 继续游戏
        /// </summary>
        private void OnContinue()
        {
            UI entity = this.GetParent<UI>();
            UI parent = (UI)entity.Parent;
            parent.GameObject.Get<GameObject>("ReadyButton").SetActive(true);
            parent.Remove(entity.Name);
        }
    }
}
