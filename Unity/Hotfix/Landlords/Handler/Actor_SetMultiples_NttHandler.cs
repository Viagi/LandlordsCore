using System;
using System.Collections.Generic;
using Model;

namespace Hotfix
{
    [MessageHandler]
    public class Actor_SetMultiples_NttHandler : AMHandler<Actor_SetMultiples_Ntt>
    {
        protected override void Run(Session session, Actor_SetMultiples_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            uiRoom.GetComponent<LandlordsRoomComponent>().SetMultiples(message.Multiples);
        }
    }
}
