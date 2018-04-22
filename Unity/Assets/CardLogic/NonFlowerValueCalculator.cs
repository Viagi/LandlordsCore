/**
 * 花色不参与牌大小比较的计算器
 * 牌值越大，牌越大
 * @author Leon
 *
 */
public class NonFlowerValueCalculator : ValueCalculator
{

    // 获取炸弹牌值绝对大小
    public int getBombValue(Player player)
{
    return player.Cards[0].getNumber() + PlayerType.STRAIGHT_FLUSH_MAX_VALUE;
}

// 获取同花顺牌值绝对大小,A32也是同花顺，是最小的同花顺(参考自百度百科)
public int getStraightFlushValue(Player player)
{
        if (player.IsA32)
        {
            //此时A就等于是1
            return 1 + PlayerType.FLUSH_MAX_VALUE;
        }
        return player.Cards[2].getNumber() + PlayerType.FLUSH_MAX_VALUE;
}

// 获取同花牌值绝对大小
public int getFlushValue(Player player)
{
    return player.Cards[0].getNumber() * 256 + player.Cards[1].getNumber() * 16 + player.Cards[2].getNumber()
            + PlayerType.STRAIGHT_MAX_VALUE;
}

// 获取顺子牌值绝对大小
public int getStraightValue(Player player)
{
        if (player.IsA32)
        {
            //此时A就等于是1
            return 1 + PlayerType.DOUBLE_MAX_VALUE;
        }
        return player.Cards[2].getNumber() + PlayerType.DOUBLE_MAX_VALUE;
}

// 获取对子牌值绝对大小
// 在判断牌型时，如果是对子，则将对子放在数组前面两位
public int getDoubleValue(Player player)
{
    return player.Cards[1].getNumber() * 16 + player.Cards[2].getNumber() + PlayerType.NORMAL_MAX_VALUE;
}

// 获取普通牌值绝对大小
public int getNormalValue(Player player)
{
    return player.Cards[0].getNumber() * 256 + player.Cards[1].getNumber() * 16 + player.Cards[2].getNumber();
}
}