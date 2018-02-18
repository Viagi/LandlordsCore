using Model;

namespace Hotfix
{
    [MessageHandler(Opcode.Actor_GamerExitRoom_Ntt)]
    public class Actor_GamerExitRoom_NttHandler : AMHandler<Actor_GamerExitRoom_Ntt>
    {
        protected override void Run(Session session, Actor_GamerExitRoom_Ntt message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
            LandlordsRoomComponent landlordsRoomComponent = uiRoom.GetComponent<LandlordsRoomComponent>();
            landlordsRoomComponent.RemoveGamer(message.UserID);
        }
    }
}
