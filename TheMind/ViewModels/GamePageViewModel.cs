using System;
using System.Collections.Generic;
using MvvmHelpers;
using TheMind.Models;

namespace TheMind.ViewModels
{
    public class GamePageViewModel : BaseViewModel
    {
        private List<Player> players;
        public List<Player> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        public GamePageViewModel()
        {
            Players = new List<Player>();
            players.Add(new Player() { NickName = "Francisco", IsSeated = true });
            players.Add(new Player() { NickName = "Lala", IsSeated = true });

            var newGame = new Game(players, 1);
        }

        private void DealCards(int noOfCards)
        {
            foreach(var player in players)
            {

            }
        }
    }
}
