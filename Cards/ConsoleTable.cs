using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class ConsoleTable
    {
        private GameDurak game;

        public ConsoleTable(GameDurak game, List<Player> players)
        {
            this.game = game;
            foreach (var player in players)
            {
                Game.TryConnect(player);
            }

            Game.StartGame();
            Console.WriteLine("\t\t\t The card game \"Durak\" has begun. Good Luck!");

            while (true)
            {
                if (Game.IsFool) break;
                if (Game.IsDraw) break;
                ShowPlayerCards();
                game.MakeMove();
                game.СhangeWhoseTurn();
            }

            Console.Clear();
            if (Game.IsDraw) Console.WriteLine("Congratulations, you have a draw");
            if (Game.IsFool) Console.WriteLine($"Plaer {Game.Players[0].Name} is Fool");
            Console.Read();
        }

        public void ShowPlayerCards()
        {
            Console.WriteLine($"\nTrump suit: {Game.Deck.TrumpSuit}");
            if (!Game.WhoseTurn.Defender && !Game.WhoseTurn.Attacker) Console.WriteLine($"Player {Game.WhoseTurn.Name} is walking! ");
            if (Game.WhoseTurn.Attacker) Console.WriteLine($"Player {Game.WhoseTurn.Name} is attacking! ");
            if (Game.WhoseTurn.Defender) Console.WriteLine($"Player {Game.WhoseTurn.Name} is defending! ");
            Console.WriteLine("\n Select a card by entering the appropriate number.");

            var number = 0;
            foreach (var card in Game.WhoseTurn.Hand)
            {
                Console.WriteLine($"{number}) {card}");
                number++;
            }

            Console.WriteLine($"{Game.WhoseTurn.Hand.Count}) Pass "); // Еще хочет ходить
            Console.WriteLine($"{Game.WhoseTurn.Hand.Count + 1}) Take ");
            Console.WriteLine($"{Game.WhoseTurn.Hand.Count + 2}) Consent to the next round, if the situation does`t change. ");
        }

        public static void Animations(Player throwingPlayer, List<Card> list)
        {
            Console.Clear();
            Console.WriteLine($"Player {Game.WhoseTurn.Name} is taking.\n\n Player {throwingPlayer.Name} is throwingPlayer." +
                $"\nYou have 10 ({TimerForCards.counter}) seconds to throw cards. Then click ENTER to continue");

            var number = 0;
            foreach (var card in throwingPlayer.Hand)
            {
                Console.WriteLine($"{number}) {card}");
                number++;
            }

            Console.WriteLine($"\n You can throw cards of the same value:");
            foreach (var cardPairsOnDesk in Game.CardsPairsOnTable)
            {
                Console.WriteLine(cardPairsOnDesk);
            }

            Console.WriteLine($"\n The players threw up:");
            foreach (var a in list)
            {
                Console.WriteLine(a);
            }
        }
    }
}

