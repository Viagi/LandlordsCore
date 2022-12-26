using System;
using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Realm)]
        public class C2R_LoginHandler : AMRpcHandler<ET.Landlords.C2R_Login, ET.Landlords.R2C_Login>
        {
            protected override async ETTask Run(Session session, ET.Landlords.C2R_Login request, ET.Landlords.R2C_Login response, Action reply)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Login, request.Account.GetHashCode()))
                {
                    C2R_GetAccount c2R_GetAccount = (C2R_GetAccount)await CacheHelper.Call(new R2C_GetAccount()
                    {
                        Account = request.Account,
                        Password = request.Password
                    });

                    if (c2R_GetAccount.Error > 0)
                    {
                        //登录失败
                        response.Error = c2R_GetAccount.Error;
                        reply();
                        return;
                    }

                    long userId = c2R_GetAccount.UserId;
                    OnlineUnitEntity onlineUnit = await CacheHelper.GetOnlineUnit(userId);
                    if (onlineUnit != null && onlineUnit.PlayerId != 0)
                    {
                        //通知下线
                        MessageHelper.SendActor(onlineUnit.PlayerId, new Actor_Offline());
                    }

                    // 随机分配一个Gate
                    StartSceneConfig config = RealmGateAddressHelper.GetGate(session.DomainZone());
                    Log.Debug($"gate address: {MongoHelper.ToJson(config)}");

                    // 向gate请求一个key,客户端可以拿着这个key连接gate
                    ET.Landlords.G2R_GetLoginKey g2R_GetLoginKey = (ET.Landlords.G2R_GetLoginKey)await ActorMessageSenderComponent.Instance.Call(
                        config.InstanceId, new ET.Landlords.R2G_GetLoginKey() { UserId = userId });

                    await CacheHelper.AddOnlineUnit(userId, g2R_GetLoginKey.GateId, g2R_GetLoginKey.Key);

                    response.Address = config.InnerIPOutPort.ToString();
                    response.Key = g2R_GetLoginKey.Key;
                    reply();
                }
            }
        }
    }
}
