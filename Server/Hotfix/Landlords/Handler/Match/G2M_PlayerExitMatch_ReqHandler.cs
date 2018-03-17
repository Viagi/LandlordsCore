using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Match)]
    public class G2M_PlayerExitMatch_ReqHandler : AMRpcHandler<G2M_PlayerExitMatch_Req, M2G_PlayerExitMatch_Ack>
    {
        protected override void Run(Session session, G2M_PlayerExitMatch_Req message, Action<M2G_PlayerExitMatch_Ack> reply)
        {
            M2G_PlayerExitMatch_Ack response = new M2G_PlayerExitMatch_Ack();
            try
            {
                Matcher matcher = Game.Scene.GetComponent<MatcherComponent>().Remove(message.UserID);
                matcher?.Dispose();
                Log.Info($"玩家{message.UserID}退出匹配队列");

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
