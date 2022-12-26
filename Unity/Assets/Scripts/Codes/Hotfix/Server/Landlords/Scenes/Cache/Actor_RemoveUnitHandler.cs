using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Cache)]
        public class Actor_RemoveUnitHandler : AMActorHandler<Scene, Actor_RemoveUnit>
        {
            protected override async ETTask Run(Scene scene, Actor_RemoveUnit message)
            {
                OnlineUnitEntity unit = scene.GetChild<OnlineUnitEntity>(message.UserId);
                if (unit != null)
                {
                    switch ((SceneType)message.SceneType)
                    {
                        case SceneType.Gate:
                            if (unit.PlayerId == message.UnitId) unit.PlayerId = 0;
                            break;
                        case SceneType.Lobby:
                            if(unit.LobbyId == message.UnitId) unit.LobbyId = 0;
                            break;
                        case SceneType.Friend:
                            if (unit.FriendId == message.UnitId) unit.FriendId = 0;
                            break;
                        case SceneType.Match:
                            if (unit.MatchId == message.UnitId) unit.MatchId = 0;
                            break;
                        case SceneType.Room:
                            if (unit.RoomId == message.UnitId) unit.RoomId = 0;
                            break;
                    }
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
