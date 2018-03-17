using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 匹配组件，匹配逻辑在MatchComponentSystem扩展
    /// </summary>
    public class MatchComponent : Component
    {
        //游戏中匹配对象列表
        public readonly Dictionary<long, long> Playing = new Dictionary<long, long>();

        //匹配成功队列
        public readonly Queue<Matcher> MatchSuccessQueue = new Queue<Matcher>();

        //创建房间消息加锁，避免因为延迟重复发多次创建房间消息
        public bool CreateRoomLock { get; set; }
    }
}
