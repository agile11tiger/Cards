using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cards
{
    public static class Game
    {
        public static int counter = 0;
        public static bool IsDefender;
        public static bool IsFool;
        public static bool IsDraw;
        public static bool IsTaker;
        public const int MaxPlayers = 6;
        public const int LimiterHandOutCards = 6;
        public static Deck Deck { get; set; }
        public static Player WhoseTurn { get; set; }
        public static List<Player> Players { get; set; }
        public static List<CardPairOnTable> CardsPairsOnTable { get; set; }

        public static bool TryConnect(Player player)
        {
            try
            {
                Players.Add(player);
            }
            catch
            {
                return false;
            }

            player.ConnectionNumber = Players.Count;
            return true;
        }

        public static void StartGame()
        {
            Deck = new Deck();
            CardsComparer.TrumpSuit = Deck.TrumpSuit;
            HandOutCards();
            WhoseTurn = DecideWhoseTurn();
            WhoseTurn.Attacker = true;
            CardsPairsOnTable = new List<CardPairOnTable>();
        }

        public static void StopGame()
        {
            Deck = null;
            Players = null;
        }

        public static Player DecideWhoseTurn()
        {
            Player firstPlayer = null;
            var flag = true;
            Card minTrumpCard = new Card(Deck.TrumpSuit, CardValue.Ace);
            //масть не имеет значение.
            Card maxNotTrump = new Card(CardSuit.Club, CardValue.Six);

            foreach (var player in Players)
            {
                var GetMaxNotTrumpOrMinTrumpPlayer = GetMaxNotTrumpOrMinTrump(player);

                if (GetMaxNotTrumpOrMinTrumpPlayer.Suit == Deck.TrumpSuit)
                {
                    if (GetMaxNotTrumpOrMinTrumpPlayer <= minTrumpCard)
                    {
                        minTrumpCard = GetMaxNotTrumpOrMinTrumpPlayer;
                        firstPlayer = player;
                        flag = false;
                    }
                }
                else if (flag)
                {
                    if (GetMaxNotTrumpOrMinTrumpPlayer.Value >= maxNotTrump.Value)
                    {
                        maxNotTrump = GetMaxNotTrumpOrMinTrumpPlayer;
                        firstPlayer = player;
                    }
                }
            }
            return firstPlayer;
        }

        public static Card GetMinTrump(Player player)
        {
            Card min = null;
            bool found = false;

            foreach (var card in player.Hand)
            {
                if (card.Suit == Deck.TrumpSuit)
                {
                    min = card;
                    found = true;
                    break;
                }
            }

            if (found)
            {
                foreach (var card in player.Hand)
                {
                    if (card.Suit == Deck.TrumpSuit)
                        if (card.Value < min.Value) min = card;
                }
            }
            else return null;

            return min;
        }

        public static Card GetMaxNotTrump(Player player)
        {
            Card max = null;
            bool found = false;

            foreach (var card in player.Hand)
            {
                if (card.Suit != Deck.TrumpSuit)
                {
                    max = card;
                    found = true;
                    break;
                }
            }

            if (found)
            {
                foreach (var card in player.Hand)
                {
                    if (card.Suit != Deck.TrumpSuit)
                        if ((int)card.Value > (int)max.Value) max = card;
                }
            }
            else return null;

            return max;
        }

        public static Card GetMaxNotTrumpOrMinTrump(Player player)
        {
            //масть не имеет значение.
            Card minTrumpCard = new Card(Deck.TrumpSuit, CardValue.Ace);
            Card maxNotTrumpCard = new Card(CardSuit.Club, CardValue.Six);
            var minTrumpPlayer = GetMinTrump(player);

            if (minTrumpPlayer is null)
            {
                var maxNotTrumpPlayer = GetMaxNotTrump(player);

                if (maxNotTrumpPlayer.Value >= maxNotTrumpCard.Value)
                {
                    maxNotTrumpCard = maxNotTrumpPlayer;
                    return maxNotTrumpCard;
                }
            }
            else
            {
                if (minTrumpPlayer <= minTrumpCard)
                {
                    minTrumpCard = minTrumpPlayer;
                    return minTrumpCard;
                }
            }

            return null;
        }

        public static void HandOutCards()
        {
            var flag = false; //нужен для того, чтобы дать последнему игроку, если это возможно, удалиться. (и получить ничью)
            while (true)
            {
                var number = 1;
                foreach (var player in Players)
                {
                    flag = true;
                    player.QueueNumber = number;
                    number++;

                    if (Deck.Cards.Count <= 0 && player.Hand.Count == 0)
                    {
                        GameDurak.AllowGoNextInQueue(player);
                        Players.Remove(player);
                        flag = false;
                        break;
                    }

                    while (player.Hand.Count < LimiterHandOutCards && Deck.Cards.Count > 0)
                    {
                        player.Hand.Add(Deck.Cards.Pop());
                    }
                }
                
                if (number == Players.Count + 1 && flag)
                {
                    if (Players.Count == 0) IsDraw = true;
                    if (Players.Count == 1) IsFool = true;
                    return;
                }
            }
        }
    }
}

enum GameAction
{
    Throw,
    Take,
    Pass,
    GetAll
}

enum GameResult
{
    Win,
    Loose,
    Draw
}

