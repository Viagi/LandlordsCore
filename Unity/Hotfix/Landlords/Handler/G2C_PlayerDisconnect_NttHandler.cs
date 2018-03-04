using Model;

namespace Hotfix
{
    [MessageHandler]
    public class G2C_PlayerDisconnect_NttHandler : AMHandler<G2C_PlayerDisconnect_Ntt>
    {
        protected override void Run(Session session, G2C_PlayerDisconnect_Ntt message)
        {
            //移除连接组件
            Game.Scene.RemoveComponent<SessionComponent>();
            //释放本地玩家对象
            ClientComponent clientComponent = Game.Scene.GetComponent<ClientComponent>();
            clientComponent.LocalPlayer.Dispose();
            clientComponent.LocalPlayer = null;

            UIComponent uiComponent = Hotfix.Scene.GetComponent<UIComponent>();

            UI uiLogin = uiComponent.Create(UIType.LandlordsLogin);
            uiLogin.GetComponent<LandlordsLoginComponent>().SetPrompt("连接断开");

            if (uiComponent.Get(UIType.LandlordsLobby) != null)
            {
                uiComponent.Remove(UIType.LandlordsLobby);
            }
            else if(uiComponent.Get(UIType.LandlordsRoom) != null)
            {
                uiComponent.Remove(UIType.LandlordsRoom);
            }
        }
    }
}
