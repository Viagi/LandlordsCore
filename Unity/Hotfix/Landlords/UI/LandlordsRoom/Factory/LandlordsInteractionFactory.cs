using ETModel;
using System;
using UnityEngine;

namespace ETHotfix
{
    public class LandlordsInteractionFactory
    {
        public static UI Create(string type, UI parent)
        {
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.LoadBundle($"{type}.unity3d");
            GameObject prefab = (GameObject)resourcesComponent.GetAsset($"{type}.unity3d", $"{type}");
            GameObject interaction = UnityEngine.Object.Instantiate(prefab);

            interaction.layer = LayerMask.NameToLayer("UI");

            UI ui = ComponentFactory.Create<UI, GameObject>(interaction);
            parent.Add(ui);
            ui.GameObject.transform.SetParent(parent.GameObject.transform, false);

            ui.AddComponent<LandlordsInteractionComponent>();
            return ui;
        }
    }
}
