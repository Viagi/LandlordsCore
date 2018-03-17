using ETModel;
using System.Net;

namespace ETHotfix
{
    public static class MapHelper
    {
        /// <summary>
        /// 发送消息给匹配服务器
        /// </summary>
        /// <param name="message"></param>
        public static void SendMessage(IMessage message)
        {
            GetMapSession().Send(message);
        }

        /// <summary>
        /// 获取匹配服务器连接
        /// </summary>
        /// <returns></returns>
        public static Session GetMapSession()
        {
            IPEndPoint matchIPEndPoint = Game.Scene.GetComponent<StartConfigComponent>().MatchConfig.GetComponent<InnerConfig>().IPEndPoint;
            Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchIPEndPoint);
            return matchSession;
        }
    }
}
