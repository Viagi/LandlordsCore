using UnityEngine;
using ETModel;
using UnityEngine.UI;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerReconnect_NttHandler : AMHandler<Actor_GamerReconnect_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerReconnect_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();

            uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(false);
            foreach (GamerState gamerState in message.GamersState)
            {
                Gamer gamer = gamerComponent.Get(gamerState.UserID);
                HandCardsComponent gamerHandCards = gamer.GetComponent<HandCardsComponent>();
                GamerUIComponent gamerUI = gamer.GetComponent<GamerUIComponent>();
                Identity gamerIdentity = gamerState.UserIdentity;
                gamerHandCards.AccessIdentity = gamerIdentity;
                gamerUI.SetIdentity(gamerIdentity);
                //初始化出牌
                if (message.UserId == gamer.UserID && gamerIdentity != Identity.None)
                {
                    if (message.Cards != null)
                    {
                        gamerHandCards.PopCards(message.Cards);
                    }
                }
                else if (message.LordCards == null && gamerState.GrabLandlordState)
                {
                    gamer.GetComponent<GamerUIComponent>().SetGrab(gamerState.GrabLandlordState);
                }
            }

            //初始化界面
            LandlordsRoomComponent uiRoomComponent = uiRoom.GetComponent<LandlordsRoomComponent>();
            uiRoomComponent.SetMultiples(message.Multiples);
            //当抢完地主时才能显示托管按钮
            if (message.LordCards != null)
            {
                uiRoomComponent.Interaction.GameStart();
            }

            //初始化地主牌
            if (message.LordCards != null)
            {
                GameObject lordPokers = uiRoom.GameObject.Get<GameObject>("Desk").Get<GameObject>("LordPokers");
                for (int i = 0; i < lordPokers.transform.childCount; i++)
                {
                    Sprite lordCardSprite = CardHelper.GetCardSprite(message.LordCards[i].GetName());
                    lordPokers.transform.GetChild(i).GetComponent<Image>().sprite = lordCardSprite;
                }
            }
        }
    }
}
