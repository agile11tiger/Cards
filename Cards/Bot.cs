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
        // более точное имя, в каждом классе по каунтеру, не понятно, что он считает
        // обычно, принято создавать поле counter в классе, который означает количество элементов содержащихся в классе.
        // в остальных случаях имя counter является неочевидным и требует уточнения.
        int counter;


        /// <summary>
        /// Добавить описание функции (т.е. summary), т.к. работа функции неочевидна и достаточно сложна
        /// </summary>
        /// <param name="player">здесь описать почему передаётся именно этот параметр</param>
        /// <returns>здесь описать что возвращает</returns>
        public int WriteNumber(Player player)
        {
            BubbleSort(player, player.Hand);
            counter = 0;

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

                    if (CardsComparer.IsTrump(myCard) && !(CardsComparer.IsTrump(enemyCard.LessCard)))
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
                    if (CardsComparer.IsTrump(card1)) break;
                    if (card1.Value == card.Value) return index;
                    index++;
                }
            }

            if (Game.IsTaker == true) Thread.Sleep(11000);
            return player.Hand.Count + 2;
        }
    }
}
