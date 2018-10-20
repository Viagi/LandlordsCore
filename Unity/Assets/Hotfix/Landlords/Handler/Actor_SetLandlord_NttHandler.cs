using System;
using System.Collections.Generic;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_SetLandlord_NttHandler : AMHandler<Actor_SetLandlord_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_SetLandlord_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.UserID);

            if (gamer != null)
            {
                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                if (gamer.UserID == gamerComponent.LocalGamer.UserID)
                {
                    //本地玩家添加手牌
                    handCards.AddCards(message.LordCards);
                }
                else
                {
                    //其他玩家设置手牌数
                    handCards.SetHandCardsNum(20);
                }
            }

            foreach (var _gamer in gamerComponent.GetAll())
            {
                HandCardsComponent handCardsComponent = _gamer.GetComponent<HandCardsComponent>();
                GamerUIComponent gamerUIComponent = _gamer.GetComponent<GamerUIComponent>();
                if (_gamer.UserID == message.UserID)
                {
                    handCardsComponent.AccessIdentity = Identity.Landlord;
                    gamerUIComponent.SetIdentity(Identity.Landlord);
                }
                else
                {
                    handCardsComponent.AccessIdentity = Identity.Farmer;
                    gamerUIComponent.SetIdentity(Identity.Farmer);
                }
            }

            //重置玩家UI提示
            foreach (var _gamer in gamerComponent.GetAll())
            {
                _gamer.GetComponent<GamerUIComponent>().ResetPrompt();
            }

            //切换地主牌精灵
            GameObject lordPokers = uiRoom.GameObject.Get<GameObject>("Desk").Get<GameObject>("LordPokers");
            for (int i = 0; i < lordPokers.transform.childCount; i++)
            {
                Sprite lordCardSprite = CardHelper.GetCardSprite(message.LordCards[i].GetName());
                lordPokers.transform.GetChild(i).GetComponent<Image>().sprite = lordCardSprite;
            }

            //显示切换游戏模式按钮
            uiRoom.GetComponent<LandlordsRoomComponent>().Interaction.GameStart();
        }
    }
}
