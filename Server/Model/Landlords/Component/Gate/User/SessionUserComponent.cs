using System.Net;

namespace Model
{
    /// <summary>
    /// Session关联User对象组件
    /// 用于Session断开时触发下线
    /// </summary>
    public class SessionUserComponent : Component
    {
        // User对象
        public User User { get; set; }

        public override async void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            //释放User对象时将User对象从管理组件中移除
            Game.Scene.GetComponent<UserComponent>()?.Remove(this.User.UserID);

            StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
            ActorProxyComponent actorProxyComponent = Game.Scene.GetComponent<ActorProxyComponent>();

            //正在匹配中发送玩家退出匹配请求
            if (this.User.IsMatching)
            {
                IPEndPoint matchIPEndPoint = config.MatchConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchIPEndPoint);
                await matchSession.Call(new G2M_PlayerExitMatch_Req() { UserID = this.User.UserID });
            }

            //正在游戏中发送玩家退出房间请求
            if(this.User.ActorID != 0)
            {
                ActorProxy actorProxy = actorProxyComponent.Get(this.User.ActorID);
                await actorProxy.Call(new Actor_PlayerExitRoom_Req() { UserID = this.User.UserID });
            }

            //向登录服务器发送玩家下线消息
            IPEndPoint realmIPEndPoint = config.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
            Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmIPEndPoint);
            realmSession.Send(new G2R_PlayerOffline_Ntt() { UserID = this.User.UserID });

            this.User.Dispose();
            this.User = null;
        }
    }
}
