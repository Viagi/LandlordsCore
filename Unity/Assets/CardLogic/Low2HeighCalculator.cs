
/**
 * 牌值越大，牌越小的计算器，花色不参与牌大小比较 
 * 有人发狂，硬要进行牌大小比较时，牌值大的反而小
 * 
 * @author Leon
 *
 */
public class Low2HeighCalculator : ValueCalculator {

	// 获取炸弹牌值绝对大小
	public int getBombValue(Player player) {
		return 14 - player.Cards[0].getNumber();
	}

	// 获取同花顺牌值绝对大小
	public int getStraightFlushValue(Player player) {
		//if (player.IsA32()) {
		//	//此时A就等于是1
		//	return 13 + PlayerTypeLow2Heigh.BOMB_MAX_VALUE;
		//}
		return 14 - player.Cards[2].getNumber() + PlayerTypeLow2Heigh.BOMB_MAX_VALUE;
	}

	// 获取同花牌值绝对大小
	public int getFlushValue(Player player) {
		return (14 - player.Cards[0].getNumber()) * 256 + (14 - player.Cards[1].getNumber()) * 16
				+ (14 - player.Cards[2].getNumber()) + PlayerTypeLow2Heigh.STRAIGHT_FLUSH_MAX_VALUE;
	}

	// 获取顺子牌值绝对大小
	public int getStraightValue(Player player) {
        if (player.IsA32)
        {
            //此时A就等于是1
            return 13 + PlayerTypeLow2Heigh.FLUSH_MAX_VALUE;
        }
        return 14 - player.Cards[2].getNumber() + PlayerTypeLow2Heigh.FLUSH_MAX_VALUE;
	}

	// 获取对子牌值绝对大小
	// 在判断牌型时，如果是对子，则将对子放在数组前面两位
	public int getDoubleValue(Player player) {
		return (14 - player.Cards[1].getNumber()) * 16 + (14 - player.Cards[2].getNumber())
				+ PlayerTypeLow2Heigh.STRAIGHT_MAX_VALUE;
	}

	// 获取普通牌值绝对大小
	public int getNormalValue(Player player) {
		return (14 - player.Cards[0].getNumber()) * 256 + (14 - player.Cards[1].getNumber()) * 16
				+ (14 - player.Cards[2].getNumber()) + PlayerTypeLow2Heigh.DOUBLE_MAX_VALUE;
	}

}
