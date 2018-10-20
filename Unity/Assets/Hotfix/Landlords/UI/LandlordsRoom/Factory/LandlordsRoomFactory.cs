using ETModel;
using System;
using UnityEngine;

namespace ETHotfix
{
    [UIFactory(UIType.LandlordsRoom)]
    public class LandlordsRoomFactory : IUIFactory
    {
        public UI Create(Scene scene, string type, GameObject parent)
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle($"{type}.unity3d");
                resourcesComponent.LoadBundle($"{CardHelper.ATLAS_NAME}.unity3d");
                resourcesComponent.LoadBundle($"{HandCardsComponent.HANDCARD_NAME}.unity3d");
                resourcesComponent.LoadBundle($"{HandCardsComponent.PLAYCARD_NAME}.unity3d");
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset($"{type}.unity3d", $"{type}");
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

        public void Remove(string type)
        {
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.UnloadBundle($"{type}.unity3d");
            resourcesComponent.UnloadBundle($"{HandCardsComponent.HANDCARD_NAME}.unity3d");
            resourcesComponent.UnloadBundle($"{HandCardsComponent.PLAYCARD_NAME}.unity3d");
            resourcesComponent.UnloadBundle($"{CardHelper.ATLAS_NAME}.unity3d");
        }
    }
}
