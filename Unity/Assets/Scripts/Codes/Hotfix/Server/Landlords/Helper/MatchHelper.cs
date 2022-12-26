using System;

namespace ET.Server
{
    namespace Landlords
    {
        public static class MatchHelper
        {
            public static async ETTask<IActorResponse> Call(IActorRequest request)
            {
                StartSceneConfig config = StartSceneConfigCategory.Instance.MatchServer;

                if (config != null)
                {
                    IActorResponse response = await ActorMessageSenderComponent.Instance.Call(config.InstanceId, request);
                    return response;
                }
                else
                {
                    throw new Exception("Please add match server in StartSceneConfig!");
                }
            }

            public static void Send(IActorMessage message)
            {
                StartSceneConfig config = StartSceneConfigCategory.Instance.MatchServer;

                if (config != null)
                {
                    ActorMessageSenderComponent.Instance.Send(config.InstanceId, message);
                }
                else
                {
                    throw new Exception("Please add match server in StartSceneConfig!");
                }
            }
        }
    }
}
