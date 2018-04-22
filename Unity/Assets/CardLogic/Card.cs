/**
 * 单张牌
 * 
 * @author Leon
 *
 */
public class Card
{

    public  const int FLOWER_SPADE = 3;// 黑桃
    public  const int FLOWER_HEART = 2;// 红桃
    public  const int FLOWER_CLUB = 1;// 梅花
    public  const int FLOWER_DIAMOND = 0;// 方片
    public  const int NUM_A = 14;
    public  const int NUM_K = 13;
    public  const int NUM_Q = 12;
    public  const int NUM_J = 11;
    public  const int NUM_10 = 10;
    public  const int NUM_9 = 9;
    public  const int NUM_8 = 8;
    public  const int NUM_7 = 7;
    public  const int NUM_6 = 6;
    public  const int NUM_5 = 5;
    public  const int NUM_4 = 4;
    public  const int NUM_3 = 3;
    public  const int NUM_2 = 2;

    // 单张牌大小
    private int number;
    // 花色
    private int flower;

    public Card() { }

    public Card(int flower, int number)
    {
        this.flower = flower;
        this.number = number;
    }

    public int getNumber()
    {
        return number;
    }

    public void setNumber(int number)
    {
        this.number = number;
    }

    public int getFlower()
    {
        return flower;
    }

    public void setFlower(int flower)
    {
        this.flower = flower;
    }

}