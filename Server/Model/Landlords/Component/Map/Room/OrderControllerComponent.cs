using System.Collections.Generic;

namespace ETModel
{
    public class OrderControllerComponent : Component
    {
        //先手玩家
        public KeyValuePair<long, bool> FirstAuthority { get; set; }

        //玩家抢地主状态
        public Dictionary<long, bool> GamerLandlordState = new Dictionary<long, bool>();

        //本轮最大牌型玩家
        public long Biggest { get; set; }

        //当前出牌玩家
        public long CurrentAuthority { get; set; }

        //当前抢地主玩家
        public int SelectLordIndex { get; set; }

        public override void Dispose()
        {
            if(this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.GamerLandlordState.Clear();
            this.Biggest = 0;
            this.CurrentAuthority = 0;
            this.SelectLordIndex = 0;
        }
    }
}
