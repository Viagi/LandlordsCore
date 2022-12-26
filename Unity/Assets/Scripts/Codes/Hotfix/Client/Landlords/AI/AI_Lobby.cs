using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        public class AI_Lobby : AAIHandler
        {
            public override int Check(AIComponent aiComponent, AIConfig aiConfig)
            {
                Scene scene = aiComponent.DomainScene();
                AccountComponent accountComponent = scene.GetComponent<AccountComponent>();
                if (accountComponent == null)
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
                AccountComponent accountComponent = scene.GetComponent<AccountComponent>();
                if(accountComponent == null)
                {
                    await LobbyHelper.GetLobby(scene, cancellationToken);
                }
            }
        }
    }
}
