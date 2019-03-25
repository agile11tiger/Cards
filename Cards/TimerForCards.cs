using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Cards
{
    class TimerForCards
    {
        public static int counter = 0;
        private Timer timer = new Timer(1000);
        private Player throwingPlayer;
        private List<Card> list;

        public void Timer(Player throwingPlayer_, List<Card> list_)
        {
            throwingPlayer = throwingPlayer_;
            list = list_;
            timer.Elapsed -= timer_Elapsed;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            counter++;
            ConsoleTable.Animations(throwingPlayer, list);

            if (counter == 10)
            {
                timer.Stop();
                timer.Close();
            }
        }
    }
}
