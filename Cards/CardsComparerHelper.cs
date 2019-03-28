using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public static class CardsComparerHelper
    {
        public static CardSuit? TrumpSuit { get; set; }

        public static bool IsTrump(Card card)
        {
            return TrumpSuit == card.Suit;
        }

        public static int Compare(Card card1, Card card2)
        {
            if (TrumpSuit is null) throw new Exception("There is no trump in the game.");
            
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
}
