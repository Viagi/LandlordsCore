using System;
using UnityEngine;

namespace ET.Client
{
    namespace Landlords
    {
        [UIEvent(UIType.Lobby)]
        public class UILobbyEvent : AUIEvent
        {
            public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
            {
                await uiComponent.DomainScene().GetComponent<ResourcesLoaderComponent>().LoadAsync(UIType.Lobby.StringToAB());
                GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.Lobby.StringToAB(), UIType.Lobby);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.GetLayer((int)uiLayer));
                UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.Lobby, gameObject);
                ui.AddComponent<UILobbyComponent>();
                return ui;
            }

            public override void OnRemove(UIComponent uiComponent)
            {
                ResourcesComponent.Instance.UnloadBundle(UIType.Lobby.StringToAB());
            }
        }
    }
}