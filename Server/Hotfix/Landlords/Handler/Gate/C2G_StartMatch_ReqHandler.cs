using System;
using ETModel;
using System.Net;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_StartMatch_ReqHandler : AMRpcHandler<C2G_StartMatch_Req, G2C_StartMatch_Ack>
    {
        protected override async void Run(Session session, C2G_StartMatch_Req message, Action<G2C_StartMatch_Ack> reply)
        {
            G2C_StartMatch_Ack response = new G2C_StartMatch_Ack();
            try
            {
                //验证Session
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_SignError;
                    reply(response);
                    return;
                }

                User user = session.GetComponent<SessionUserComponent>().User;

                //验证玩家是否符合进入房间要求,默认为100底分局
                RoomConfig roomConfig = RoomHelper.GetConfig(RoomLevel.Lv100);
                UserInfo userInfo = await Game.Scene.GetComponent<DBProxyComponent>().Query<UserInfo>(user.UserID, false);
                if (userInfo.Money < roomConfig.MinThreshold)
                {
                    response.Error = ErrorCode.ERR_UserMoneyLessError;
                    reply(response);
                    return;
                }

                //这里先发送响应，让客户端收到后切换房间界面，否则可能会出现重连消息在切换到房间界面之前发送导致重连异常
                reply(response);

                //向匹配服务器发送匹配请求
                StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
                IPEndPoint matchIPEndPoint = config.MatchConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchIPEndPoint);
                M2G_PlayerEnterMatch_Ack m2G_PlayerEnterMatch_Ack = await matchSession.Call(new G2M_PlayerEnterMatch_Req()
                {
                    PlayerID = user.InstanceId,
                    UserID = user.UserID,
                    SessionID = session.InstanceId,
                }) as M2G_PlayerEnterMatch_Ack;

                user.IsMatching = true;
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
