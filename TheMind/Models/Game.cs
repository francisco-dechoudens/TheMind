using System;
using System.Collections.Generic;
using System.Linq;

namespace TheMind.Models
{
    public class Game
    {
        public List<Card> Deck { get; set; }
        public List<Player> Players { get; set; }

        private int DECKSIZE = 100;

        public Game(List<Player> players, int level)
        {
            Players = players;

            Deck = InitDeck(DECKSIZE);
            Deck = Shuffle(Deck);
            DealCards(Deck, level, Players);
        }

        private List<Card> InitDeck(int size)
        {
            var deck = new List<Card>();
            for(int i = 0; i < size; i++)
            {
                deck.Add(new Card() { Value = i + 1 });
            }

            return deck;
        }

        public List<Card> Shuffle(List<Card> deck)
        {
            Random rng = new Random();
            int n = deck.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }

            return deck;
        }

        public void DealCards(List<Card> deck, int noOfCards, List<Player> players)
        {
            foreach(var player in players)
            {
                player.CardsInHand = deck.Take(noOfCards).OrderByDescending(c => c.Value).ToList();
                deck.RemoveRange(0, noOfCards);
            }
        }
    }
}
