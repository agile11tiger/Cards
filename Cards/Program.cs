using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            var countPlayers = 3;
            var game = new GameDurak(countPlayers);
            var players = new List<Player>(countPlayers);
            for (int i = 1; i <= countPlayers; i++)
            {
                players.Add(new Player(i.ToString()));
            }
            var table = new ConsoleTable(game, players);
        }
    }
}
