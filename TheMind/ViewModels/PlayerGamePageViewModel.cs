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
    public class PlayerGamePageViewModel : BaseViewModel
    {
        private PlayerService services;

        private List<Card> cards;
        public List<Card> Cards
        {
            get => cards;
            set => SetProperty(ref cards, value);
        }

        private Player player;
        public Player Player
        {
            get => player;
            set => SetProperty(ref player, value);
        }


        public PlayerGamePageViewModel()
        {
            services = new PlayerService();
            GetCardsCommand = new Command(async () => await GetCardsClicked());
           

            var playerDBBind = services.GetPlayerData("Francisco");

            playerDBBind.Subscribe(item =>
            {
                Player = ((Player)item.Object);
                Cards = Player.CardsInHand;
            });
        }

        public Command GetCardsCommand { get; }

        public async Task GetCardsClicked()
        {
            Player.CardsInHand.RemoveAt(Player.CardsInHand.Count-1);
            await services.UpdatePlayerState(Player);
        }
    }
}
