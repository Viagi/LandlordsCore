
using System.Collections.Generic;
/**
* 牌型判断比较器，负责对所有玩家的牌大小进行计算和排序
* 
* @author Leon
*
*/
public class PlayerComparator//:IComparer<Player>
{

    private TypeValueSetter recognizer;

    public PlayerComparator(ValueCalculator calculator)
    {
        this.recognizer = new TypeValueSetter(calculator);
    }

    public PlayerComparator()
    {
    }

    /**
     * 对玩家牌型进行牌型识别和牌值计算 这副牌三张都已经按照数字从大到小排好序
     * 
     * @param player
     *            一副牌
     */
    public void setupRegularPlayer(Player player)
    {
        recognizer.regPlayerType(player);
    }

    /**
     * 对玩家牌型进行牌型识别和牌值计算 这副牌没有按照数字从大到小排好序
     * 
     * @param player
     *            一副牌
     */
    public void setupUnRegularPlayer(Player player)
    {
        PlayerUtil.sortPlayerByNumber(player);
        recognizer.regPlayerType(player);
    }

    /**
     * 对玩家列表进行牌型判断、值获取及排序 每副牌的三张牌都已经按照数字从大到小排序
     * 
     * @param playersInput
     *            一组牌
     */
    public void sortRegularPlayers(List<Player> playersInput)
    {
        for (int i = 0; i < playersInput.Count; i++)
        {
            recognizer.regPlayerType(playersInput[i]);
        }
        playersInput.Sort(new PlayerCompare());

        //for (Player player : playersInput)
        //{
        //    recognizer.regPlayerType(player);
        //}
        //Collections.sort(playersInput, this);
    }

    /**
     * 对玩家列表进行牌型判断、值获取及排序 每副牌的三张牌没有按照从大到小排序
     * 
     * @param playersInput
     *            一组牌
     */
    public void sortUnRegularPlayers(List<Player> playersInput)
    {

        for (int i = 0; i < playersInput.Count; i++)
        {
            PlayerUtil.sortPlayerByNumber(playersInput[i]);
            recognizer.regPlayerType(playersInput[i]);
        }
        playersInput.Sort(new PlayerCompare());
        //for (Player player : playersInput)
        //{
        //    PlayerUtil.sortPlayerByNumber(player);
        //    recognizer.regPlayerType(player);
        //}
        //Collections.sort(playersInput, this);
    }



}