using UnityEngine;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerEnterRoom_NttHandler : AMHandler<Actor_GamerEnterRoom_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerEnterRoom_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent landlordsRoomComponent = uiRoom.GetComponent<LandlordsRoomComponent>();
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();

            //从匹配状态中切换为准备状态
            if (landlordsRoomComponent.Matching)
            {
                landlordsRoomComponent.Matching = false;
                GameObject matchPrompt = uiRoom.GameObject.Get<GameObject>("MatchPrompt");
                if (matchPrompt.activeSelf)
                {
                    matchPrompt.SetActive(false);
                    uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(true);
                }
            }

            int localGamerIndex = new List<GamerInfo>(message.Gamers).FindIndex(info => info.UserID == gamerComponent.LocalGamer.UserID);
            //添加未显示玩家
            for (int i = 0; i < message.Gamers.Count; i++)
            {
                GamerInfo gamerInfo = message.Gamers[i];
                if (gamerInfo.UserID == 0)
                    continue;
                if (gamerComponent.Get(gamerInfo.UserID) == null)
                {
                    Gamer gamer = GamerFactory.Create(gamerInfo.UserID, gamerInfo.IsReady);
                    if ((localGamerIndex + 1) % 3 == i)
                    {
                        //玩家在本地玩家右边
                        landlordsRoomComponent.AddGamer(gamer, 2);
                    }
                    else
                    {
                        //玩家在本地玩家左边
                        landlordsRoomComponent.AddGamer(gamer, 0);
                    }
                }
            }
        }
    }
}
