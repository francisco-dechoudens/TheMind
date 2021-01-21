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

        private bool isSeated;
        public bool IsSeated
        {
            get => isSeated;
            set => SetProperty(ref isSeated, value);
        }

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
