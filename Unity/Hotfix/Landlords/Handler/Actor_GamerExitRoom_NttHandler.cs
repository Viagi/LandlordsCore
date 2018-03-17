using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerExitRoom_NttHandler : AMHandler<Actor_GamerExitRoom_Ntt>
    {
        protected override void Run(Session session, Actor_GamerExitRoom_Ntt message)
        {
            UI uiRoom = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent landlordsRoomComponent = uiRoom.GetComponent<LandlordsRoomComponent>();
            landlordsRoomComponent.RemoveGamer(message.UserID);
        }
    }
}
