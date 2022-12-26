using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Cache)]
        public class Actor_CreateUnitHandler : AMActorHandler<Scene, Actor_CreateUnit>
        {
            protected override async ETTask Run(Scene scene, Actor_CreateUnit message)
            {
                OnlineUnitEntity unit = scene.GetChild<OnlineUnitEntity>(message.UserId);
                if (unit != null)
                {
                    switch ((SceneType)message.SceneType)
                    {
                        case SceneType.Gate:
                            unit.PlayerId = message.UnitId;
                            break;
                        case SceneType.Lobby:
                            unit.LobbyId = message.UnitId;
                            break;
                        case SceneType.Friend:
                            unit.FriendId = message.UnitId;
                            break;
                        case SceneType.Match:
                            unit.MatchId = message.UnitId;
                            break;
                        case SceneType.Room:
                            unit.RoomId = message.UnitId;
                            break;
                    }
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
