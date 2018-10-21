using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_GamerReady_NttHandler : AMActorHandler<Gamer, Actor_GamerReady_Ntt>
    {
        protected override void Run(Gamer gamer, Actor_GamerReady_Ntt message)
        {
            gamer.IsReady = true;

            Room room = Game.Scene.GetComponent<RoomComponent>().Get(gamer.RoomID);

            //转发玩家准备消息
            Actor_GamerReady_Ntt transpond = new Actor_GamerReady_Ntt();
            transpond.UserID = gamer.UserID;
            room.Broadcast(transpond);
            Log.Info($"玩家{gamer.UserID}准备");

            //检测开始游戏
            room.GetComponent<GameControllerComponent>().ReadyStartGame();
        }
    }
}
