using System;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Views;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace TheMind.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public HomePageViewModel(INavigation navigation) 
        {
            Navigation = navigation;
            CreateGameCommand = new Command(CreateGame);
            JoinGameCommand = new Command(JoinGame);
        }


        public ICommand CreateGameCommand { get; private set; }

        private async void CreateGame()
        {
            var detailPage = new CreateRoomPage();
            detailPage.BindingContext = new CreateRoomPageViewModel(this.Navigation);

            await this.Navigation.PushAsync(detailPage);
        }

        public ICommand JoinGameCommand { get; private set; }

        private async void JoinGame()
        {
            var detailPage = new SearchRoomPage();
            detailPage.BindingContext = new SearchRoomPageViewModel(this.Navigation);

            await this.Navigation.PushAsync(detailPage);
        }
    }
}
