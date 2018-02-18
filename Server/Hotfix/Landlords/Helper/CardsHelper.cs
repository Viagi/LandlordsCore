using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Model;

namespace Hotfix
{
    public static class CardsHelper
    {
        /// <summary>
        /// 获取牌组权重
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static int GetWeight(Card[] cards, CardsType rule)
        {
            int totalWeight = 0;
            if (rule == CardsType.JokerBoom)
            {
                totalWeight = int.MaxValue;
            }
            else if (rule == CardsType.Boom)
            {
                totalWeight = (int)cards[0].CardWeight * (int)cards[1].CardWeight * (int)cards[2].CardWeight * (int)cards[3].CardWeight + (int.MaxValue / 2);
            }
            else if (rule == CardsType.ThreeAndOne || rule == CardsType.ThreeAndTwo)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    if (i < cards.Length - 2)
                    {
                        if (cards[i].CardWeight == cards[i + 1].CardWeight &&
                            cards[i].CardWeight == cards[i + 2].CardWeight)
                        {
                            totalWeight += (int)cards[i].CardWeight;
                            totalWeight *= 3;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    totalWeight += (int)cards[i].CardWeight;
                }
            }

            return totalWeight;
        }

        /// <summary>
        /// 卡组排序
        /// </summary>
        /// <param name="cards"></param>
        public static void SortCards(List<Card> cards)
        {
            cards.Sort(
                (Card a, Card b) =>
                {
                    //先按照权重降序，再按花色升序
                    return -a.CardWeight.CompareTo(b.CardWeight) * 2 +
                        a.CardWeight.CompareTo(b.CardWeight);
                }
            );
        }

        /// <summary>
        /// 是否是单
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsSingle(Card[] cards)
        {
            if (cards.Length == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否是对子
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsDouble(Card[] cards)
        {
            if (cards.Length == 2)
            {
                if (cards[0].CardWeight == cards[1].CardWeight)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 是否是顺子
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsStraight(Card[] cards)
        {
            if (cards.Length < 5 || cards.Length > 12)
                return false;
            for (int i = 0; i < cards.Length - 1; i++)
            {
                Weight w = cards[i].CardWeight;
                if (w - cards[i + 1].CardWeight != 1)
                    return false;

                //不能超过A
                if (w > Weight.One || cards[i + 1].CardWeight > Weight.One)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 是否是双顺子
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsDoubleStraight(Card[] cards)
        {
            if (cards.Length < 6 || cards.Length % 2 != 0)
                return false;

            for (int i = 0; i < cards.Length; i += 2)
            {
                if (cards[i + 1].CardWeight != cards[i].CardWeight)
                    return false;

                if (i < cards.Length - 2)
                {
                    if (cards[i].CardWeight - cards[i + 2].CardWeight != 1)
                        return false;

                    //不能超过A
                    if (cards[i].CardWeight > Weight.One || cards[i + 2].CardWeight > Weight.One)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 飞机不带
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsTripleStraight(Card[] cards)
        {
            if (cards.Length < 6 || cards.Length % 3 != 0)
                return false;

            for (int i = 0; i < cards.Length; i += 3)
            {
                if (cards[i + 1].CardWeight != cards[i].CardWeight)
                    return false;
                if (cards[i + 2].CardWeight != cards[i].CardWeight)
                    return false;
                if (cards[i + 1].CardWeight != cards[i + 2].CardWeight)
                    return false;

                if (i < cards.Length - 3)
                {
                    if (cards[i].CardWeight - cards[i + 3].CardWeight != 1)
                        return false;

                    //不能超过A
                    if (cards[i].CardWeight > Weight.One || cards[i + 3].CardWeight > Weight.One)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 三不带
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsOnlyThree(Card[] cards)
        {
            if (cards.Length % 3 != 0)
                return false;
            if (cards[0].CardWeight != cards[1].CardWeight)
                return false;
            if (cards[1].CardWeight != cards[2].CardWeight)
                return false;
            if (cards[0].CardWeight != cards[2].CardWeight)
                return false;

            return true;
        }


        /// <summary>
        /// 三带一
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsThreeAndOne(Card[] cards)
        {
            if (cards.Length != 4)
                return false;

            if (cards[0].CardWeight == cards[1].CardWeight &&
                cards[1].CardWeight == cards[2].CardWeight)
                return true;
            else if (cards[1].CardWeight == cards[2].CardWeight &&
                cards[2].CardWeight == cards[3].CardWeight)
                return true;
            return false;
        }

        /// <summary>
        /// 三代二
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsThreeAndTwo(Card[] cards)
        {
            if (cards.Length != 5)
                return false;

            if (cards[0].CardWeight == cards[1].CardWeight &&
                cards[1].CardWeight == cards[2].CardWeight)
            {
                if (cards[3].CardWeight == cards[4].CardWeight)
                    return true;
            }

            else if (cards[2].CardWeight == cards[3].CardWeight &&
                cards[3].CardWeight == cards[4].CardWeight)
            {
                if (cards[0].CardWeight == cards[1].CardWeight)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 炸弹
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsBoom(Card[] cards)
        {
            if (cards.Length != 4)
                return false;

            if (cards[0].CardWeight != cards[1].CardWeight)
                return false;
            if (cards[1].CardWeight != cards[2].CardWeight)
                return false;
            if (cards[2].CardWeight != cards[3].CardWeight)
                return false;

            return true;
        }


        /// <summary>
        /// 王炸
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsJokerBoom(Card[] cards)
        {
            if (cards.Length != 2)
                return false;
            if (cards[0].CardWeight == Weight.SJoker)
            {
                if (cards[1].CardWeight == Weight.LJoker)
                    return true;
                return false;
            }
            else if (cards[0].CardWeight == Weight.LJoker)
            {
                if (cards[1].CardWeight == Weight.SJoker)
                    return true;
                return false;
            }

            return false;
        }

        /// <summary>
        /// 判断是否符合出牌规则
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool PopEnable(Card[] cards, out CardsType type)
        {
            type = CardsType.None;
            bool isRule = false;
            switch (cards?.Length)
            {
                case 1:
                    isRule = true;
                    type = CardsType.Single;
                    break;
                case 2:
                    if (IsDouble(cards))
                    {
                        isRule = true;
                        type = CardsType.Double;
                    }
                    else if (IsJokerBoom(cards))
                    {
                        isRule = true;
                        type = CardsType.JokerBoom;
                    }
                    break;
                case 3:
                    if (IsOnlyThree(cards))
                    {
                        isRule = true;
                        type = CardsType.OnlyThree;
                    }
                    break;
                case 4:
                    if (IsBoom(cards))
                    {
                        isRule = true;
                        type = CardsType.Boom;
                    }
                    else if (IsThreeAndOne(cards))
                    {
                        isRule = true;
                        type = CardsType.ThreeAndOne;
                    }

                    break;
                case 5:
                    if (IsStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.Straight;
                    }
                    else if (IsThreeAndTwo(cards))
                    {
                        isRule = true;
                        type = CardsType.ThreeAndTwo;
                    }
                    break;
                case 6:
                    if (IsStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.Straight;
                    }
                    else if (IsTripleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.TripleStraight;
                    }
                    else if (IsDoubleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.DoubleStraight;
                    }
                    break;
                case 7:
                    if (IsStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.Straight;
                    }
                    break;
                case 8:
                    if (IsStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.Straight;
                    }
                    else if (IsDoubleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.DoubleStraight;
                    }
                    break;
                case 9:
                    if (IsStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.Straight;
                    }
                    else if (IsOnlyThree(cards))
                    {
                        isRule = true;
                        type = CardsType.OnlyThree;
                    }
                    break;
                case 10:
                    if (IsStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.Straight;
                    }
                    else if (IsDoubleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.DoubleStraight;
                    }
                    break;

                case 11:
                    if (IsStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.Straight;
                    }
                    break;
                case 12:
                    if (IsStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.Straight;
                    }
                    else if (IsDoubleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.DoubleStraight;
                    }
                    else if (IsOnlyThree(cards))
                    {
                        isRule = true;
                        type = CardsType.OnlyThree;
                    }
                    break;
                case 13:
                    break;
                case 14:
                    if (IsDoubleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.DoubleStraight;
                    }
                    break;
                case 15:
                    if (IsOnlyThree(cards))
                    {
                        isRule = true;
                        type = CardsType.OnlyThree;
                    }
                    break;
                case 16:
                    if (IsDoubleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.DoubleStraight;
                    }
                    break;
                case 17:
                    break;
                case 18:
                    if (IsDoubleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.DoubleStraight;
                    }
                    else if (IsOnlyThree(cards))
                    {
                        isRule = true;
                        type = CardsType.OnlyThree;
                    }
                    break;
                case 19:
                    break;

                case 20:
                    if (IsDoubleStraight(cards))
                    {
                        isRule = true;
                        type = CardsType.DoubleStraight;
                    }
                    break;
                default:
                    break;
            }

            return isRule;
        }

        /// <summary>
        /// 提示出牌
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static async Task<List<Card[]>> GetPrompt(List<Card> cards, DeskCardsCacheComponent deskCardsCache, CardsType type)
        {
            List<Card[]> result = new List<Card[]>();
            Card[] deskCards = deskCardsCache.GetAll();
            int weight = deskCardsCache.GetTotalWeight();

            if (type == CardsType.JokerBoom)
            {
                return result;
            }

            //检索王炸
            if (cards.Count >= 2)
            {
                Card[] groupCards = new Card[2];
                groupCards[0] = cards[0];
                groupCards[1] = cards[1];

                if (IsJokerBoom(groupCards))
                {
                    result.Add(groupCards);
                }
            }

            //检索炸弹
            for (int i = cards.Count - 1; i >= 3; i--)
            {
                Card[] groupCards = new Card[4];
                groupCards[0] = cards[i - 3];
                groupCards[1] = cards[i - 2];
                groupCards[2] = cards[i - 1];
                groupCards[3] = cards[i];

                if (IsBoom(groupCards) && GetWeight(groupCards, CardsType.Boom) > weight)
                {
                    result.Add(groupCards);
                }
            }

            switch (type)
            {
                case CardsType.OnlyThree:
                    for (int i = cards.Count - 1; i >= 2; i--)
                    {
                        if (cards[i].CardWeight <= deskCards[deskCards.Length - 1].CardWeight)
                        {
                            continue;
                        }

                        Card[] groupCards = new Card[3];
                        groupCards[0] = cards[i - 2];
                        groupCards[1] = cards[i - 1];
                        groupCards[2] = cards[i];

                        if (IsOnlyThree(groupCards) && GetWeight(groupCards, type) > weight)
                        {
                            result.Add(groupCards);
                        }
                    }
                    break;
                case CardsType.ThreeAndOne:
                    if (cards.Count >= 4)
                    {
                        for (int i = cards.Count - 1; i >= 2; i--)
                        {
                            if (cards[i].CardWeight <= deskCards[deskCards.Length - 1].CardWeight)
                            {
                                continue;
                            }

                            List<Card> other = new List<Card>(cards);
                            other.RemoveRange(i - 2, 3);

                            Card[] groupCards = new Card[4];
                            groupCards[0] = cards[i - 2];
                            groupCards[1] = cards[i - 1];
                            groupCards[2] = cards[i];
                            groupCards[3] = other[RandomHelper.RandomNumber(0, other.Count)];

                            if (IsThreeAndOne(groupCards) && GetWeight(groupCards, type) > weight)
                            {
                                result.Add(groupCards);
                            }
                        }
                    }
                    break;
                case CardsType.ThreeAndTwo:
                    if (cards.Count >= 5)
                    {
                        for (int i = cards.Count - 1; i >= 2; i--)
                        {
                            if (cards[i].CardWeight <= deskCards[deskCards.Length - 1].CardWeight)
                            {
                                continue;
                            }

                            List<Card> other = new List<Card>(cards);
                            other.RemoveRange(i - 2, 3);

                            List<Card[]> otherDouble = await GetPrompt(other, deskCardsCache, CardsType.Double);
                            if (otherDouble.Count > 0)
                            {
                                Card[] randomDouble = otherDouble[RandomHelper.RandomNumber(0, otherDouble.Count)];
                                Card[] groupCards = new Card[5];
                                groupCards[0] = cards[i - 2];
                                groupCards[1] = cards[i - 1];
                                groupCards[2] = cards[i];
                                groupCards[3] = randomDouble[0];
                                groupCards[4] = randomDouble[1];

                                if (IsThreeAndTwo(groupCards) && GetWeight(groupCards, type) > weight)
                                {
                                    result.Add(groupCards);
                                }
                            }
                        }
                    }
                    break;
                case CardsType.Straight:
                    /*
                     * 7 6 5 4 3
                     * 8 7 6 5 4
                     * 
                     * */
                    if (cards.Count >= deskCards.Length)
                    {
                        for (int i = cards.Count - 1; i >= deskCards.Length - 1; i--)
                        {
                            if (cards[i].CardWeight <= deskCards[deskCards.Length - 1].CardWeight)
                            {
                                continue;
                            }

                            //是否全部搜索完成
                            bool isTrue = true;
                            Card[] groupCards = new Card[deskCards.Length];
                            for (int j = 0; j < deskCards.Length; j++)
                            {
                                //搜索连续权重牌
                                Card findCard = cards.Where(card => (int)card.CardWeight == (int)cards[i].CardWeight + j).FirstOrDefault();
                                if (findCard == null)
                                {
                                    isTrue = false;
                                    break;
                                }
                                groupCards[deskCards.Length - 1 - j] = findCard;
                            }

                            if (isTrue && IsStraight(groupCards) && GetWeight(groupCards, type) > weight)
                            {
                                result.Add(groupCards);
                            }
                        }
                    }
                    break;
                case CardsType.DoubleStraight:
                    /*
                     * 5 5 4 4 3 3
                     * 6 6 5 5 4 4
                     * 
                     * */
                    if (cards.Count >= deskCards.Length)
                    {
                        for (int i = cards.Count - 1; i >= deskCards.Length - 1; i--)
                        {
                            if (cards[i].CardWeight <= deskCards[deskCards.Length - 1].CardWeight)
                            {
                                continue;
                            }

                            //是否全部搜索完成
                            bool isTrue = true;
                            Card[] groupCards = new Card[deskCards.Length];
                            for (int j = 0; j < deskCards.Length; j += 2)
                            {
                                //搜索连续权重牌
                                Card[] findCards = cards.Where(card => (int)card.CardWeight == (int)cards[i].CardWeight + (j / 2)).ToArray();
                                if (findCards.Length < 2)
                                {
                                    isTrue = false;
                                    break;
                                }
                                groupCards[deskCards.Length - 2 - j] = findCards[0];
                                groupCards[deskCards.Length - 1 - j] = findCards[1];
                            }

                            if (isTrue && IsDoubleStraight(groupCards) && GetWeight(groupCards, type) > weight)
                            {
                                result.Add(groupCards);
                            }
                        }
                    }
                    break;
                case CardsType.TripleStraight:
                    if (cards.Count >= deskCards.Length)
                    {
                        for (int i = cards.Count - 1; i >= deskCards.Length - 1; i--)
                        {
                            if (cards[i].CardWeight <= deskCards[deskCards.Length - 1].CardWeight)
                            {
                                continue;
                            }

                            //是否全部搜索完成
                            bool isTrue = true;
                            Card[] groupCards = new Card[deskCards.Length];
                            for (int j = 0; j < deskCards.Length; j += 3)
                            {
                                //搜索连续权重牌
                                Card[] findCards = cards.Where(card => (int)card.CardWeight == (int)cards[i].CardWeight + (j / 3)).ToArray();
                                if (findCards.Length < 3)
                                {
                                    isTrue = false;
                                    break;
                                }
                                groupCards[deskCards.Length - 3 - j] = findCards[0];
                                groupCards[deskCards.Length - 2 - j] = findCards[1];
                                groupCards[deskCards.Length - 1 - j] = findCards[2];
                            }

                            if (isTrue && IsTripleStraight(groupCards) && GetWeight(groupCards, type) > weight)
                            {
                                result.Add(groupCards);
                            }
                        }
                    }
                    break;
                case CardsType.Double:
                    if (cards.Count >= 2)
                    {
                        for (int i = cards.Count - 1; i >= 1; i--)
                        {
                            Card[] groupCards = new Card[2];
                            groupCards[0] = cards[i - 1];
                            groupCards[1] = cards[i];

                            if (IsDouble(groupCards) && GetWeight(groupCards, type) > weight)
                            {
                                result.Add(groupCards);
                            }
                        }
                    }
                    break;
                case CardsType.Single:
                    if (cards.Count >= 1)
                    {
                        for (int i = cards.Count - 1; i >= 0; i--)
                        {
                            if (cards[i].CardWeight <= deskCards[deskCards.Length - 1].CardWeight)
                            {
                                continue;
                            }

                            Card[] groupCards = new Card[1];
                            groupCards[0] = cards[i];

                            if (IsSingle(groupCards) && GetWeight(groupCards, type) > weight)
                            {
                                result.Add(groupCards);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
