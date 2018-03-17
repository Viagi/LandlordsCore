using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 分配房间服务器组件，逻辑在AllotMapComponentSystem扩展
    /// </summary>
    public class AllotMapComponent : Component
    {
        public readonly List<StartConfig> MapAddress = new List<StartConfig>();
    }
}
