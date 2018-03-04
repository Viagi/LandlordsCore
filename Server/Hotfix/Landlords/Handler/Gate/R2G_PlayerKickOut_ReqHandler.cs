using System;
using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Gate)]
    public class R2G_PlayerKickOut_ReqHandler : AMRpcHandler<R2G_PlayerKickOut_Req, G2R_PlayerKickOut_Ack>
    {
        protected override async void Run(Session session, R2G_PlayerKickOut_Req message, Action<G2R_PlayerKickOut_Ack> reply)
        {
            G2R_PlayerKickOut_Ack response = new G2R_PlayerKickOut_Ack();
            try
            {
                User user = Game.Scene.GetComponent<UserComponent>().Get(message.UserID);
                long userSessionId = user.GetComponent<UnitGateComponent>().GateSessionId;
                Session userSession = Game.Scene.GetComponent<NetOuterComponent>().Get(userSessionId);
                userSession.Send(new G2C_PlayerDisconnect_Ntt());

                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);
                Log.Info($"将玩家{message.UserID}连接断开");
                userSession.Dispose();

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
