using Model;
using System;
using UnityEngine;

namespace Hotfix
{
    public class LandlordsInteractionFactory
    {
        public static UI Create(Scene scene, UIType type, UI parent)
        {
            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.LoadBundle($"{type}.unity3d");
            GameObject prefab = resourcesComponent.GetAsset<GameObject>($"{type}.unity3d", $"{type}");
            GameObject interaction = UnityEngine.Object.Instantiate(prefab);

            interaction.layer = LayerMask.NameToLayer("UI");

            UI ui = EntityFactory.Create<UI, Scene, UI, GameObject>(scene, parent, interaction);
            ui.AddComponent<LandlordsInteractionComponent>();
            return ui;
        }
    }
}
