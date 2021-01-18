using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using Xamarin.Forms;

namespace TheMind.ViewModels
{
    public class WaitingRoomPageViewModel : BaseViewModel
    {
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


        public WaitingRoomPageViewModel()
        {
            Players = new List<Player>();
            players.Add(new Player() { NickName = "Seat #1" });
            players.Add(new Player() { NickName = "Seat #2" });

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
