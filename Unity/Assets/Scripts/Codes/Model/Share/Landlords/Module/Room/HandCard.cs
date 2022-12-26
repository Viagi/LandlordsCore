using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;
using System;

namespace ET
{
    namespace Landlords
    {
        public enum CardNumber : ushort
        {
            None = 0,
            Three,      //3
            Four,       //4
            Five,       //5
            Six,        //6
            Seven,      //7
            Eight,      //8
            Nine,       //9
            Ten,        //10
            Jack,       //J
            Queen,      //Q
            King,       //K
            One,        //A
            Two = 20,   //2
            Joker,      //王牌
        }

        public enum CardType : ushort
        {
            None = 0,
            Spade,      //黑桃
            Heart,      //红桃
            Club,       //梅花
            Diamond,    //方块
            SJoker,     //大王
            LJoker,     //小王
        }

        /*
         * 带牌只能带单王，不能带双王
         */
        public enum CardGroupType : ushort
        {
            None = 0,
            One,            //单牌
            Two,            //对子
            Three,          //三张
            ThreeAndOne,    //三带一
            ThreeAndTwo,    //三带一对
            FourAndTwo,     //四带二(两张单牌),不算炸弹
            AirplaneAndOne, //飞机带同数量单牌
            AirplaneAndTwo, //飞机带同数量对子
            Straight,       //顺子,5张或以上连续牌,2和双王除外
            TwoStraight,    //双顺,三对或以上连续牌
            ThreeStraight,  //三顺,两对三张或以上连续牌
            Bomb,           //炸弹,四张相同牌
            JokerBomb       //王炸,双王
        }

        [ProtoContract]
        [Serializable]
        public class HandCard : IComparable<HandCard>, IEquatable<HandCard>
        {
            [BsonElement]
            [ProtoMember(1)]
            public CardNumber Number;

            [BsonElement]
            [ProtoMember(2)]
            public CardType Type;

            public int CompareTo(HandCard other)
            {
                if (Number == CardNumber.Joker && other.Number == CardNumber.Joker)
                    return Type.CompareTo(other.Type);
                else
                    return Number.CompareTo(other.Number);
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public bool Equals(HandCard other)
            {
                return Number == other?.Number && Type == other?.Type;
            }

            public override int GetHashCode()
            {
                return ((int)Type * 100 + (int)Number).GetHashCode();
            }

            public override string ToString()
            {
                if (Number == CardNumber.Joker)
                {
                    return Type.ToString();
                }
                else
                {
                    return $"{Type}{Number}";
                }
            }

            public static bool operator >(HandCard a, HandCard b)
            {
                if (a.Number == CardNumber.Joker && b.Number == CardNumber.Joker)
                    return a.Type > b.Type;
                else
                    return a.Number > b.Number;
            }

            public static bool operator <(HandCard a, HandCard b)
            {
                if (a.Number == CardNumber.Joker && b.Number == CardNumber.Joker)
                    return a.Type < b.Type;
                else
                    return a.Number < b.Number;
            }

            public static bool operator ==(HandCard a, HandCard b)
            {
                bool aNull = Equals(a, null);
                bool bNull = Equals(b, null);
                if (!aNull && !bNull)
                    return a.Number == b.Number;
                else if (aNull && bNull)
                    return true;
                else
                    return false;
            }

            public static bool operator !=(HandCard a, HandCard b)
            {
                bool aNull = Equals(a, null);
                bool bNull = Equals(b, null);
                if (!aNull && !bNull)
                    return a.Number != b.Number;
                else if (aNull && bNull)
                    return false;
                else
                    return true;
            }

            public static int operator -(HandCard a, HandCard b)
            {
                return (int)a.Number - (int)b.Number;
            }

            public static int operator +(HandCard a, HandCard b)
            {
                return (int)a.Number + (int)b.Number;
            }
        }
    }
}
