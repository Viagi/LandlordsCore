using Model;
using System.Net;

namespace Hotfix
{
    public static class MapHelper
    {
        /// <summary>
        /// 发送消息给匹配服务器
        /// </summary>
        /// <param name="message"></param>
        public static void SendMessage(IMessage message)
        {
            IPEndPoint matchIPEndPoint = Game.Scene.GetComponent<StartConfigComponent>().MatchConfig.GetComponent<InnerConfig>().IPEndPoint;
            Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchIPEndPoint);
            matchSession.Send(message);
        }
    }
}
