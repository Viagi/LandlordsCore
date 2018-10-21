using System;
using ETModel;
using System.Net;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_LoginGate_ReqHandler : AMRpcHandler<C2G_LoginGate_Req, G2C_LoginGate_Ack>
    {
        protected override async void Run(Session session, C2G_LoginGate_Req message, Action<G2C_LoginGate_Ack> reply)
        {
            G2C_LoginGate_Ack response = new G2C_LoginGate_Ack();
            try
            {
                LandlordsGateSessionKeyComponent landlordsGateSessionKeyComponent = Game.Scene.GetComponent<LandlordsGateSessionKeyComponent>();
                long userId = landlordsGateSessionKeyComponent.Get(message.Key);

                //验证登录Key是否正确
                if (userId == 0)
                {
                    response.Error = ErrorCode.ERR_ConnectGateKeyError;
                    reply(response);
                    return;
                }

                //Key过期
                landlordsGateSessionKeyComponent.Remove(message.Key);


                //创建User对象
                User user = UserFactory.Create(userId, session.InstanceId);
                await user.AddComponent<MailBoxComponent>().AddLocation();

                //添加User对象关联到Session上
                session.AddComponent<SessionUserComponent>().User = user;
                //添加消息转发组件
                await session.AddComponent<MailBoxComponent, string>(ActorInterceptType.GateSession).AddLocation();

                //向登录服务器发送玩家上线消息
                StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
                IPEndPoint realmIPEndPoint = config.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmIPEndPoint);
                await realmSession.Call(new G2R_PlayerOnline_Req() { UserID = userId, GateAppID = config.StartConfig.AppId });

                response.PlayerID = user.InstanceId;
                response.UserID = user.UserID;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
