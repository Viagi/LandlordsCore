namespace ET
{
    namespace Landlords
    {
        public enum ELandlordStatus : ushort
        {
            None = 0,
            //准备
            Ready,
            //不叫
            NotCall,
            //叫地主
            CallLandlord,
            //不抢
            DontRob,
            //抢地主
            RobLandlord,
            //再抢
            RobAgain,
            //出牌
            ShowCards,
            //不出
            DontShow,
        }
    }
}
