using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Match)]
        public class G2M_OfflineHandler : AMActorHandler<MatchUnitEntity, Actor_Offline>
        {
            protected override async ETTask Run(MatchUnitEntity unit, Actor_Offline message)
            {
                MatchUnitManagerComponent managerComponent = unit.DomainScene().GetComponent<MatchUnitManagerComponent>();
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

                await ETTask.CompletedTask;
            }
        }
    }
}
