using System;
using System.Collections.Generic;
using MvvmHelpers;

namespace TheMind.Models
{
    public class Player : ObservableObject
    {
        public Player()
        {
        }

        public Guid Id { get; set; }
        public string IsSeated { get; set; }

        private string nickName;
        public string NickName
        {
            get => nickName;
            set => SetProperty(ref nickName, value);
        }

        private List<Card> cardsInHand;
        public List<Card> CardsInHand
        {
            get => cardsInHand;
            set => SetProperty(ref cardsInHand, value);
        }
    }
}
