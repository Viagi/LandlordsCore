using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_Gameover_NttHandler : AMHandler<Actor_Gameover_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_Gameover_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Identity localGamerIdentity = gamerComponent.LocalGamer.GetComponent<HandCardsComponent>().AccessIdentity;
            UI uiEndPanel = LandlordsEndFactory.Create(UIType.LandlordsEnd, uiRoom, (Identity)message.Winner == localGamerIdentity);
            LandlordsEndComponent landlordsEndComponent = uiEndPanel.GetComponent<LandlordsEndComponent>();

            foreach (GamerScore gamerScore in message.GamersScore)
            {
                Gamer gamer = uiRoom.GetComponent<GamerComponent>().Get(gamerScore.UserID);
                gamer.GetComponent<GamerUIComponent>().UpdatePanel();
                gamer.GetComponent<HandCardsComponent>().Hide();
                landlordsEndComponent.CreateGamerContent(
                    gamer,
                    (Identity)message.Winner,
                    message.BasePointPerMatch,
                    message.Multiples,
                    gamerScore.Score);
            }

            LandlordsRoomComponent landlordsRoomComponent = uiRoom.GetComponent<LandlordsRoomComponent>();
            landlordsRoomComponent.Interaction.Gameover();
            landlordsRoomComponent.ResetMultiples();
        }
    }
}
