
/**
 * 对牌型分类，并提供牌大小值的算法，和已经计算好的牌型最大值
 * 有人发狂， 硬要进行牌大小比较时，牌值大的反而小
 * 
 * @author Leon
 *
 */
public class PlayerTypeLow2Heigh {
	// 最大值222=14
	public const int BOMB_MAX_VALUE = 14;

	// A32也是顺子，是最小的同花顺(参考自百度百科)
	// 最大值A32=13，加上炸弹14=27
	public const int STRAIGHT_FLUSH_MAX_VALUE = 27;

	// 最大值532，14*16*16+13*16+11=3803，加上顺子27=3830
	public const int FLUSH_MAX_VALUE = 3830;

	// A32也是顺子，是最小的同花顺(参考自百度百科)
	// 最大值A32=13，加上对子的最大值基数3830=3843
	public const int STRAIGHT_MAX_VALUE = 3843;

	// 最大值223=14*16+13=237,加上普通牌的基数3843=4080
	public const int DOUBLE_MAX_VALUE = 4080;

	// 最大值532=14*16*16+13*16+11=3803，加上对子4080=7883
	public const int NORMAL_MAX_VALUE = 7883;

}