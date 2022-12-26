using System.Net.Sockets;

namespace ET.Client
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> CreateClientScene(int zone, string name)
        {
            await ETTask.CompletedTask;

            Scene clientScene = EntitySceneFactory.CreateScene(zone, SceneType.Client, name, ClientSceneManagerComponent.Instance);
            clientScene.AddComponent<CurrentScenesComponent>();
            clientScene.AddComponent<ObjectWait>();
            clientScene.AddComponent<PlayerComponent>();

            EventSystem.Instance.Publish(clientScene, new EventType.AfterCreateClientScene());
            return clientScene;
        }

        public static async ETTask<Scene> CreateUnitScene(Entity entity, string name)
        {
            await ETTask.CompletedTask;

            Scene robotScene = EntitySceneFactory.CreateScene(entity.Id, IdGenerater.Instance.GenerateInstanceId(), entity.DomainZone(), SceneType.Client, name, ClientSceneManagerComponent.Instance);
            robotScene.AddComponent<ObjectWait>();

            EventSystem.Instance.Publish(robotScene, new EventType.AfterCreateClientScene());
            return robotScene;
        }

        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, IdGenerater.Instance.GenerateInstanceId(), zone, SceneType.Current, name, currentScenesComponent);
            currentScenesComponent.Scene = currentScene;

            EventSystem.Instance.Publish(currentScene, new EventType.AfterCreateCurrentScene());
            return currentScene;
        }


    }
}