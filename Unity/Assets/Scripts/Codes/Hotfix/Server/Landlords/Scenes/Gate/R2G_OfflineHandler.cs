using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Gate)]
        public class R2G_OfflineHandler : AMActorHandler<Player, Actor_Offline>
        {
            protected override async ETTask Run(Player unit, Actor_Offline message)
            {
                UnitGateComponent unitGateComponent = unit.GetComponent<UnitGateComponent>();
                Session session = Root.Instance.Get(unitGateComponent.GateSessionId) as Session;

                if (session != null && !session.IsDisposed)
                {
                    session.Send(new ClientOffline());
                    session.Dispose();
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
