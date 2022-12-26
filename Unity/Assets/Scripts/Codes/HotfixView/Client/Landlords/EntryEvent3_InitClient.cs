using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Process)]
        public class EntryEvent3_InitClient : AEvent<ET.EventType.EntryEvent3>
        {
            protected override async ETTask Run(Scene scene, ET.EventType.EntryEvent3 args)
            {
                // 加载配置
                Root.Instance.Scene.AddComponent<ResourcesComponent>();

                Root.Instance.Scene.AddComponent<GlobalComponent>();
                Root.Instance.Scene.AddComponent<ClientConsoleComponent>();

                Scene clientScene = await SceneFactory.CreateClientScene(1, "Game");

                await EventSystem.Instance.PublishAsync(clientScene, new EventType.Landlords.AppStartInitFinish());
            }
        }
    }
}