using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Models.Enums;
using TheMind.Services;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace TheMind.ViewModels
{
    public class PlayerGamePageViewModel : BaseViewModel
    {
        private INavigation Navigation;
        private PlayerService services;
        private GameService gameServices;

        public Game Game { get; set; }

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


        public PlayerGamePageViewModel(INavigation navigation, string tableName, Player currentPlayer)
        {
            Navigation = navigation;
            services = new PlayerService();
            gameServices = new GameService();
            SendCardsCommand = new Command(async () => await SendCardToBoard(currentPlayer));

            var gameDBBind = gameServices.GetGameData(tableName);

            gameDBBind.Subscribe(item =>
            {
                Game = ((Game)item.Object);
                var players = Game.Players;
                var selectedPlayer = players.Single(p => p.Id == currentPlayer.Id);

                Player = selectedPlayer;
                Cards = selectedPlayer.CardsInHand;
            });
        }

        public Command SendCardsCommand { get; set; }

        public async Task SendCardToBoard(Player currentPlayer)
        {
            var cardremoved = Player.CardsInHand.LastOrDefault();
            Player.CardsInHand.Remove(cardremoved);

            var selectedPlayer = Game.Players.First(i => i.Id == currentPlayer.Id);
            var index = Game.Players.IndexOf(selectedPlayer);

            if (index != -1)
                Game.Players[index] = Player;

            if(Game.PlayedCards == null)
            {
                Game.PlayedCards = new List<Card>();
            }

            Game.PlayedCards.Add(cardremoved);

            await gameServices.UpdateGameState(Game);
        }
    }
}
