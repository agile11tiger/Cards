using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class Program
    {
        public static void Main()
        {
            var countPlayers = 2;
            var game = new GameDurak(countPlayers);
            var players = new List<Player>(countPlayers);
            for (int i = 1; i <= countPlayers; i++)
            {
                players.Add(new Player(i.ToString()));
                if (i >= 2) players[i - 1].IsBot = true;
            }
            var table = new ConsoleTable(game, players);
        }
    }
}
