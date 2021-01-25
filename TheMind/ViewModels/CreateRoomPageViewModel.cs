using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Models.Enums;
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
                game.TableName = RoomName = $"#{RandomString(6)}";

                var dummyPlayer = new List<Player>();
                for(int i=0; i < noPlayer; i++)
                {
                    dummyPlayer.Add(new Player()
                    {
                        Id = Guid.NewGuid(),
                        IsSeated = "false",
                        NickName = $"Vacant Seat #{i + 1}"
                    });
                }
                game.Players = dummyPlayer;
                var gamekey = await services.SaveGameState(game);

                IsBusy = false;

                var detailPage = new WaitingRoomPage()
                {
                    BindingContext = new WaitingRoomPageViewModel(this.Navigation, RoomName)//gamekey)
                    {
                        Title = RoomName,
                        ViewMode = ViewModeEnum.Board
                    }
                };

                await this.Navigation.PushAsync(detailPage);
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
