
using System.Collections.Generic;
/**
* 发牌器
* 
* @author Leon
*
*/
public interface PlayerProvider
{

    // 产生单副牌
    Player getSinglePlayer();

    // 产生多副牌
    List<Player> getPlayers(int number);

    // 发一张牌
    Card getCard();

    // 洗牌
    void shuffle();
}