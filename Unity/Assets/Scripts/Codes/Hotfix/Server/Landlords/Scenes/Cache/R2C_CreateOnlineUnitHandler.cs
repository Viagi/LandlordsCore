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
        public class R2C_CreateOnlineUnitHandler : AMActorRpcHandler<Scene, R2C_CreateOnlineUnit, C2R_CreateOnlineUnit>
        {
            protected override async ETTask Run(Scene scene, R2C_CreateOnlineUnit request, C2R_CreateOnlineUnit response, Action reply)
            {
                OnlineUnitEntity unit = scene.GetChild<OnlineUnitEntity>(request.UserId);
                if (unit == null)
                {
                    scene.AddChildWithId<OnlineUnitEntity, long, long>(request.UserId, request.GateId, request.GateKey);
                }
                else
                {
                    unit.GateId = request.GateId;
                    unit.GateKey = request.GateKey;
                }

                reply();
                await ETTask.CompletedTask;
            }
        }
    }
}
