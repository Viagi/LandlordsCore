using ET.Landlords;
using ET.Server.Landlords;
using System;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Match)]
    public class Actor_RemoveMatchUnitHandler : AMActorLocationRpcHandler<MatchUnitEntity, Actor_RemoveMatchUnitRequest, Actor_RemoveMatchUnitResponse>
    {
        protected override async ETTask Run(MatchUnitEntity unit, Actor_RemoveMatchUnitRequest request, Actor_RemoveMatchUnitResponse response, Action reply)
        {
            MatchUnitManagerComponent managerComponent = unit.GetParent<MatchUnitManagerComponent>();
            managerComponent.RemoveMatch(unit);
            managerComponent.ExitRoom(unit);

            if (unit.IsPlaying)
            {
                unit.IsOffline = true;
            }
            else
            {
                unit.Dispose();
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}
