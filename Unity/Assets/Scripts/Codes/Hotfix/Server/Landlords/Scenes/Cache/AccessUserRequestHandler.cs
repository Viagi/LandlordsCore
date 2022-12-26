using ET.Landlords;
using System;
using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Cache)]
        public class AccessUserRequestHandler : AMActorRpcHandler<Scene, AccessUserRequest, AccessUserResponse>
        {
            protected override async ETTask Run(Scene scene, AccessUserRequest request, AccessUserResponse response, Action reply)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.User, request.UserId))
                {
                    UserEntity userEntity = await scene.GetComponent<UserManagerComponent>().Get(request.UserId);

                    //读
                    if (userEntity != null)
                    {
                        response.Components = new List<byte[]>();
                        foreach (Entity component in userEntity.Components.Values)
                        {
                            response.Components.Add(MongoHelper.Serialize(component));
                        }
                    }

                    reply();

                    //写
                    if (userEntity != null && request.ReadWrite)
                    {
                        Wait_Save wait_Save = await userEntity.GetComponent<ObjectWait>().Wait<Wait_Save>();

                        try
                        {
                            foreach (byte[] bytes in wait_Save.Message.Components)
                            {
                                Entity component = MongoHelper.Deserialize<Entity>(bytes);
                                userEntity.RemoveComponent(component.GetType());
                                userEntity.AddComponent(component);
                            }

                            if (wait_Save.Message.Save)
                            {
                                await userEntity.SaveDB();
                            }
                            else
                            {
                                userEntity.DirtySaveDB();
                            }

                            wait_Save.Task.SetResult(true);
                        }
                        catch(Exception e)
                        {
                            wait_Save.Task.SetException(e);
                        }
                    }
                }
            }
        }
    }
}
