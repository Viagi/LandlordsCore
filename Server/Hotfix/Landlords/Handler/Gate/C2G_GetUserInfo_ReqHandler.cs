using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_GetUserInfo_ReqHandler : AMRpcHandler<C2G_GetUserInfo_Req, G2C_GetUserInfo_Ack>
    {
        protected override async void Run(Session session, C2G_GetUserInfo_Req message, Action<G2C_GetUserInfo_Ack> reply)
        {
            G2C_GetUserInfo_Ack response = new G2C_GetUserInfo_Ack();
            try
            {
                //验证Session
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_SignError;
                    reply(response);
                    return;
                }

                //查询用户信息
                DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                UserInfo userInfo = await dbProxyComponent.Query<UserInfo>(message.UserID, false);

                response.NickName = userInfo.NickName;
                response.Wins = userInfo.Wins;
                response.Loses = userInfo.Loses;
                response.Money = userInfo.Money;

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
