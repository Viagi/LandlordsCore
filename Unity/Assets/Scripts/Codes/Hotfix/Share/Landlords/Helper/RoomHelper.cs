using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    namespace Landlords
    {
        public static class RoomHelper
        {
            public static int CardComparison(HandCard a, HandCard b)
            {
                return -a.CompareTo(b);
            }

            public static void AnalysisCards(List<HandCard> cards, out CardGroupType groupType, out HandCard weightCard)
            {
                groupType = CardGroupType.None;
                weightCard = null;

                if (cards == null)
                    return;

                bool doubleJokers;
                SortedDictionary<HandCard, int> result = ObjectPool.Instance.Fetch<SortedDictionary<HandCard, int>>();

                StatisticCards(cards, result, out weightCard, out doubleJokers);

                if (cards.Count == 1)
                {
                    groupType = CardGroupType.One;
                }
                else if (cards.Count == 2)
                {
                    if (cards[0].Number == CardNumber.Joker && cards[1].Number == CardNumber.Joker)
                        groupType = CardGroupType.JokerBomb;
                    else if (cards[0].Number == cards[1].Number)
                        groupType = CardGroupType.Two;
                }
                else if (cards.Count == 3 && cards.TrueForAll((card) => card == cards[0]))
                {
                    groupType = CardGroupType.Three;
                }
                else if (cards.Count == 4)
                {
                    int same = 0;
                    for (int i = 0; i < 4; i++)
                        if (cards[i] == cards[0])
                            same++;
                    if (same == 3)
                        groupType = CardGroupType.ThreeAndOne;
                    else if (same == 4)
                        groupType = CardGroupType.Bomb;
                }
                else if (cards.Count == 5 && !doubleJokers)
                {
                    if (result.ContainsValue(3) && result.ContainsValue(2))
                    {
                        groupType = CardGroupType.ThreeAndTwo;
                    }
                }
                else if (cards.Count == 6 && !doubleJokers)
                {
                    if (result.ContainsValue(4) && result.Count((pair) => pair.Value == 1) == 2)
                    {
                        groupType = CardGroupType.FourAndTwo;
                    }
                }
                if (groupType == CardGroupType.None && cards.Count % 4 == 0 && !doubleJokers)
                {
                    HandCard card = null;
                    int continuous = 0;
                    foreach (var pair in result)
                    {
                        if (pair.Value == 3 && card == null)
                        {
                            card = pair.Key;
                            continuous++;
                        }
                        else if (pair.Value == 3 && card != null)
                        {
                            if (pair.Key - card == 1)
                            {
                                continuous++;
                                card = pair.Key;
                            }
                            else
                            {
                                continuous = 0;
                                break;
                            }
                        }
                    }
                    if (continuous > 0 && result.Count((pair) => pair.Value == 1) == continuous)
                    {
                        groupType = CardGroupType.AirplaneAndOne;
                    }
                }
                if (groupType == CardGroupType.None && cards.Count % 5 == 0 && !doubleJokers)
                {
                    HandCard card = null;
                    int continuous = 0;
                    foreach (var pair in result)
                    {
                        if (pair.Value == 3 && card == null)
                        {
                            card = pair.Key;
                            continuous++;
                        }
                        else if (pair.Value == 3 && card != null)
                        {
                            if (pair.Key - card == 1)
                            {
                                continuous++;
                                card = pair.Key;
                            }
                            else
                            {
                                continuous = 0;
                                break;
                            }
                        }
                    }
                    if (continuous > 0 && result.Count((pair) => pair.Value == 2) == continuous)
                        groupType = CardGroupType.AirplaneAndTwo;
                }

                if (groupType == CardGroupType.None && !doubleJokers)
                {
                    HandCard card = null;
                    int continuous = 0;
                    foreach (var pair in result)
                    {
                        if (card == null)
                        {
                            card = pair.Key;
                            continuous++;
                        }
                        else if (card != null)
                        {
                            if (result[card] == pair.Value && pair.Key - card == 1)
                            {
                                card = pair.Key;
                                continuous++;
                            }
                            else
                            {
                                continuous = 0;
                                break;
                            }
                        }
                    }

                    if (continuous >= 5 && result[card] == 1)
                    {
                        groupType = CardGroupType.Straight;
                    }
                    else if (continuous >= 3 && result[card] == 2)
                    {
                        groupType = CardGroupType.TwoStraight;
                    }
                    else if (continuous >= 2 && result[card] == 3)
                    {
                        groupType = CardGroupType.ThreeStraight;
                    }
                }

                result.Clear();
                ObjectPool.Instance.Recycle(result);
            }

            public static ListComponent<HandCard> SearchCards(List<HandCard> playCards, List<HandCard> handCards)
            {
                ListComponent<HandCard> results = ListComponent<HandCard>.Create();
                if (playCards.Count == 0)
                {
                    HandCard weightCard;
                    bool doubleJokers;
                    SortedDictionary<HandCard, int> cards = ObjectPool.Instance.Fetch<SortedDictionary<HandCard, int>>();
                    StatisticCards(handCards, cards, out weightCard, out doubleJokers);

                    var first = cards.First();
                    if (first.Value == 1)
                    {
                        HandCard nextCard = first.Key;
                        for (int i = handCards.Count - 1; i > 0; i--)
                        {
                            if (handCards[i] == nextCard)
                            {
                                results.Add(handCards[i]);
                                nextCard = new HandCard() { Number = nextCard.Number + 1, Type = nextCard.Type };
                            }
                        }
                        if (results.Count < 5)
                        {
                            results.Clear();
                            results.Add(first.Key);
                        }
                    }
                    else if (first.Value == 2)
                    {
                        HandCard nextCard = first.Key;
                        int num = 0;
                        for (int i = handCards.Count - 1; i > 0; i--)
                        {
                            if (handCards[i] == nextCard && num < 2 && cards[handCards[i]] >= 2)
                            {
                                results.Add(handCards[i]);
                                num++;
                                if (num == 2)
                                {
                                    nextCard = new HandCard() { Number = nextCard.Number + 1, Type = nextCard.Type };
                                    num = 0;
                                }
                            }
                        }
                        if (results.Count / 2 < 3)
                        {
                            results.Clear();
                            num = 0;
                            for (int i = handCards.Count - 1; i > 0; i--)
                            {
                                if (handCards[i] == first.Key && num < 2)
                                {
                                    results.Add(handCards[i]);
                                    num++;
                                    if (num == 2)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (first.Value == 3)
                    {
                        HandCard nextCard = first.Key;
                        int num = 0;
                        int continuous = 0;
                        for (int i = handCards.Count - 1; i > 0; i--)
                        {
                            if (handCards[i] == nextCard && num < 3 && cards[handCards[i]] >= 3)
                            {
                                results.Add(handCards[i]);
                                num++;
                                if (num == 3)
                                {
                                    nextCard = new HandCard() { Number = nextCard.Number + 1, Type = nextCard.Type };
                                    num = 0;
                                    continuous++;
                                }
                            }
                        }
                        if (results.Count / 3 >= 2)
                        {
                            using (ListComponent<HandCard> attachCards = ListComponent<HandCard>.Create())
                            {
                                HandCard attachCard = null;
                                int attachNum = 0;
                                num = 0;
                                for (int i = handCards.Count - 1; i > 0; i--)
                                {
                                    bool exist = handCards[i].Number >= first.Key.Number
                                        && handCards[i].Number <= first.Key.Number + (ushort)continuous;
                                    if (exist) continue;
                                    if ((attachCard == null || handCards[i] != attachCard)
                                        && cards[handCards[i]] >= 2
                                        && handCards[i].Number != CardNumber.Joker)
                                    {
                                        if (num == continuous)
                                            break;
                                        attachCard = handCards[i];
                                        attachCards.Add(handCards[i]);
                                        attachNum = 1;
                                        num++;
                                    }
                                    else if (handCards[i] == attachCard && attachNum < 2)
                                    {
                                        attachCards.Add(handCards[i]);
                                        attachNum++;
                                    }
                                }

                                if (num != continuous)
                                {
                                    attachCards.Clear();
                                    attachCard = null;
                                    num = 0;
                                    for (int i = handCards.Count - 1; i > 0; i--)
                                    {
                                        bool exist = handCards[i].Number >= first.Key.Number
                                       && handCards[i].Number <= first.Key.Number + (ushort)continuous;
                                        if (exist) continue;
                                        if (attachCard == null || handCards[i] != attachCard)
                                        {
                                            if (num == continuous)
                                                break;
                                            attachCard = handCards[i];
                                            attachCards.Add(handCards[i]);
                                            num++;
                                        }
                                    }
                                }

                                if (num == continuous)
                                {
                                    results.AddRange(attachCards);
                                }
                            }
                        }
                    }
                    else if (first.Value == 4)
                    {
                        HandCard attachCard = null;
                        int attachNum = 0;
                        for (int i = handCards.Count - 1; i > 0; i--)
                        {
                            if (handCards[i] == first.Key)
                            {
                                results.Add(handCards[i]);
                            }
                            else if (cards[handCards[i]] >= 2
                                && attachCard == null
                                && handCards[i].Number != CardNumber.Joker)
                            {
                                results.Add(handCards[i]);
                                attachCard = handCards[i];
                                attachNum++;
                            }
                            else if (handCards[i] == attachCard && attachNum < 2)
                            {
                                results.Add(handCards[i]);
                                attachNum++;
                            }
                        }
                    }

                    cards.Clear();
                    ObjectPool.Instance.Recycle(cards);
                }
                else
                {
                    HandCard weightCard;
                    bool doubleJokers;
                    SortedDictionary<HandCard, int> cards = ObjectPool.Instance.Fetch<SortedDictionary<HandCard, int>>();
                    StatisticCards(handCards, cards, out weightCard, out doubleJokers);

                    CardGroupType currentType;
                    HandCard currentWeightCard;
                    AnalysisCards(playCards, out currentType, out currentWeightCard);

                    if (handCards.Count >= playCards.Count)
                    {
                        switch (currentType)
                        {
                            case CardGroupType.One:
                                foreach (HandCard card in cards.Keys)
                                {
                                    if (card.Number > currentWeightCard.Number)
                                    {
                                        results.Add(card);
                                        break;
                                    }
                                }
                                break;
                            case CardGroupType.Two:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 2 && card.Key > currentWeightCard)
                                    {
                                        int num = 0;
                                        for (int i = 0; i < handCards.Count; i++)
                                        {
                                            if (handCards[i].Number == card.Key.Number)
                                            {
                                                results.Add(handCards[i]);
                                                num++;
                                                if (num == 2)
                                                    break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            case CardGroupType.Three:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 3 && card.Key > currentWeightCard)
                                    {
                                        int num = 0;
                                        for (int i = handCards.Count - 1; i >= 0; i--)
                                        {
                                            if (handCards[i].Number == card.Key.Number)
                                            {
                                                results.Add(handCards[i]);
                                                num++;
                                                if (num == 3)
                                                    break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            case CardGroupType.ThreeAndOne:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 3 && card.Key > currentWeightCard)
                                    {
                                        int num = 0;
                                        int attachNum = 0;
                                        for (int i = handCards.Count - 1; i >= 0; i--)
                                        {
                                            if (handCards[i].Number == card.Key.Number && num < 3)
                                            {
                                                results.Add(handCards[i]);
                                                num++;
                                            }
                                            else if (handCards[i].Number != card.Key.Number && attachNum < 1)
                                            {
                                                results.Add(handCards[i]);
                                                attachNum++;
                                            }
                                        }
                                        if (attachNum == 1)
                                            break;
                                        else
                                            results.Clear();
                                    }
                                }
                                break;
                            case CardGroupType.ThreeAndTwo:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 3 && card.Key > currentWeightCard)
                                    {
                                        int num = 0;
                                        HandCard twoCard = null;
                                        int attachNum = 0;
                                        for (int i = handCards.Count - 1; i >= 0; i--)
                                        {
                                            if (handCards[i].Number == card.Key.Number && num < 3)
                                            {
                                                results.Add(handCards[i]);
                                                num++;
                                            }
                                            else if (handCards[i].Number != card.Key.Number
                                                && twoCard == null
                                                && handCards[i].Number != CardNumber.Joker
                                                && cards[handCards[i]] >= 2)
                                            {
                                                results.Add(handCards[i]);
                                                twoCard = handCards[i];
                                                attachNum++;
                                            }
                                            else if (twoCard != null && attachNum < 2 && handCards[i].Number == twoCard.Number)
                                            {
                                                results.Add(handCards[i]);
                                                attachNum++;
                                            }
                                        }
                                        if (attachNum == 2)
                                            break;
                                        else
                                            results.Clear();
                                    }
                                }
                                break;
                            case CardGroupType.FourAndTwo:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 4 && card.Key > currentWeightCard)
                                    {
                                        int num = 0;
                                        HandCard attachCard = null;
                                        int attachNum = 0;
                                        for (int i = handCards.Count - 1; i >= 0; i--)
                                        {
                                            if (handCards[i].Number == card.Key.Number && num < 4)
                                            {
                                                results.Add(handCards[i]);
                                                num++;
                                            }
                                            else if (handCards[i].Number != card.Key.Number && attachCard == null)
                                            {
                                                results.Add(handCards[i]);
                                                attachCard = handCards[i];
                                                attachNum++;
                                            }
                                            else if (attachCard != null
                                                && attachNum < 2
                                                && handCards[i].Number != attachCard.Number)
                                            {
                                                results.Add(handCards[i]);
                                                attachNum++;
                                            }
                                        }
                                        if (attachNum == 2)
                                            break;
                                        else
                                            results.Clear();
                                    }
                                }
                                break;
                            case CardGroupType.AirplaneAndOne:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 3 && card.Key > currentWeightCard)
                                    {
                                        int continuous = 0;
                                        for (int i = 0; i < playCards.Count / 4; i++)
                                        {
                                            HandCard nextCard = new HandCard() { Number = card.Key.Number + (ushort)i, Type = card.Key.Type };
                                            int cardNum;
                                            if (cards.TryGetValue(nextCard, out cardNum) && cardNum >= 3)
                                            {
                                                continuous++;
                                            }
                                        }
                                        if (continuous == playCards.Count / 4)
                                        {
                                            HandCard nextCard = card.Key;
                                            HandCard attachCard = null;
                                            int num = 0;
                                            int attachNum = 0;
                                            continuous = 0;
                                            for (int i = handCards.Count - 1; i >= 0; i--)
                                            {
                                                bool exist = handCards[i].Number >= card.Key.Number
                                                    && handCards[i].Number <= card.Key.Number + (ushort)(playCards.Count / 4);
                                                if (handCards[i].Number == nextCard.Number
                                                    && num < 3
                                                    && continuous < playCards.Count / 4)
                                                {
                                                    results.Add(handCards[i]);
                                                    num++;
                                                    if (num == 3)
                                                    {
                                                        nextCard = new HandCard() { Number = nextCard.Number + 1, Type = nextCard.Type };
                                                        num = 0;
                                                        continuous++;
                                                    }
                                                }
                                                else if (!exist
                                                    && handCards[i] != attachCard
                                                    && attachNum < playCards.Count / 4)
                                                {
                                                    results.Add(handCards[i]);
                                                    attachCard = handCards[i];
                                                    attachNum++;
                                                }
                                            }
                                            if (attachNum == playCards.Count / 4)
                                                break;
                                            else
                                                results.Clear();
                                        }
                                    }
                                }
                                break;
                            case CardGroupType.AirplaneAndTwo:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 3 && card.Key > currentWeightCard)
                                    {
                                        int continuous = 0;
                                        for (int i = 0; i < playCards.Count / 5; i++)
                                        {
                                            HandCard nextCard = new HandCard() { Number = card.Key.Number + (ushort)i, Type = card.Key.Type };
                                            int cardNum;
                                            if (cards.TryGetValue(nextCard, out cardNum) && cardNum >= 3)
                                            {
                                                continuous++;
                                            }
                                        }
                                        if (continuous == playCards.Count / 5)
                                        {
                                            HandCard nextCard = card.Key;
                                            int num = 0;
                                            int attachNum = 0;
                                            int attachTwo = 0;
                                            HandCard attachCard = null;
                                            continuous = 0;
                                            for (int i = handCards.Count - 1; i >= 0; i--)
                                            {
                                                bool exist = handCards[i].Number >= card.Key.Number
                                                    && handCards[i].Number <= card.Key.Number + (ushort)(playCards.Count / 5);
                                                if (handCards[i].Number == nextCard.Number
                                                    && num < 3
                                                    && continuous < playCards.Count / 5)
                                                {
                                                    results.Add(handCards[i]);
                                                    num++;
                                                    if (num == 3)
                                                    {
                                                        nextCard = new HandCard() { Number = nextCard.Number + 1, Type = nextCard.Type };
                                                        num = 0;
                                                        continuous++;
                                                    }
                                                }
                                                else if (!exist
                                                    && handCards[i] != attachCard
                                                    && handCards[i].Number != CardNumber.Joker
                                                    && cards[handCards[i]] >= 2
                                                    && attachTwo < playCards.Count / 5)
                                                {
                                                    results.Add(handCards[i]);
                                                    attachCard = handCards[i];
                                                    attachTwo++;
                                                    attachNum = 1;
                                                }
                                                else if (handCards[i] == attachCard && attachNum < 2)
                                                {
                                                    results.Add(handCards[i]);
                                                    attachNum++;
                                                }
                                            }
                                            if (attachTwo == playCards.Count / 5)
                                                break;
                                            else
                                                results.Clear();
                                        }
                                    }
                                }
                                break;
                            case CardGroupType.Straight:
                                foreach (var card in cards)
                                {
                                    if (card.Key > currentWeightCard)
                                    {
                                        int continuous = 0;
                                        for (int i = 0; i < playCards.Count; i++)
                                        {
                                            HandCard nextCard = new HandCard() { Number = card.Key.Number + (ushort)i, Type = card.Key.Type };
                                            if (cards.ContainsKey(nextCard))
                                            {
                                                continuous++;
                                                if (continuous == playCards.Count)
                                                    break;
                                            }
                                        }
                                        if (continuous == playCards.Count)
                                        {
                                            HandCard nextCard = card.Key;
                                            continuous = 0;
                                            for (int i = handCards.Count - 1; i >= 0; i--)
                                            {
                                                if (handCards[i].Number == nextCard.Number)
                                                {
                                                    results.Add(handCards[i]);
                                                    nextCard = new HandCard() { Number = nextCard.Number + 1, Type = nextCard.Type };
                                                    continuous++;
                                                    if (continuous == playCards.Count)
                                                        break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                break;
                            case CardGroupType.TwoStraight:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 2 && card.Key > currentWeightCard)
                                    {
                                        int continuous = 0;
                                        for (int i = 0; i < playCards.Count / 2; i++)
                                        {
                                            HandCard nextCard = new HandCard() { Number = card.Key.Number + (ushort)i, Type = card.Key.Type };
                                            int cardNum;
                                            if (cards.TryGetValue(nextCard, out cardNum) && cardNum >= 2)
                                            {
                                                continuous++;
                                                if (continuous == playCards.Count / 1)
                                                    break;
                                            }
                                        }
                                        if (continuous == playCards.Count / 2)
                                        {
                                            HandCard nextCard = card.Key;
                                            int num = 0;
                                            continuous = 0;
                                            for (int i = handCards.Count - 1; i >= 0; i--)
                                            {
                                                if (handCards[i].Number == nextCard.Number && num < 2)
                                                {
                                                    results.Add(handCards[i]);
                                                    num++;
                                                    if (num == 2)
                                                    {
                                                        nextCard = new HandCard() { Number = nextCard.Number + 1, Type = nextCard.Type };
                                                        num = 0;
                                                        continuous++;
                                                        if (continuous == playCards.Count / 2)
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                break;
                            case CardGroupType.ThreeStraight:
                                foreach (var card in cards)
                                {
                                    if (card.Value >= 3 && card.Key > currentWeightCard)
                                    {
                                        int continuous = 0;
                                        for (int i = 0; i < playCards.Count / 3; i++)
                                        {
                                            HandCard nextCard = new HandCard() { Number = card.Key.Number + (ushort)i, Type = card.Key.Type };
                                            int cardNum;
                                            if (cards.TryGetValue(nextCard, out cardNum) && cardNum >= 3)
                                            {
                                                continuous++;
                                                if (continuous == playCards.Count / 3)
                                                    break;
                                            }
                                        }
                                        if (continuous == playCards.Count / 3)
                                        {
                                            HandCard nextCard = card.Key;
                                            int num = 0;
                                            continuous = 0;
                                            for (int i = handCards.Count - 1; i >= 0; i--)
                                            {
                                                if (handCards[i].Number == nextCard.Number && num < 3)
                                                {
                                                    results.Add(handCards[i]);
                                                    num++;
                                                    if (num == 3)
                                                    {
                                                        nextCard = new HandCard() { Number = nextCard.Number + 1, Type = nextCard.Type };
                                                        num = 0;
                                                        continuous++;
                                                        if (continuous == playCards.Count / 3)
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (results.Count == 0)
                    {
                        if (currentType < CardGroupType.Bomb)
                            currentWeightCard = new HandCard();
                        foreach (var card in cards)
                        {
                            if (card.Value >= 4 && card.Key > currentWeightCard)
                            {
                                int num = 0;
                                for (int i = handCards.Count - 1; i >= 0; i--)
                                {
                                    if (handCards[i].Number == card.Key.Number)
                                    {
                                        results.Add(handCards[i]);
                                        num++;
                                        if (num == 4)
                                            break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    if (results.Count == 0 && doubleJokers)
                    {
                        for (int i = handCards.Count - 1; i >= 0; i--)
                        {
                            if (handCards[i].Number == CardNumber.Joker)
                            {
                                results.Add(handCards[i]);
                            }
                        }
                    }
                    cards.Clear();
                    ObjectPool.Instance.Recycle(cards);
                }

                return results;
            }

            private static void StatisticCards(List<HandCard> cards, SortedDictionary<HandCard, int> result, out HandCard weightCard, out bool doubleJokers)
            {
                weightCard = null;
                int jokerNum = 0;
                for (int i = 0; i < cards.Count; i++)
                {
                    HandCard card = cards[i];
                    int num;
                    if (!result.TryGetValue(card, out num))
                    {
                        result.Add(card, 1);
                    }
                    else
                    {
                        result[card] = num + 1;
                    }

                    if (card.Number == CardNumber.Joker)
                    {
                        jokerNum++;
                    }
                    if (weightCard == null || (result[card] >= result[weightCard] && card < weightCard))
                    {
                        weightCard = card;
                    }
                }

                doubleJokers = jokerNum == 2;
            }
        }
    }
}
