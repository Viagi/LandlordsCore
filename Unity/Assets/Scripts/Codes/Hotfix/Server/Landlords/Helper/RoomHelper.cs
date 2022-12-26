using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        public static class RoomHelper
        {
            public static ListComponent<HandCard> Shuffle()
            {
                ListComponent<HandCard> cards = ListComponent<HandCard>.Create();
                cards.Capacity = 54;

                //随机插入手牌
                for (CardType i = CardType.Spade; i <= CardType.Diamond; i++)
                {
                    for (CardNumber j = CardNumber.Three; j <= CardNumber.One; j++)
                    {
                        cards.Insert(RandomGenerator.RandomNumber(0, cards.Count), new HandCard() { Type = i, Number = j });
                    }
                }
                cards.Insert(RandomGenerator.RandomNumber(0, cards.Count), new HandCard() { Type = CardType.Spade, Number = CardNumber.Two });
                cards.Insert(RandomGenerator.RandomNumber(0, cards.Count), new HandCard() { Type = CardType.Heart, Number = CardNumber.Two });
                cards.Insert(RandomGenerator.RandomNumber(0, cards.Count), new HandCard() { Type = CardType.Club, Number = CardNumber.Two });
                cards.Insert(RandomGenerator.RandomNumber(0, cards.Count), new HandCard() { Type = CardType.Diamond, Number = CardNumber.Two });
                cards.Insert(RandomGenerator.RandomNumber(0, cards.Count), new HandCard() { Type = CardType.SJoker, Number = CardNumber.Joker });
                cards.Insert(RandomGenerator.RandomNumber(0, cards.Count), new HandCard() { Type = CardType.LJoker, Number = CardNumber.Joker });

                return cards;
            }
        }
    }
}
