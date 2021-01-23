using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Services;
using TheMind.Views;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace TheMind.ViewModels
{
    public class CreateRoomPageViewModel : BaseViewModel
    {

        private INavigation Navigation;
        private GameService services;

        private bool twoPlayer;
        public bool TwoPlayer
        {
            get => twoPlayer;
            set => SetProperty(ref twoPlayer, value);
        }

        private bool threePlayer;
        public bool ThreePlayer
        {
            get => threePlayer;
            set => SetProperty(ref threePlayer, value);
        }

        private bool fourPlayer;
        public bool FourPlayer
        {
            get => fourPlayer;
            set => SetProperty(ref fourPlayer, value);
        }

        private string roomName;
        public string RoomName
        {
            get => roomName;
            set => SetProperty(ref roomName, value);
        }

        public Command CreateRoomCommand { get; }


        public CreateRoomPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.Black;
            navigationPage.BarTextColor = Color.White;

            services = new GameService();
            CreateRoomCommand = new Command(async () => await CreateRoomClicked("Something"));
        }

        public async Task CreateRoomClicked(string FirstName)
        {
            if (TwoPlayer || ThreePlayer || FourPlayer)
            {
                IsBusy = true;

                var noPlayer = TwoPlayer ? 2 : ThreePlayer ? 3 : FourPlayer ? 4 : 0;

                var game = new Game();
                game.TableName = RoomName;

                var dummyPlayer = new List<Player>();
                for(int i=0; i < noPlayer; i++)
                {
                    dummyPlayer.Add(new Player()
                    {
                        Id = Guid.NewGuid(),
                        NickName = $"Seat #{i + 1}"
                    });
                }
                game.Players = dummyPlayer;
                var gamekey = await services.SaveGameState(game);

                IsBusy = false;

                var detailPage = new WaitingRoomPage();
                detailPage.Title = RoomName;
                detailPage.BindingContext = new WaitingRoomPageViewModel(this.Navigation, gamekey);

                await this.Navigation.PushAsync(detailPage);
            }
        }
    }
}
