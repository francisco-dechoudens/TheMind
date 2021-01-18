using System;
using System.Collections.Generic;
using System.Linq;

namespace TheMind.Models
{
    public class Game
    {
        public string Level { get; set; }
        public List<Card> Deck { get; set; }
        private int NoOfCards = 10;

        public Game(int level)
        {
            Deck = InitDeck(NoOfCards);
            Deck = Shuffle(Deck);
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
    }
}
