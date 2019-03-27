using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Card
    {
        // т.к. эти свойства задаются один раз при инициализации класса и не изменяются в дальнейшем, 
        // можно убрать метод set, тем самым формально сделав их "readonly"
        public CardValue Value { get; }
        public CardSuit Suit { get; }

        public Card(CardSuit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        //можно вот так
        public bool IsTrump(CardSuit trump) => Suit == trump;

        public static bool operator >(Card card1, Card card2)
        {
            switch (CardsComparer.Compare(card1, card2))
            {
                case 1:
                    return true;
                case 0:
                case -1:
                default:
                    return false;
            }
        }

        public static bool operator <(Card card1, Card card2)
        {
            switch (CardsComparer.Compare(card1, card2))
            {
                case -1:
                    return true;
                case 0:
                case 1:
                default:
                    return false;
            }
        }

        public static bool operator !=(Card card1, Card card2)
        {
            switch (CardsComparer.Compare(card1, card2))
            {
                case 0:
                    return false;
                case -1:
                case 1:
                default:
                    return true;
            }
        }

        public static bool operator ==(Card card1, Card card2)
        {
            switch (CardsComparer.Compare(card1, card2))
            {
                case 0:
                    return true;
                case -1:
                case 1:
                default:
                    return false;
            }
        }

        public static bool operator >=(Card card1, Card card2)
        {
            switch (CardsComparer.Compare(card1, card2))
            {
                case 0:
                case 1:
                    return true;
                case -1:
                default:
                    return false;
            }
        }

        public static bool operator <=(Card card1, Card card2)
        {
            switch (CardsComparer.Compare(card1, card2))
            {
                case 0:
                case -1:
                    return true;
                case 1:
                default:
                    return false;
            }
        }

        // и так тоже можно
        public override string ToString() => $"{Value} {Suit}";
    }

    public enum CardSuit
    {
        Diamond,
        Heart,
        Spade,
        Club
    }

    public enum CardValue
    {
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    enum CardPosition
    {
        Player,
        Desc,
        Deck,
        OutOfDesc
    }
}
