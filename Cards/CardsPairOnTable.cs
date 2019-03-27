using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    // имя можно упростить, и так понятно, что они на столе а не на потолке
    // сущности должны быть как можно более асбтрактными и универсальными
    public class CardPair
    {
        private List<Card> cards = new List<Card>(2);
        public Card LargerCard => cards[0];
        public Card LessCard => cards[1];

        // IsEmpty - общеизвестное свойство, которое подходит в данной ситуации как нельзя лучше
        public bool IsEmpty => cards.Count == 0;
        public bool IsFull => cards.Count == 2;

        //добавим конструктор
        public CardPair(Card card1 = null, Card card2 = null /* если не знаешь что это почитай про "параметры по умолчанию" */)
        {
            cards.Add(card1);
            cards.Add(card2);
            cards.Sort(new CardComparer());
        }

        // если метод возвращает bool, то его имя должно начинаться с Try (если это не противоречит логике, конечно =) )
        public bool TryAdd(Card card)
        {
            // верификация
            if(card is null) return false;
            if(IsFull) return false;
            if(cards.Contains(card)) return false;

            // коротко и ясно
            cards.Add(card);
            cards.Sort();
            return true;
        }

        public override string ToString()
        {
            if(IsEmpty) return "Empty";
            if(IsFull) return $"{LessCard.ToString()}:{LargerCard.ToString()}";
            return $"{LessCard.ToString()}:";
        }
    }
}
