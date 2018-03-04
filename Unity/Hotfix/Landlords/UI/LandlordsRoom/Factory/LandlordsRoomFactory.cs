using Model;
using System;
using UnityEngine;

namespace Hotfix
{
    [UIFactory((int)UIType.LandlordsRoom)]
    public class LandlordsRoomFactory : IUIFactory
    {
        public UI Create(Scene scene, UIType type, GameObject gameObject)
        {
            try
            {
                ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle($"{type}.unity3d");
                resourcesComponent.LoadBundle($"{CardHelper.ATLAS_NAME}.unity3d");
                resourcesComponent.LoadBundle($"{HandCardsComponent.HANDCARD_NAME}.unity3d");
                resourcesComponent.LoadBundle($"{HandCardsComponent.PLAYCARD_NAME}.unity3d");
                GameObject bundleGameObject = resourcesComponent.GetAsset<GameObject>($"{type}.unity3d", $"{type}");
                GameObject room = UnityEngine.Object.Instantiate(bundleGameObject);
                room.layer = LayerMask.NameToLayer(LayerNames.UI);
                UI ui = ComponentFactory.Create<UI, GameObject>(room);

                ui.AddComponent<GamerComponent>();
                ui.AddComponent<LandlordsRoomComponent>();
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
            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.UnloadBundle($"{type}.unity3d");
            resourcesComponent.UnloadBundle($"{HandCardsComponent.HANDCARD_NAME}.unity3d");
            resourcesComponent.UnloadBundle($"{HandCardsComponent.PLAYCARD_NAME}.unity3d");
            resourcesComponent.UnloadBundle($"{CardHelper.ATLAS_NAME}.unity3d");
        }
    }
}
