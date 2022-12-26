using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Gate)]
        public class C2G_ReturnLobbyHandler : AMRpcHandler<C2G_ReturnLobby, G2C_ReturnLobby>
        {
            protected override async ETTask Run(Session session, C2G_ReturnLobby request, G2C_ReturnLobby response, Action reply)
            {
                Player player = session.GetPlayer();
                LandlordsComponent landlordsComponent = player?.GetComponent<LandlordsComponent>();

                if (landlordsComponent != null)
                {
                    if (landlordsComponent.MatchId != 0)
                    {
                        await MessageHelper.CallActor(landlordsComponent.MatchId, new Actor_RemoveMatchUnitRequest());
                        landlordsComponent.MatchId = 0;
                    }
                    if (landlordsComponent.RoomId != 0)
                    {
                        await MessageHelper.CallActor(landlordsComponent.RoomId, new Actor_ExitRoomRequest());
                    }
                }

                reply();
            }
        }
    }
}
