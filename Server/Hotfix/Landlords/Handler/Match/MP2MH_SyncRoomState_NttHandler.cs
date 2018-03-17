using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Match)]
    public class MP2MH_SyncRoomState_NttHandler : AMHandler<MP2MH_SyncRoomState_Ntt>
    {
        protected override void Run(Session session, MP2MH_SyncRoomState_Ntt message)
        {
            MatchRoomComponent matchRoomComponent = Game.Scene.GetComponent<MatchRoomComponent>();
            Room room = matchRoomComponent.Get(message.RoomID);

            //同步房间状态
            switch (message.State)
            {
                case RoomState.Game:
                    matchRoomComponent.RoomStartGame(room.Id);
                    Log.Info($"房间{room.Id}切换为游戏状态");
                    break;
                case RoomState.Ready:
                    Log.Info($"房间{room.Id}切换为准备状态");
                    matchRoomComponent.RoomEndGame(room.Id);
                    break;
            }
        }
    }
}
