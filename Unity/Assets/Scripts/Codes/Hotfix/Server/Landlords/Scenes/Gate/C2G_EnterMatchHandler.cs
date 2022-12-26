using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Gate)]
        public class C2G_EnterMatchHandler : AMRpcHandler<C2G_EnterMatch, G2C_EnterMatch>
        {
            protected override async ETTask Run(Session session, C2G_EnterMatch request, G2C_EnterMatch response, Action reply)
            {
                Player player = session.GetPlayer();
                LandlordsComponent landlordsComponent = player?.GetComponent<LandlordsComponent>();

                if (landlordsComponent != null && landlordsComponent.MatchId == 0)
                {
                    M2G_CreateMatchUnit m2G_CreateMatch = (M2G_CreateMatchUnit)await MatchHelper.Call(new G2M_CreateMatchUnit() { UserId = landlordsComponent.UserId, Entity = MongoHelper.Serialize(player.GetComponent<UnitGateComponent>()) });

                    if (m2G_CreateMatch.Error > 0)
                    {
                        response.Error = m2G_CreateMatch.Error;
                    }
                    else
                    {
                        landlordsComponent.MatchId = m2G_CreateMatch.UnitId;
                    }
                }

                reply();
            }
        }
    }
}
