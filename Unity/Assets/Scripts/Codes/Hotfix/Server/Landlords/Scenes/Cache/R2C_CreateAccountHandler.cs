using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Cache)]
        public class R2C_CreateAccountHandler : AMActorRpcHandler<Scene, R2C_CreateAccount, C2R_CreateAccount>
        {
            protected override async ETTask Run(Scene scene, R2C_CreateAccount request, C2R_CreateAccount response, Action reply)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.User, request.Account.GetHashCode()))
                {
                    UserManagerComponent managerComponent = scene.GetComponent<UserManagerComponent>();
                    UserEntity entity = await managerComponent.Get(request.Account, string.Empty);

                    if(entity == null)
                    {
                        DBManagerComponent dbManagerComponent = scene.GetComponent<DBManagerComponent>();
                        DBComponent dbComponent = dbManagerComponent.GetZoneDB(scene.Zone);

                        //初始化账号
                        entity = managerComponent.AddChild<UserEntity>();
                        entity.Account = request.Account;
                        entity.Password = request.Password;
                        entity.AddComponent<AccountComponent, string, long>($"User_{RandomGenerator.RandUInt32()}", 10000);
                        entity.AddComponent<FriendComponent>();

                        managerComponent.Add(entity);
                        //写入数据库
                        await dbComponent.Save(entity);
                        response.UserId = entity.Id;
                    }
                    else
                    {
                        response.Error = ErrorCode.ERR_AccountAlreadyExist;
                    }

                    reply();
                }
            }
        }
    }
}
