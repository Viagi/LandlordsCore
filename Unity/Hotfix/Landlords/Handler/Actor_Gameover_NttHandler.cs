using System;
using System.Collections.Generic;
using Model;

namespace Hotfix
{
    [MessageHandler]
    public class Actor_Gameover_NttHandler : AMHandler<Actor_Gameover_Ntt>
    {
        protected override void Run(Session session, Actor_Gameover_Ntt message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Identity localGamerIdentity = gamerComponent.LocalGamer.GetComponent<HandCardsComponent>().AccessIdentity;
            UI uiEndPanel = LandlordsEndFactory.Create(UIType.LandlordsEnd, uiRoom, message.Winner == localGamerIdentity);
            LandlordsEndComponent landlordsEndComponent = uiEndPanel.GetComponent<LandlordsEndComponent>();
            uiRoom.Add(uiEndPanel);

            foreach (var gamer in gamerComponent.GetAll())
            {
                gamer.GetComponent<GamerUIComponent>().UpdatePanel();
                gamer.GetComponent<HandCardsComponent>().Hide();
                landlordsEndComponent.CreateGamerContent(
                    gamer,
                    message.Winner,
                    message.BasePointPerMatch,
                    message.Multiples,
                    message.GamersScore[gamer.UserID]);
            }

            LandlordsRoomComponent landlordsRoomComponent = uiRoom.GetComponent<LandlordsRoomComponent>();
            landlordsRoomComponent.Interaction.Gameover();
            landlordsRoomComponent.ResetMultiples();
        }
    }
}
