using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    // статические классы часто удобно называть Хэлперами. У нас подходящий случай
    public static class CardsComparerHelper
    {
        public static CardSuit? TrumpSuit { get; set; }

        public static bool IsTrump(Card card)
        {
            return TrumpSuit == card.Suit;
        }

        public static int Compare(Card card1, Card card2)
        {
            if (TrumpSuit is null)
            {
                throw new Exception("There is no trump in the game.");
            }

            // верификация аргументов
            if (card1 is null && card2 is null) return 0;
            if (card1 is null) return -1;
            if (card2 is null) return 1;

            if (card1.Suit == card2.Suit)
            {
                if (card1.Value == card2.Value) return 0;
                if (card1.Value > card2.Value) return 1;
                return -1;
            }
            else if (IsTrump(card1)) return 1;
            else if (IsTrump(card2)) return -1;

            else return -1; //throw new Exception($"Разная масть у {card1} и {card2}");
        }
    }

    // Нужен для того, чтобы работал Sort
    public class CardComparer : IComparer<Card>
    {
        public int Compare(Card card1, Card card2)
        {
            // воспользуемся хелпером
            return CardsComparerHelper.Compare(card1, card2);
        }
    }
}
