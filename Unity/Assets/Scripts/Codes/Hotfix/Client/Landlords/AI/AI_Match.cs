using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        public class AI_Match : AAIHandler
        {
            public override int Check(AIComponent aiComponent, AIConfig aiConfig)
            {
                Scene scene = aiComponent.DomainScene();
                AccountComponent accountComponent = scene.GetComponent<AccountComponent>();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                if (accountComponent != null && roomComponent == null && accountComponent.Money > 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
            {
                Scene scene = aiComponent.DomainScene();
                await LobbyHelper.EnterMatch(scene, cancellationToken);
            }
        }
    }
}
