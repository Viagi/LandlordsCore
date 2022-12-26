using ET.Server.Landlords;
using System.Collections.Generic;

namespace ET
{
    namespace Landlords
    {
        [ObjectSystem]
        public class RoomUnitEntityAwakeSystem : AwakeSystem<RoomUnitEntity>
        {
            protected override void Awake(RoomUnitEntity self)
            {
                self.HandCards = ListComponent<HandCard>.Create();
                self.PlayCards = ListComponent<HandCard>.Create();
            }
        }

        [ObjectSystem]
        public class RoomUnitEntityDestroySystem : DestroySystem<RoomUnitEntity>
        {
            protected override void Destroy(RoomUnitEntity self)
            {
                self.HandCards.Dispose();
                self.PlayCards.Dispose();
                self.HandCards = null;
                self.PlayCards = null;
            }
        }

        [FriendOf(typeof(RoomUnitEntity))]
        public static class RoomUnitEntitySystem
        {
            public static void Reset(this RoomUnitEntity self)
            {
                self.Identity = ELandlordIdentity.None;
                self.Status = ELandlordStatus.None;
                self.IsTrust = false;
                self.HandCards.Clear();
                self.PlayCards.Clear();

                self.RemoveComponent<UnitRobotComponent>();
            }

            public static void AddCards(this RoomUnitEntity self, List<HandCard> cards)
            {
                self.HandCards.AddRange(cards);
                self.HandCards.Sort(RoomHelper.CardComparison);
            }

            public static void ShowCards(this RoomUnitEntity self, List<HandCard> cards)
            {
                self.PlayCards.Clear();
                if (cards != null)
                {
                    self.PlayCards.AddRange(cards);
                    for (int i = 0; i < cards.Count; i++)
                    {
                        self.HandCards.Remove(cards[i]);
                    }
                    self.PlayCards.Sort(RoomHelper.CardComparison);
                    self.HandCards.Sort(RoomHelper.CardComparison);
                }
            }

            public static void ShowAllCards(this RoomUnitEntity self)
            {
                if (self.HandCards.Count > 0)
                {
                    self.PlayCards.Clear();
                    self.PlayCards.AddRange(self.HandCards);
                    self.HandCards.Clear();
                }
            }
        }
    }
}
