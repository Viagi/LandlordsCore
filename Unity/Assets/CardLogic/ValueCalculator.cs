/**
 * 牌值计算器
 * 
 * @author Leon
 *
 */
public interface ValueCalculator
{
    // 获取炸弹牌值绝对大小
    int getBombValue(Player player);

    // 获取同花顺牌值绝对大小
    int getStraightFlushValue(Player player);

    // 获取同花牌值绝对大小
    int getFlushValue(Player player);

    // 获取顺子牌值绝对大小
    int getStraightValue(Player player);

    // 获取对子牌值绝对大小
    // 在判断牌型时，如果是对子，则将对子放在数组前面两位
    int getDoubleValue(Player player);

    // 获取普通牌值绝对大小
    int getNormalValue(Player player);

}