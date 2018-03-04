using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Hotfix
{
    [MessageHandler]
    public class Actor_GamerReady_NttHandler : AMHandler<Actor_GamerReady_Ntt>
    {
        protected override void Run(Session session, Actor_GamerReady_Ntt message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.UserID);
            gamer.GetComponent<GamerUIComponent>().SetReady();

            //本地玩家准备,隐藏准备按钮
            if (gamer.UserID == gamerComponent.LocalGamer.UserID)
            {
                uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(false);
            }
        }
    }
}
