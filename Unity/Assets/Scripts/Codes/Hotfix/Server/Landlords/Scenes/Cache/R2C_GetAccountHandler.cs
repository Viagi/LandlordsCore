using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Cache)]
        public class R2C_GetAccountHandler : AMActorRpcHandler<Scene, R2C_GetAccount, C2R_GetAccount>
        {
            protected override async ETTask Run(Scene scene, R2C_GetAccount request, C2R_GetAccount response, Action reply)
            {
                UserEntity account = await scene.GetComponent<UserManagerComponent>().Get(request.Account, request.Password);

                if (account != null)
                {
                    response.UserId = account.Id;
                }
                else
                {
                    response.Error = ErrorCode.ERR_AccountOrPasswordError;
                }

                reply();
            }
        }
    }
}
