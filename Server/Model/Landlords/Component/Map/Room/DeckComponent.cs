using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 牌库组件
    /// </summary>
    public class DeckComponent : Component
    {
        //牌库中的牌
        public readonly List<Card> library = new List<Card>();

        //牌库中的总牌数
        public int CardsCount { get { return this.library.Count; } }

        public override void Dispose()
        {
            if(this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            library.Clear();
        }
    }
}
