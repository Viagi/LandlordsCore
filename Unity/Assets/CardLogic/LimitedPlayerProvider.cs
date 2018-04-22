using System.Collections.Generic;
using UnityEngine;
/**
* 有限制的发牌器，只有一副牌，玩家数量有限
* 
* @author Leon
*
*/
public class LimitedPlayerProvider : PlayerProvider
{

    private List<Card> cards = new List<Card>();

    public LimitedPlayerProvider()
    {
        this.initCards();
    }

    // 产生一副新的牌
    private void initCards()
    {
        cards.Clear();
        for (int i = 14; i > 1; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                Card card = new Card(j, i);
                cards.Add(card);
            }
        }
    }

    //@Override
    public Player getSinglePlayer()
    {
        if (cards.Count < 3)
        {// 牌不够发了，请洗牌！
            return null;//throw new Exception("牌不够发了，请洗牌");
        }
        Player player = new Player();
        for (int i = 0; i < 3; i++)
        {
            // 随机从一副有序的牌中抽取一张牌
            player.Cards[i] = getCard();
        }
        PlayerUtil.sortPlayerByNumber(player);
        return player;
    }

    //@Override
    public List<Player> getPlayers(int number)
    {
        if (cards.Count == 52 && number > 17)
        {
            Debug.LogError("这么多人玩？牌都不够发!");
        }
        else if (number * 3 > cards.Count)
        {
            return null;
        }
        List<Player> players = new List<Player>();
        for (int i = 0; i < number; i++)
        {
            Player player = getSinglePlayer();
            players.Add(player);
        }
        return players;
    }

    //@Override
    public void shuffle()
    {
        this.initCards();
    }

    //@Override
    public Card getCard()
    {
        if (cards.Count > 0)
        {

            var card = cards[Random.Range(0, cards.Count)];
            cards.Remove(card);
            return card;
        }
        return null;
    }
		//return ? cards.Remove(random.nextInt(cards.Count)) : null;
	
}
