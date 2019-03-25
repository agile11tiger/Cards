using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Player
    {
        public List<Card> Hand { get; set; }
        public string Name { get; set; }
        public int ConnectionNumber { get; set; }
        public int QueueNumber { get; set; }
        public bool Attacker { get; set; }
        public bool Defender { get; set; }
        public bool IsFool { get; set; }
        public bool IsBot { get; set; }

        public Player(string name)
        {
            Hand = new List<Card>();
            Name = name;
        }

        public bool IsValueCardOnTable(int numberCard)
        {
            if (Hand.Count > numberCard)
            {
                var cardsOnTable = new List<CardValue>();
                var card = Hand[numberCard];

                foreach (var pairCards in Game.CardsPairsOnTable)
                {
                    if (!(pairCards.LessCard is null)) cardsOnTable.Add(pairCards.LessCard.Value);
                    if (!(pairCards.LargerCard is null)) cardsOnTable.Add(pairCards.LargerCard.Value);
                }

                if (cardsOnTable.Contains(card.Value)) { cardsOnTable.Clear(); return true; }
            }

            Console.WriteLine($"Dear, player {Name}. There aren`t ({Hand[numberCard].Value}) on the table!");
            return false;
        }

        public bool Throw(int numberCard)
        {
            Game.counter++;
            if (Hand.Count > numberCard)
            {
                var card = Hand[numberCard];

                if (!(card is null))
                {
                    if (Game.counter % 2 != 0)
                    {
                        var cardPair = new CardPairOnTable();
                        cardPair.Add(card);
                        Game.CardsPairsOnTable.Add(cardPair);
                        Hand.RemoveAt(numberCard);
                        return true;
                    }
                    else if (Game.counter % 2 == 0)
                    {
                        var result = Game.CardsPairsOnTable.Last().Add(card);
                        if (!result) { Game.counter--; return false; }
                        Hand.RemoveAt(numberCard);
                        return true;

                    }
                }
            }
            throw new ArgumentException($"у игрока {Name} нет карты ({Hand[numberCard]})");
        }

        public bool Pass()
        {
            if (Game.WhoseTurn.Attacker) { Console.WriteLine(@" An attacker can`t pass a move!"); return false; }
            if (Game.WhoseTurn.Defender) { Console.WriteLine(@" Who will beat a card? If you can`t then take it!"); return false; }
            Game.counter++;
            return true;
        }

        public void Take()
        {
            GameDurak.PlayersThrowOutCards();
            Game.counter++;

            foreach (var pairCards in Game.CardsPairsOnTable)
            {
                if (!(pairCards.LessCard is null)) Hand.Add(pairCards.LessCard);
                if (!(pairCards.LargerCard is null)) Hand.Add(pairCards.LargerCard);
            }

            Game.CardsPairsOnTable.Clear();
        }

        public void GetAll()
        {

        }
    }
}
