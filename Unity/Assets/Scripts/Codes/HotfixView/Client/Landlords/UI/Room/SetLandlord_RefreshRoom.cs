using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class SetLandlord_RefreshRoom : AEvent<SetLandlord>
        {
            protected override async ETTask Run(Scene scene, SetLandlord args)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
                {
                    RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                    UIComponent uiComponent = scene.GetComponent<UIComponent>();
                    UI ui = uiComponent.Get(UIType.Room);
                    UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                    if (uiRoomComponent != null && roomComponent != null)
                    {
                        uiRoomComponent.RefreshLandlordCards(roomComponent);
                        if (args.UnitId == roomComponent.MyId)
                        {
                            uiRoomComponent.Get(args.UnitId).SelectedCards(args.Cards);
                        }
                    }
                }
            }
        }
    }
}
