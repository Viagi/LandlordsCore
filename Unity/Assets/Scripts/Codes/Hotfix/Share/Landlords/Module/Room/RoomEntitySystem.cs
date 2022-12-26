using System.Collections.Generic;

namespace ET
{
    namespace Landlords
    {
        [FriendOf(typeof(RoomEntity))]
        public static class RoomEntitySystem
        {
            public static void Add(this RoomEntity self, RoomUnitEntity unit)
            {
                for (int i = 0; i < self.Seats.Count; i++)
                {
                    if (self.Seats[i] == 0)
                    {
                        self.Seats[i] = unit.Id;
                        unit.Index = i;
                        break;
                    }
                }
            }

            public static void Remove(this RoomEntity self, long userId)
            {
                for (int i = 0; i < self.Seats.Count; i++)
                {
                    if (self.Seats[i] == userId)
                    {
                        self.Seats[i] = 0;
                        break;
                    }
                }
            }

            public static RoomUnitEntity Get(this RoomEntity self, int index)
            {
                if(index < 0)
                {
                    return null;
                }

                return self.GetChild<RoomUnitEntity>(self.Seats[index % 3]);
            }

            public static RoomUnitEntity GetCurrent(this RoomEntity self)
            {
                return self.Get(self.Current);
            }

            public static RoomUnitEntity GetActive(this RoomEntity self)
            {
                return self.Get(self.Active);
            }

            public static void SetLandlordCards(this RoomEntity self, params HandCard[] cards)
            {
                self.LandlordCards.Clear();
                self.LandlordCards.AddRange(cards);
                self.LandlordCards.Sort(RoomHelper.CardComparison);
            }

            public static bool CheckPlayCards(this RoomEntity self, RoomUnitEntity unit, List<HandCard> cards, out CardGroupType type)
            {
                HandCard weightCard;
                RoomHelper.AnalysisCards(cards, out type, out weightCard);
                RoomUnitEntity activeUnit = self.GetActive();

                if (type == CardGroupType.None)
                {
                    //不符合牌型
                    return false;
                }
                else if (type == CardGroupType.JokerBomb)
                {
                    //王炸
                    return true;
                }
                else if (activeUnit == unit && cards.Count > 0)
                {
                    //先手出牌
                    return true;
                }
                else if (activeUnit != unit && cards.Count == 0)
                {
                    //不出牌
                    return true;
                }

                CardGroupType activeType;
                HandCard activeWeightCard;
                RoomHelper.AnalysisCards(activeUnit.PlayCards, out activeType, out activeWeightCard);

                if (type != activeType && type == CardGroupType.Bomb)
                {
                    //炸弹
                    return true;
                }
                else
                {
                    //比较牌型大小
                    return weightCard > activeWeightCard;
                }
            }

            public static long GetMoney(this RoomEntity self)
            {
                return self.BaseMoney * self.BaseRate * self.Rate;
            }
        }
    }
}
