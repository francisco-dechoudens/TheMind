using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Services;
using Xamarin.Forms;

namespace TheMind.ViewModels
{
    public class WaitingRoomPageViewModel : BaseViewModel
    {
        GameService services;

        private ObservableCollection<Player> players;
        public ObservableCollection<Player> Players
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
            services = new GameService();

            Players = services.GetGamePlayers(key);
            SeatSelectedCommand = new MvvmHelpers.Commands.Command(this.SeatSelected);
        }

        public ICommand SeatSelectedCommand { get; private set; }

        private async void SeatSelected()
        {
            if (SelectedSeat != null && !SelectedSeat.IsSeated)
            {

                string name = await Application.Current.MainPage.DisplayPromptAsync("ALMOST SEATED", "ENTER A NAME");
                if (!string.IsNullOrEmpty(name))
                {
                    SelectedSeat.IsSeated = true;
                    SelectedSeat.NickName = name;
                }

                CheckSeatStatus();
                //var detailPage = new SelectedRolePage();
                //detailPage.BindingContext = new SelectedRolePageViewModel(this.Navigation, SelectedRole);
                //await this.Navigation.PushAsync(detailPage);
                //SelectedRole = null;
            }
        }

        private void CheckSeatStatus()
        {
            AllPlayerSeated = Players.All(player => player.IsSeated);
        }
    }
}
