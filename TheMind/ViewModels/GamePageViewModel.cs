using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Models.Enums;
using TheMind.Services;

namespace TheMind.ViewModels
{
    public class GamePageViewModel : BaseViewModel
    {
        private PlayerService services;

        private List<Player> players;
        public List<Player> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        private List<Card> cards;
        public List<Card> Cards
        {
            get => cards;
            set => SetProperty(ref cards, value);
        }

        public GamePageViewModel()
        {
            services = new PlayerService();
            StartGameCommand = new Command(async () => await StartGameClicked());
        }
        
        public Command StartGameCommand { get; }

        public async Task StartGameClicked()
        {
            Players = new List<Player>();
            players.Add(new Player() { NickName = "Francisco", IsSeated = true });
            players.Add(new Player() { NickName = "Lala", IsSeated = true });

            var newGame = new Game(players, 3);

            await services.SavePlayerState(players[0]);
        }

    }
}
