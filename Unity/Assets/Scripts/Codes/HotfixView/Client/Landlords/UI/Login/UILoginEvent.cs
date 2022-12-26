using System;
using UnityEngine;

namespace ET.Client
{
    namespace Landlords
    {
        [UIEvent(UIType.Login)]
        public class UILoginEvent : AUIEvent
        {
            public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
            {
                await uiComponent.DomainScene().GetComponent<ResourcesLoaderComponent>().LoadAsync(UIType.Login.StringToAB());
                GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.Login.StringToAB(), UIType.Login);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.GetLayer((int)uiLayer));
                UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.Login, gameObject);
                ui.AddComponent<UILoginComponent>();
                return ui;
            }

            public override void OnRemove(UIComponent uiComponent)
            {
                ResourcesComponent.Instance.UnloadBundle(UIType.Login.StringToAB());
            }
        }
    }
}