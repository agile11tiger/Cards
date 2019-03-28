using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Deck
    {
        public readonly Stack<Card> Cards;
        public readonly CardSuit TrumpSuit;
        private CardSuit GetTrumpSuit() => Cards.Last().Suit;

        public Deck()
        {
            Cards = new Stack<Card>(36);

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Cards.Push(new Card(suit, value));
                }
            }

            Shuffle();
            TrumpSuit = GetTrumpSuit();
        }

        public void Shuffle()
        {
            var rand = new Random();
            var cardArr = Cards.ToArray();

            for (int i = cardArr.Count() - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                Swap(cardArr, i, j);
            }

            Cards.Clear();

            foreach (var card in cardArr)
            {
                Cards.Push(card);
            }
        }

        private void Swap(Array arr, int i, int j)
        {
            var tmp = arr.GetValue(j);
            arr.SetValue(arr.GetValue(i), j);
            arr.SetValue(tmp, i);
        }
    }
}
