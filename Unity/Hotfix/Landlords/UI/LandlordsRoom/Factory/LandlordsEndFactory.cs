using Model;
using UnityEngine;

namespace Hotfix
{
    public class LandlordsEndFactory
    {
        public static UI Create(UIType type, UI parent, bool isWin)
        {
            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.LoadBundle($"{type}.unity3d");
            GameObject prefab = resourcesComponent.GetAsset<GameObject>($"{type}.unity3d", $"{type}");
            GameObject endPanel = UnityEngine.Object.Instantiate(prefab);

            endPanel.layer = LayerMask.NameToLayer("UI");

            UI ui = ComponentFactory.Create<UI, UI, GameObject>(parent, endPanel);
            ui.AddComponent<LandlordsEndComponent, bool>(isWin);
            return ui;
        }
    }
}
