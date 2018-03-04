using Model;
using System;
using UnityEngine;

namespace Hotfix
{
    [UIFactory((int)UIType.LandlordsLobby)]
    public class LandlordsLobbyFactory : IUIFactory
    {
        public UI Create(Scene scene, UIType type, GameObject gameObject)
        {
            try
            {
                //加载AB包
                ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle($"{type}.unity3d");

                //加载大厅界面预设并生成实例
                GameObject bundleGameObject = resourcesComponent.GetAsset<GameObject>($"{type}.unity3d", $"{type}");
                GameObject lobby = UnityEngine.Object.Instantiate(bundleGameObject);

                //设置UI层级，只有UI摄像机可以渲染
                lobby.layer = LayerMask.NameToLayer(LayerNames.UI);
                UI ui = ComponentFactory.Create<UI, GameObject>(lobby);

                ui.AddComponent<LandlordsLobbyComponent>();
                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
                return null;
            }
        }

        public void Remove(UIType type)
        {
            Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"{type}.unity3d");
        }
    }
}
