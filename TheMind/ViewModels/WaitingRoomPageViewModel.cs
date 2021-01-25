using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Models.Enums;
using TheMind.Services;
using TheMind.Views;
using Xamarin.Forms;

namespace TheMind.ViewModels
{
    public class WaitingRoomPageViewModel : BaseViewModel
    {
        private INavigation Navigation;
        public string CurrentPlayerNickName { get; set; }
        GameService services;

        public ViewModeEnum ViewMode { get; set; }
        public string RoomId { get; set; }

        private List<Player> players;
        public List<Player> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        private Player selectedSeat;
        public Player SelectedSeat
        {
            get => selectedSeat;
            set => SetProperty(ref selectedSeat, value);
        }


        private bool allPlayerSeated;
        public bool AllPlayerSeated
        {
            get => allPlayerSeated;
            set => SetProperty(ref allPlayerSeated, value);
        }

        public WaitingRoomPageViewModel(INavigation navigation, string key)
        {
            Navigation = navigation;
            services = new GameService();

            RoomId = key;
            var gameDBBind = services.GetGameData(RoomId);

            gameDBBind.Subscribe(item =>
            {
                var game = ((Game)item.Object);
                Players = game.Players;
                CheckSeatStatus();
            });

            StartGameCommand = new MvvmHelpers.Commands.Command(this.StartGame);
        }

        public ICommand StartGameCommand { get; private set; }

        private async void StartGame()
        {
            if (AllPlayerSeated)
            {
                if(ViewMode == ViewModeEnum.Board)
                {
                    var detailPage = new GamePage();
                    detailPage.BindingContext = new GamePageViewModel(Navigation, RoomId);
                    await this.Navigation.PushAsync(detailPage);
                }
                else if(ViewMode == ViewModeEnum.Player)
                {
                    var detailPage = new PlayerGamePage();
                    detailPage.BindingContext = new PlayerGamePageViewModel(this.Navigation, this.RoomId, Players.First(i => i.NickName == this.CurrentPlayerNickName));
                    await this.Navigation.PushAsync(detailPage);
                }
            }
        }

        private void CheckSeatStatus()
        {
            AllPlayerSeated = Players.All(player => player.IsSeated == "true");
        }
    }
}
