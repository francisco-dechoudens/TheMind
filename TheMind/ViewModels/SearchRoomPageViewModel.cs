using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using TheMind.Models;
using TheMind.Services;
using TheMind.Views;
using Xamarin.Forms;

namespace TheMind.ViewModels
{
    public class SearchRoomPageViewModel : BaseViewModel
    {
        private INavigation Navigation;
        private GameService gameServices;
        public Command JoinRoomCommand { get; }
        public Command SearchRoomCommand { get; }
        private bool isSeated;
        public Game Game { get; set; }

        private string roomId;
        public string RoomId
        {
            get => roomId;
            set => SetProperty(ref roomId, value);
        }

        private string nickname;
        public string Nickname
        {
            get => nickname;
            set => SetProperty(ref nickname, value);
        }

        public SearchRoomPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.Black;
            navigationPage.BarTextColor = Color.White;

            gameServices = new GameService();

            SearchRoomCommand = new Command(async () => await SearchRoomClicked());
            JoinRoomCommand = new Command(async () => await JoinRoomClicked());
        }

        public async Task SearchRoomClicked()
        {
            var gameDBBind = gameServices.GetGameData(RoomId);

            gameDBBind.Subscribe(item =>
            {
                Game = ((Game)item.Object);
            });


        }

        public async Task JoinRoomClicked()
        {
            var players = Game.Players;
            var openSeat = players.FirstOrDefault(p => p.IsSeated == "false");

            openSeat.NickName = nickname;
            openSeat.IsSeated = "true";

            await gameServices.UpdateGameState(Game);

            var detailPage = new WaitingRoomPage()
            {
                BindingContext = new WaitingRoomPageViewModel(this.Navigation, RoomId)
                {
                    Title = RoomId
                }
            };

            await this.Navigation.PushAsync(detailPage);
        }
    }
}
