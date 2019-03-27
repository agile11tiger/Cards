using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class GameDurak
    {
        int counterForTurn = 1;
        int counterPass = 0;
        int counterNextRound = 0;

        public GameDurak(int countPlayers)
        {
            if (countPlayers > Game.MaxPlayers) throw new Exception("Too many players");
            if (countPlayers <= 1) throw new Exception("Too few players.");
            Game.Players = new List<Player>(countPlayers);
        }

        public void MakeMove()
        {
            while (true)
            {
                var bot = new Bot();
                var number = 0;
                var result = false;

                if (!Game.WhoseTurn.IsBot) result = int.TryParse(Console.ReadLine(), out number);
                if ( Game.WhoseTurn.IsBot) { number = bot.WriteNumber(Game.WhoseTurn); result = true; }

                if (0 <= number && number <= Game.WhoseTurn.Hand.Count + 2 && result)
                {
                    if (number == Game.WhoseTurn.Hand.Count)
                    {
                        var flag = Game.WhoseTurn.Pass();
                        if (flag) { counterPass++; break; }
                        continue;
                    }

                    if (number == Game.WhoseTurn.Hand.Count + 1 && Game.WhoseTurn.Defender)
                    {
                        Game.IsTaker = true;
                        Game.WhoseTurn.Take();
                        counterNextRound = 6;
                        break;
                    }
                    if (number == Game.WhoseTurn.Hand.Count + 1 && !Game.WhoseTurn.Defender)
                        Console.WriteLine(@"Do you really want this?)");

                    if (number == Game.WhoseTurn.Hand.Count + 2)
                    {
                        var flag = Game.WhoseTurn.Pass();
                        if (flag && Game.Players.Count == 2) counterNextRound++;
                        if (flag) { counterPass++; counterNextRound++; break; }
                        continue;
                    }

                    if (number < Game.WhoseTurn.Hand.Count && (Game.WhoseTurn.Attacker || Game.WhoseTurn.Defender || Game.WhoseTurn.IsValueCardOnTable(number)))
                    {
                        var flag = Game.WhoseTurn.Throw(number);
                        if (!flag) continue;
                        counterPass = 0;
                        counterNextRound = 0;
                        break;
                    }
                }
                else Console.WriteLine(@"Enter the number that you have!");
            }

            Console.Clear();
            foreach (var cardPairsOnDesk in Game.CardsPairsOnTable)
            {
                Console.WriteLine(cardPairsOnDesk);
            }
        }

        public void СhangeWhoseTurn()
        {
            Game.WhoseTurn.Attacker = false;
            while (true)
            {
                foreach (var player in Game.Players)
                {
                    if (counterPass == 3 && counterNextRound <= 1) counterPass = 1; // нужно для зацикливания пропусков
                    if (Game.WhoseTurn.Defender && Game.WhoseTurn.Hand.Count == 0)  counterNextRound = 2; 

                    if (counterNextRound == 2 || counterNextRound == 6)
                    {
                        PrepareNextRound();
                        return;
                    }

                    if (0 == (Game.WhoseTurn.QueueNumber % (Game.Players.Count + 1)) - 1 && counterForTurn >= 3)
                    {
                        Game.WhoseTurn = Game.Players.Last();
                        if (counterPass == 1) { counterPass++; counterForTurn++; break; }
                        counterForTurn++;
                        if (counterForTurn == 5) counterForTurn = 1; 
                        return;
                    }

                    if (counterForTurn < 3 && player.QueueNumber == (Game.WhoseTurn.QueueNumber % Game.Players.Count) + 1)
                    {
                        Game.WhoseTurn = player;
                        if (counterPass == 1) { counterPass++; counterForTurn++; break; }
                        if (counterForTurn == 1 && !Game.IsDefender) { player.Defender = true; Game.IsDefender = true; }
                        counterForTurn++;
                        return;
                    }

                    if (counterForTurn >= 3 && (player.QueueNumber == (Game.WhoseTurn.QueueNumber % (Game.Players.Count + 1)) - 1))
                    {
                        Game.WhoseTurn = player;
                        if (counterPass == 1) { counterPass++; counterForTurn++; break; }
                        counterForTurn++;
                        if (counterForTurn >= 5) counterForTurn = 1;
                        return;
                    }
                }
            }
        }

        public void PrepareNextRound()
        {
            while (true)
            {
                foreach (var player in Game.Players)
                {
                    if (player.Defender && counterNextRound == 2) Game.WhoseTurn = player; 
                    else if (player.Defender && counterNextRound == 6)
                    {
                        var number = player.QueueNumber;
                        Game.WhoseTurn.Defender = false;

                        if (number == Game.Players.Count) Game.WhoseTurn = Game.Players[0];
                        else Game.WhoseTurn = Game.Players[number];
                    }
                    else continue;

                    Game.HandOutCards();
                    counterPass = 0;
                    counterNextRound = 0;
                    counterForTurn = 1;
                    Game.IsDefender = false;
                    Game.IsTaker = false;
                    if (Game.IsFool) return;
                    if (Game.IsDraw) return;
                    Game.WhoseTurn.Defender = false;
                    Game.WhoseTurn.Attacker = true;
                    Game.CardsPairsOnTable.Clear();
                    Console.Clear();
                    return;
                }
            }
        }
        
        public static void AllowGoNextInQueue(Player player)
        {
            if (Game.Players.Count == 1) return;

            var number_ = Game.WhoseTurn.QueueNumber;
            if (Game.WhoseTurn.QueueNumber == Game.Players.Count && (player.Defender || player.Attacker))
            {
                Game.WhoseTurn = Game.Players[0];
            }
            else if (player.Defender || player.Attacker)
            {
                Game.WhoseTurn = Game.Players[number_];
            }
        }

        public static void PlayersThrowOutCards()
        {
            var bot = new Bot();
            var list = new List<Card>();
            var timer = new TimerForCards();
            var number = Game.WhoseTurn.QueueNumber;
            Player throwingPlayer = null;
            var counter = 2;
            if (Game.Players.Count == 2) counter--;

            for (var i = 0; i < counter; i++)
            {
                if (i == 0)
                {
                    if (Game.WhoseTurn.QueueNumber != Game.Players.Count) throwingPlayer = Game.Players[number];
                    else throwingPlayer = Game.Players[0];
                }

                if (i == 1)
                {
                    if (Game.WhoseTurn.QueueNumber != 1) throwingPlayer = Game.Players[number - 2];
                    else throwingPlayer = Game.Players[Game.Players.Count - 1];
                }

                timer.Timer(throwingPlayer, list);

                while (true)
                {
                    if (TimerForCards.counter == 10) { TimerForCards.counter = 0; break; }

                    var numeric = 0;
                    var result = false;
                    if (!throwingPlayer.IsBot) result = int.TryParse(Console.ReadLine(), out numeric);
                    if (throwingPlayer.IsBot) { numeric = bot.WriteNumber(throwingPlayer); result = true; }

                    if (result && numeric <= throwingPlayer.Hand.Count && throwingPlayer.IsValueCardOnTable(numeric))
                    {
                        Game.WhoseTurn.Hand.Add(throwingPlayer.Hand[numeric]);
                        list.Add(throwingPlayer.Hand[numeric]);
                        Console.WriteLine($"{throwingPlayer.Hand[numeric]}");
                        throwingPlayer.Hand.RemoveAt(numeric);
                    }
                    else Console.WriteLine(@"Enter the number that you have!");
                }
            }
        }
    }
}
