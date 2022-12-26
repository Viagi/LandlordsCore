namespace ET
{
    namespace Landlords
    {
        public enum ERoomStatus : ushort
        {
            None = 0,
            //叫地主
            CallLandlord,
            //抢地主
            RobLandlord,
            //出牌
            PlayCard
        }
    }
}
