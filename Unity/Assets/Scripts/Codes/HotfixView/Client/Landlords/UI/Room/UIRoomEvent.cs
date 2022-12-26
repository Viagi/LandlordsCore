using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ET.Client
{
    namespace Landlords
    {
        [UIEvent(UIType.Room)]
        public class UIRoomEvent : AUIEvent
        {
            public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
            {
                ResourcesLoaderComponent resourcesLoaderComponent = uiComponent.DomainScene().GetComponent<ResourcesLoaderComponent>();
                using (ListComponent<ETTask> tasks = ListComponent<ETTask>.Create())
                {
                    tasks.Add(resourcesLoaderComponent.LoadAsync(UIType.Room.StringToAB()));
                    tasks.Add(resourcesLoaderComponent.LoadAsync(RoomResourceType.Atlas.StringToAB()));
                    tasks.Add(resourcesLoaderComponent.LoadAsync(RoomResourceType.HandCard.StringToAB()));
                    tasks.Add(resourcesLoaderComponent.LoadAsync(RoomResourceType.PlayCard.StringToAB()));
                    tasks.Add(resourcesLoaderComponent.LoadAsync(RoomResourceType.Content.StringToAB()));
                    await ETTaskHelper.WaitAll(tasks);
                }
                GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.Room.StringToAB(), UIType.Room);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.GetLayer((int)uiLayer));
                UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.Room, gameObject);
                UIRoomComponent uiRoomComponent = ui.AddComponent<UIRoomComponent>();
                uiRoomComponent.AddComponent<InteractionComponent>();
                uiRoomComponent.AddComponent<EndComponent>();
                return ui;
            }

            public override void OnRemove(UIComponent uiComponent)
            {
                ResourcesComponent.Instance.UnloadBundle(UIType.Room.StringToAB());
                ResourcesComponent.Instance.UnloadBundle(RoomResourceType.Atlas.StringToAB());
                ResourcesComponent.Instance.UnloadBundle(RoomResourceType.HandCard.StringToAB());
                ResourcesComponent.Instance.UnloadBundle(RoomResourceType.PlayCard.StringToAB());
                ResourcesComponent.Instance.UnloadBundle(RoomResourceType.Content.StringToAB());
            }
        }
    }
}