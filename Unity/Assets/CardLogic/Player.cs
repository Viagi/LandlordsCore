
using System.Collections.Generic;
/**
* 玩家，对应一副牌
*/
public class Player
{

    private Card[] cards = new Card[3];
    public Card[] Cards {get { return cards; }set { cards = value; }}
    
    // 牌类型
    private int type;
    public int Type { get { return type; }set{ type = value; } }
    
    // 是否为特殊牌
    private bool isSpecial = false;
    public bool IsSpecial { get { return isSpecial; } set { isSpecial = value; } }

    // A32也是顺子，比花色时，从3开始比较
    private bool isA32 = false;
    public bool IsA32 { get { return isA32; }set { isA32 = value; }}

    // 牌绝对值大小
    private int value;
    public int Value { get { return value; }set { this.value = value; } }

    public Player()
    {
    }

    public Player(Card card0, Card card1, Card card2)
    {
        this.cards[0] = card0;
        this.cards[1] = card1;
        this.cards[2] = card2;
    }

}

public class PlayerCompare : IComparer<Player>
{
    public int Compare(Player x, Player y)
    {
        return x.Value.CompareTo(y.Value);
    }
}

