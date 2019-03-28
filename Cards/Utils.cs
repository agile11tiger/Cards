using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public static class Utils
    {
        public static List<Card> BubbleSort(List<Card> listTrump)
        {
            for (int i = 0; i < listTrump.Count; i++)
                for (int j = 0; j < listTrump.Count - 1; j++)
                    if (listTrump[j].Value > listTrump[j + 1].Value)
                    {
                        var card = listTrump[j + 1];
                        listTrump[j + 1] = listTrump[j];
                        listTrump[j] = card;
                    }

            return listTrump;
        }

        public static List<Card> BubbleSort(Player player)
        {
            var listTrump = new List<Card>();
            for (int i = 0; i < player.Hand.Count; i++)
                for (int j = 0; j < player.Hand.Count - 1; j++)
                {
                    var card = player.Hand[j + 1];
                    if (CardsComparerHelper.IsTrump(card))
                    {
                        listTrump.Add(card);
                        player.Hand.Remove(card);
                        j--;
                        continue;
                    }
                    else if (player.Hand[j].Value > player.Hand[j + 1].Value)
                    {
                        player.Hand[j + 1] = player.Hand[j];
                        player.Hand[j] = card;
                    }
                }

            return listTrump;
        }
    }
}
