using System;
using ProtoBuf;

namespace Model
{
    /// <summary>
    /// 牌类
    /// </summary>
    [ProtoContract]
    public class Card : IEquatable<Card>
    {
        //牌权值
        [ProtoMember(1)]
        public Weight CardWeight { get; private set; }

        //牌花色
        [ProtoMember(2)]
        public Suits CardSuits { get; private set; }

        public Card()
        {
            //protobuf反序列化必须要有一个无参构造函数
        }

        public Card(Weight weight, Suits suits)
        {
            this.CardWeight = weight;
            this.CardSuits = suits;
        }

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
