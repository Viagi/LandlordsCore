namespace ETModel
{
    /// <summary>
    /// 房间等级
    /// </summary>
    public enum RoomLevel : byte
    {
        Lv100   //100底分局
    }

    /// <summary>
    /// 出牌类型
    /// </summary>
    public enum CardsType : byte
    {
        None,
        JokerBoom,          //王炸
        Boom,               //炸弹
        OnlyThree,          //三张
        ThreeAndOne,        //三带一
        ThreeAndTwo,        //三带二
        Straight,           //顺子 五张或更多的连续单牌
        DoubleStraight,     //双顺 三对或更多的连续对牌
        TripleStraight,     //三顺 二个或更多的连续三张牌
        Double,             //对子
        Single              //单牌
    }
}
