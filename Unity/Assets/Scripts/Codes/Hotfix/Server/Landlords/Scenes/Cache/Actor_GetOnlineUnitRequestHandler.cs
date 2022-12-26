using ET.Landlords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Cache)]
        public class Actor_GetOnlineUnitRequestHandler : AMActorRpcHandler<Scene, Actor_GetOnlineUnitRequest, Actor_GetOnlineUnitResponse>
        {
            protected override async ETTask Run(Scene scene, Actor_GetOnlineUnitRequest request, Actor_GetOnlineUnitResponse response, Action reply)
            {
                OnlineUnitEntity unit = scene.GetChild<OnlineUnitEntity>(request.UserId);

                if(unit != null)
                {
                    response.Entity = MongoHelper.Serialize(unit);
                }

                reply();
                await ETTask.CompletedTask;
            }
        }
    }
}
