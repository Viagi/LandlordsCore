using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class GameStart_RefreshRoom : AEvent<GameStart>
        {
            protected override async ETTask Run(Scene scene, GameStart args)
            {
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                UIComponent uiComponent = scene.GetComponent<UIComponent>();
                UI ui = uiComponent.Get(UIType.Room);
                UIRoomComponent uiRoomComponent = ui?.GetComponent<UIRoomComponent>();
                if (uiRoomComponent != null && roomComponent != null)
                {
                    uiRoomComponent.RefreshLandlordCards(roomComponent);
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
