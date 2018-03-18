using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public class LandlordsEndFactory
    {
        public static UI Create(string type, UI parent, bool isWin)
        {
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.LoadBundle($"{type}.unity3d");
            GameObject prefab = (GameObject)resourcesComponent.GetAsset($"{type}.unity3d", $"{type}");
            GameObject endPanel = UnityEngine.Object.Instantiate(prefab);

            endPanel.layer = LayerMask.NameToLayer("UI");

            UI ui = ComponentFactory.Create<UI, GameObject>(endPanel);
            parent.Add(ui);
            ui.GameObject.transform.SetParent(parent.GameObject.transform, false);

            ui.AddComponent<LandlordsEndComponent, bool>(isWin);
            return ui;
        }
    }
}
