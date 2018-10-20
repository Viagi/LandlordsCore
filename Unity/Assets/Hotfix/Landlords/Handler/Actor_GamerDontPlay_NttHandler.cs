using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerDontPlay_NttHandler : AMHandler<Actor_GamerDontPlay_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerDontPlay_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.UserID);
            if (gamer != null)
            {
                if (gamer.UserID == gamerComponent.LocalGamer.UserID)
                {
                    uiRoom.GetComponent<LandlordsRoomComponent>().Interaction.EndPlay();
                }
                gamer.GetComponent<HandCardsComponent>().ClearPlayCards();
                gamer.GetComponent<GamerUIComponent>().SetDiscard();
            }
        }
    }
}
