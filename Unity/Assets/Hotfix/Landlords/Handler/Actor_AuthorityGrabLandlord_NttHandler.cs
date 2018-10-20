using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_AuthorityGrabLandlord_NttHandler : AMHandler<Actor_AuthorityGrabLandlord_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_AuthorityGrabLandlord_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();

            if (message.UserID == gamerComponent.LocalGamer.UserID)
            {
                //显示抢地主交互
                uiRoom.GetComponent<LandlordsRoomComponent>().Interaction.StartGrab();
            }
        }
    }
}
