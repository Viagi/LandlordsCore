using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerPlayCard_NttHandler : AMHandler<Actor_GamerPlayCard_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerPlayCard_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.UserID);
            if (gamer != null)
            {
                gamer.GetComponent<GamerUIComponent>().ResetPrompt();

                if (gamer.UserID == gamerComponent.LocalGamer.UserID)
                {
                    LandlordsInteractionComponent interaction = uiRoom.GetComponent<LandlordsRoomComponent>().Interaction;
                    interaction.Clear();
                    interaction.EndPlay();
                }

                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                handCards.PopCards(message.Cards);
            }
        }
    }
}
