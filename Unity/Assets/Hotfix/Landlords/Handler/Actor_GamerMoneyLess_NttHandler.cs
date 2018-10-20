using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerMoneyLess_NttHandler : AMHandler<Actor_GamerMoneyLess_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerMoneyLess_Ntt message)
        {
            long userId = ClientComponent.Instance.LocalPlayer.UserID;
            if (message.UserID == userId)
            {
                //余额不足时退出房间
                UI room = Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom);
                room.GetComponent<LandlordsRoomComponent>().OnQuit();
            }
        }
    }
}
