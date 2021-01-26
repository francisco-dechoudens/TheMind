using System;
using System.Collections.Generic;
using System.Linq;

namespace TheMind.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        public string TableName { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> DealtDeck { get; set; }
        public List<Card> PlayedCards { get; set; }
        public List<Player> Players { get; set; }
        public int Level { get; set; }
    }
}
