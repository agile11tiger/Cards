using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class CardPair
    {
        public Card LargerCard { get; private set; }
        public Card LessCard { get; private set; }

        public bool TryAdd(Card card)
        {
            if (LargerCard is null && LessCard is null)
            {
                LessCard = card;
                return true;
            }
            else if (!(LessCard is null))
            {
                if (!(card > LessCard))
                {
                    Console.WriteLine($"You can`t fight off ({LessCard}) a smaller card ({card}) or " +
                        $"\n You have different suit of ({LessCard}) and ({card})");
                    return false;
                }

                LargerCard = card;
                return true;
            }
            else { Console.WriteLine($"You can`t fight off a smaller card!"); return false; }
        }

        public override string ToString()
        {
            if (LargerCard is null) return $"{LessCard.ToString()}:";
            return $"{LessCard.ToString()}:{LargerCard.ToString()}";
        }
    }
}
