

/**
 * 牌值越大，牌越小的计算器，并且花色参与牌大小比较
 * 
 * @author Leon
 *
 */
public class FlowerLow2HeighCalculator : ValueCalculator {

	private int getFlowerValue(Player player) {
		return (4 - player.Cards[0].getFlower()) * 16 + (4 - player.Cards[1].getFlower()) * 4 + 4
				- player.Cards[2].getFlower();
	}

	private int getA32FlowerValue(Player player) {
		return (4 - player.Cards[1].getFlower()) * 16 + (4 - player.Cards[1].getFlower()) * 4 + 4
				- player.Cards[0].getFlower();
	}

	// 获取炸弹牌值绝对大小
	public int getBombValue(Player player) {
		PlayerUtil.sortPlayerByFlower(player);
		return (14 - player.Cards[0].getNumber()) * 64 + getFlowerValue(player);
	}

	// 获取同花顺牌值绝对大小
	public int getStraightFlushValue(Player player) {
        if (player.IsA32)
        {
            return (13 + PlayerTypeLow2Heigh.BOMB_MAX_VALUE) * 64 + getA32FlowerValue(player);
        }
        return (14 - player.Cards[2].getNumber() + PlayerTypeLow2Heigh.BOMB_MAX_VALUE) * 64 + getFlowerValue(player);
	}

	// 获取同花牌值绝对大小
	public int getFlushValue(Player player) {
		return ((14 - player.Cards[0].getNumber()) * 256 + (14 - player.Cards[1].getNumber()) * 16
				+ (14 - player.Cards[2].getNumber()) + PlayerTypeLow2Heigh.STRAIGHT_FLUSH_MAX_VALUE) * 64
				+ getFlowerValue(player);
	}

	// 获取顺子牌值绝对大小
	public int getStraightValue(Player player) {
		//if (player.IsA32()) {
		//	return (13 + PlayerTypeLow2Heigh.FLUSH_MAX_VALUE) * 64 + getA32FlowerValue(player);
		//}
		return (14 - player.Cards[2].getNumber() + PlayerTypeLow2Heigh.FLUSH_MAX_VALUE) * 64 + getFlowerValue(player);
	}

	// 获取对子牌值绝对大小
	// 在判断牌型时，如果是对子，则将对子放在数组前面两位
	public int getDoubleValue(Player player) {
		// 在花色参与计算大小时，将对子中的花色大的换到前面
		PlayerUtil.exchangeSortedDoubleFlower(player);
		return ((14 - player.Cards[1].getNumber()) * 16 + (14 - player.Cards[2].getNumber())
				+ PlayerTypeLow2Heigh.STRAIGHT_MAX_VALUE) * 64 + getFlowerValue(player);
	}

	// 获取普通牌值绝对大小
	public int getNormalValue(Player player) {
		return ((14 - player.Cards[0].getNumber()) * 256 + (14 - player.Cards[1].getNumber()) * 16
				+ (14 - player.Cards[2].getNumber()) + PlayerTypeLow2Heigh.DOUBLE_MAX_VALUE) * 64
				+ getFlowerValue(player);
	}

}
