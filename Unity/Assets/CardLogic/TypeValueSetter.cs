/**
 * 牌型识别器，负责鉴定牌型，并按照指定的模式计算牌大小
 * 
 * @author Leon
 *
 */
public class TypeValueSetter
{

    private ValueCalculator calculator;

    public TypeValueSetter(ValueCalculator calculator)
    {
        this.calculator = calculator;
    }

    // 判断牌型、计算牌型绝对值大小
    public Player regPlayerType(Player player)
    {
        if (isFlush(player))
        {
            if (isStraight(player))
            {// 同花顺
                player.Type = PlayerType.STRAIGHT_FLUSH;
                player.Value = calculator.getStraightFlushValue(player);
            }
            else
            {// 同花
                player.Type = PlayerType.FLUSH;
                player.Value = calculator.getFlushValue(player);
            }
        }
        else if (isStraight(player))
        {// 顺子
            player.Type = PlayerType.STRAIGHT;
            player.Value = calculator.getStraightValue(player);
        }
        else if (isDouble(player))
        {
            if (isBmob(player))
            {// 炸弹
                player.Type = PlayerType.BOMB;
                player.Value = calculator.getBombValue(player);
            }
            else
            {// 对子
                player.Type = PlayerType.DOUBLE;
                // 将对子放到玩家牌的前两张的位置,以便于之后的牌值计算
                PlayerUtil.moveDouble2Front(player);
                player.Value = calculator.getDoubleValue(player);
            }
        }
        else
        {// 普通牌
            player.Type = PlayerType.NORMAL;
            player.Value = calculator.getNormalValue(player);
            // 对于特殊牌，本算法不提供特殊大小计算，外部调用者自行判断是否有炸弹玩家产生
            player.IsSpecial = isSpecial(player);

        }
        return player;
    }

    // 是否同花
    private bool isFlush(Player player)
    {
        return player.Cards[0].getFlower() == player.Cards[1].getFlower()
                && player.Cards[1].getFlower() == player.Cards[2].getFlower();
    }

    // 是否顺子,A32也是同花顺，是最小的同花顺(参考自百度百科)
    // 花色参与比较的时候，黑桃A红桃3黑桃2<方片A黑桃3方片2
    private bool isStraight(Player player)
    {
        bool isNomalStraight = player.Cards[0].getNumber() == player.Cards[1].getNumber() + 1
                && player.Cards[1].getNumber() == player.Cards[2].getNumber() + 1;
        bool isA32 = player.Cards[0].getNumber() == 14 && player.Cards[1].getNumber() == 3
                && player.Cards[2].getNumber() == 2;

        player.IsA32 = isA32;

        return isNomalStraight || isA32;
    }

    // 是否炸弹
    private bool isBmob(Player player)
    {
        return player.Cards[0].getNumber() == player.Cards[1].getNumber()
                && player.Cards[1].getNumber() == player.Cards[2].getNumber();
    }

    // 是否对子
    private bool isDouble(Player player)
    {
        return player.Cards[0].getNumber() == player.Cards[1].getNumber()
                || player.Cards[1].getNumber() == player.Cards[2].getNumber();
    }

    // 是否特殊牌
    private bool isSpecial(Player player)
    {
        return player.Cards[0].getNumber() == 5 && player.Cards[1].getNumber() == 3 && player.Cards[2].getNumber() == 2;
    }
}