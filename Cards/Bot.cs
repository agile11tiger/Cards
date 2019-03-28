using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cards
{
    class Bot
    {
        public int WriteNumber(Player player)
        {
            var listTrump = Utils.BubbleSort(player);
            var listTrumpSorted = Utils.BubbleSort(listTrump);
            player.Hand.AddRange(listTrump);

            if (player.Attacker) return 0;

            if (player.Defender)
            {
                var enemyCard = Game.CardsPairsOnTable.Last();
                var index = 0;

                foreach (var myCard in player.Hand)
                {
                    if (myCard.Suit == enemyCard.LessCard?.Suit && myCard.Value > enemyCard.LessCard?.Value)
                    {
                        return index;
                    }

                    if (CardsComparerHelper.IsTrump(myCard) && !(CardsComparerHelper.IsTrump(enemyCard.LessCard)))
                    {
                        return index;
                    }

                    index++;
                }
                return player.Hand.Count + 1;
            }

            var cardsOnTable = new List<Card>();
            foreach (var card in Game.CardsPairsOnTable)
            {
                if (!(card.LessCard is null)) cardsOnTable.Add(card.LessCard);
                if (!(card.LargerCard is null)) cardsOnTable.Add(card.LargerCard);
            }

            foreach (var card in cardsOnTable)
            {
                var index = 0;
                foreach (var card1 in player.Hand)
                {
                    if (CardsComparerHelper.IsTrump(card1)) break;
                    if (card1.Value == card.Value) return index;
                    index++;
                }
            }

            if (Game.IsTaker == true) Thread.Sleep(11000);
            return player.Hand.Count + 2;
        }
    }
}
