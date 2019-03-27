using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public static class Utils
    {
        //общеизвестные методы, такие как buubleSort лучше держать вне рабочего кода
        public static void BubbleSort(Player player, List<Card> list)
        {
            var listTrump = new List<Card>();
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list.Count - 1; j++)
                {
                    var card = list[j + 1];
                    // и соотвественно он не должен зависеть от counter
                    // где-то тут прячеться изначально неверная логика
                    if (CardsComparer.IsTrump(card) && counter != 1)
                    {
                        listTrump.Add(card);
                        list.Remove(card);
                        j--;
                        continue;
                    }
                    else if (list[j].Value > list[j + 1].Value)
                    {
                        list[j + 1] = list[j];
                        list[j] = card;
                    }
                }

            counter++;
            if (counter == 1) BubbleSort(player, listTrump);
            if (counter != 3) { player.Hand.AddRange(list); counter++; }
        }
    }
}
