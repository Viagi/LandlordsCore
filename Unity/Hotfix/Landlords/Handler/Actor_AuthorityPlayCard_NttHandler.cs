using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_AuthorityPlayCard_NttHandler : AMHandler<Actor_AuthorityPlayCard_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_AuthorityPlayCard_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.UserID);
            if (gamer != null)
            {
                //重置玩家提示
                gamer.GetComponent<GamerUIComponent>().ResetPrompt();

                //当玩家为先手，清空出牌
                if (message.IsFirst)
                {
                    gamer.GetComponent<HandCardsComponent>().ClearPlayCards();
                }

                //显示出牌按钮
                if (gamer.UserID == gamerComponent.LocalGamer.UserID)
                {
                    LandlordsInteractionComponent interaction = uiRoom.GetComponent<LandlordsRoomComponent>().Interaction;
                    interaction.IsFirst = message.IsFirst;
                    interaction.StartPlay();
                }
            }
        }
    }
}
