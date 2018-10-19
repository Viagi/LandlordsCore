using System;
using Google.Protobuf;

namespace ETModel
{
    /// <summary>
    /// 牌类
    /// </summary>
    public partial class Card : IEquatable<Card>
    {
        public bool Equals(Card other)
        {
            return this.CardWeight == other.CardWeight && this.CardSuits == other.CardSuits;
        }

        /// <summary>
        /// 获取卡牌名
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.CardSuits == Suits.None ? this.CardWeight.ToString() : $"{this.CardSuits.ToString()}{this.CardWeight.ToString()}";
        }
    }
}
