using System;
using System.Collections.Generic;
using System.Linq;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_Trusteeship_NttHandler : AMHandler<Actor_Trusteeship_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_Trusteeship_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.UserID);
            if (gamer.UserID == ClientComponent.Instance.LocalPlayer.UserID)
            {
                LandlordsInteractionComponent interaction = uiRoom.GetComponent<LandlordsRoomComponent>().Interaction;
                interaction.isTrusteeship = message.IsTrusteeship;
            }
        }
    }
}
