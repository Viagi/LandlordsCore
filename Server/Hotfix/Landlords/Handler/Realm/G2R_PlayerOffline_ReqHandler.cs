using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class G2R_PlayerOffline_ReqHandler : AMRpcHandler<G2R_PlayerOffline_Req, R2G_PlayerOffline_Ack>
    {
        protected override void Run(Session session, G2R_PlayerOffline_Req message,Action<R2G_PlayerOffline_Ack> reply)
        {
            R2G_PlayerOffline_Ack response = new R2G_PlayerOffline_Ack();
            try
            {
                //玩家下线
                Game.Scene.GetComponent<OnlineComponent>().Remove(message.UserID);
                Log.Info($"玩家{message.UserID}下线");

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
