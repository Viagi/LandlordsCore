using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Cache)]
        public class ChangeUserRequestHandler : AMActorRpcHandler<Scene, ChangeUserRequest, ChangeUserResponse>
        {
            protected override async ETTask Run(Scene scene, ChangeUserRequest request, ChangeUserResponse response, Action reply)
            {
                UserEntity userEntity = scene.GetComponent<UserManagerComponent>().GetChild<UserEntity>(request.UserId);

                if(userEntity != null)
                {
                    ETTask<bool> saver = ETTask<bool>.Create(true);
                    userEntity.GetComponent<ObjectWait>().Notify(new Wait_Save() { Message = request, Task = saver });
                    await saver;
                }

                reply();
            }
        }
    }
}
