using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        public static class CacheHelper
        {
            public static async ETTask<IActorResponse> Call(IActorRequest request)
            {
                StartSceneConfig config = StartSceneConfigCategory.Instance.CacheServer;

                if (config != null)
                {
                    IActorResponse response = await ActorMessageSenderComponent.Instance.Call(config.InstanceId, request);
                    return response;
                }
                else
                {
                    throw new Exception("Please add cache server in StartSceneConfig!");
                }
            }

            public static void Send(IActorMessage message)
            {
                StartSceneConfig config = StartSceneConfigCategory.Instance.CacheServer;

                if (config != null)
                {
                    ActorMessageSenderComponent.Instance.Send(config.InstanceId, message);
                }
                else
                {
                    throw new Exception("Please add cache server in StartSceneConfig!");
                }
            }

            public static async ETTask<OnlineUnitEntity> GetOnlineUnit(long userId)
            {
                Actor_GetOnlineUnitResponse getOnlineUnitResponse = (Actor_GetOnlineUnitResponse)await Call(new Actor_GetOnlineUnitRequest() { UserId = userId });

                OnlineUnitEntity unit = null;
                if (getOnlineUnitResponse.Entity != null)
                {
                    unit = MongoHelper.Deserialize<OnlineUnitEntity>(getOnlineUnitResponse.Entity);
                }

                return unit;
            }

            public static async ETTask AddOnlineUnit(long userId, long gateId, long gateKey)
            {
                await Call(new R2C_CreateOnlineUnit() { UserId = userId, GateId = gateId, GateKey = gateKey });
            }
        }
    }
}
