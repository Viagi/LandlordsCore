using System;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_Login_ReqHandler : AMRpcHandler<C2R_Login_Req, R2C_Login_Ack>
    {
        protected override async void Run(Session session, C2R_Login_Req message, Action<R2C_Login_Ack> reply)
        {
            R2C_Login_Ack response = new R2C_Login_Ack();

            try
            {
                //数据库操作对象
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

                Log.Info($"登录请求：{{Account:'{message.Account}',Password:'{message.Password}'}}");
                //验证账号密码是否正确
                List<ComponentWithId> result = await dbProxy.Query<AccountInfo>(_account => _account.Account == message.Account && _account.Password == message.Password);
                if (result.Count == 0)
                {
                    response.Error = ErrorCode.ERR_LoginError;
                    reply(response);
                    return;
                }

                AccountInfo account = result[0] as AccountInfo;
                Log.Info($"账号登录成功{MongoHelper.ToJson(account)}");

                //将已在线玩家踢下线
                await RealmHelper.KickOutPlayer(account.Id);

                //随机分配网关服务器
                StartConfig gateConfig = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(gateConfig.GetComponent<InnerConfig>().IPEndPoint);

                //请求登录Gate服务器密匙
                G2R_GetLoginKey_Ack getLoginKey_Ack = await gateSession.Call(new R2G_GetLoginKey_Req() { UserID = account.Id }) as G2R_GetLoginKey_Ack;

                response.Key = getLoginKey_Ack.Key;
                response.Address = gateConfig.GetComponent<OuterConfig>().Address2;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
