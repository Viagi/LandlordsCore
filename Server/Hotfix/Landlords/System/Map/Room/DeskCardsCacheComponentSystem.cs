using ETModel;

namespace ETHotfix
{
    public static class DeskCardsCacheComponentSystem
    {
        /// <summary>
        /// 获取总权值
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int GetTotalWeight(this DeskCardsCacheComponent self)
        {
            return CardsHelper.GetWeight(self.library.ToArray(), self.Rule);
        }

        /// <summary>
        /// 获取牌桌所有牌
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Card[] GetAll(this DeskCardsCacheComponent self)
        {
            return self.library.ToArray();
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <returns></returns>
        public static Card Deal(this DeskCardsCacheComponent self)
        {
            Card card = self.library[self.CardsCount - 1];
            self.library.Remove(card);
            return card;
        }

        /// <summary>
        /// 向牌库中添加牌
        /// </summary>
        /// <param name="card"></param>
        public static void AddCard(this DeskCardsCacheComponent self, Card card)
        {
            self.library.Add(card);
        }

        /// <summary>
        /// 清空牌桌
        /// </summary>
        /// <param name="self"></param>
        public static void Clear(this DeskCardsCacheComponent self)
        {
            DeckComponent deck = self.GetParent<Entity>().GetComponent<DeckComponent>();
            while (self.CardsCount > 0)
            {
                Card card = self.library[self.CardsCount - 1];
                self.library.Remove(card);
                deck.AddCard(card);
            }

            self.Rule = CardsType.None;
        }

        /// <summary>
        /// 手牌排序
        /// </summary>
        /// <param name="self"></param>
        public static void Sort(this DeskCardsCacheComponent self)
        {
            CardsHelper.SortCards(self.library);
        }
    }
}
