using Model;
using System;
using UnityEngine;

namespace Hotfix
{
    [UIFactory((int)UIType.LandlordsLogin)]
    public class LandlordsLoginFactory : IUIFactory
    {
        public UI Create(Scene scene, UIType type, GameObject gameObject)
        {
            try
            {
                //加载AB包
                ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle($"{type}.unity3d");

                //加载登录界面预设并生成实例
                GameObject bundleGameObject = resourcesComponent.GetAsset<GameObject>($"{type}.unity3d", $"{type}");
                GameObject login = UnityEngine.Object.Instantiate(bundleGameObject);

                //设置UI层级，只有UI摄像机可以渲染
                login.layer = LayerMask.NameToLayer(LayerNames.UI);

                //创建登录界面实体
                UI ui = ComponentFactory.Create<UI,GameObject>(login);

                //添加登录界面组件
                ui.AddComponent<LandlordsLoginComponent>();
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
            //卸载AB包
            Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"{type}.unity3d");
        }
    }
}
