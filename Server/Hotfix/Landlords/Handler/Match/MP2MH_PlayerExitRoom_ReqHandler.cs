using System;
using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Match)]
    public class MP2MH_PlayerExitRoom_NttHandler : AMHandler<MP2MH_PlayerExitRoom_Ntt>
    {
        protected override void Run(Session session, MP2MH_PlayerExitRoom_Ntt message)
        {
            MatchRoomComponent matchRoomComponent = Game.Scene.GetComponent<MatchRoomComponent>();
            Room room = matchRoomComponent.Get(message.RoomID);

            //移除玩家对象
            Gamer gamer = room.Remove(message.UserID);
            Game.Scene.GetComponent<MatchComponent>().Playing.Remove(gamer.UserID);
            gamer.Dispose();

            if (room.Count == 0)
            {
                //当房间中没有玩家时回收
                matchRoomComponent.Recycle(room.Id);
                Log.Info($"回收房间{room.Id}");
            }
        }
    }
}
