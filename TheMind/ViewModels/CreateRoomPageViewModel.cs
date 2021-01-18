using System;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Services;

namespace TheMind.ViewModels
{
    public class CreateRoomPageViewModel : BaseViewModel
    {

        private RoomService services;

        private bool twoPlayer;
        public bool TwoPlayer { get => twoPlayer; set => SetProperty(ref twoPlayer, value); }

        private bool threePlayer;
        public bool ThreePlayer { get => threePlayer; set => SetProperty(ref threePlayer, value); }

        private bool fourPlayer;
        public bool FourPlayer { get => fourPlayer; set => SetProperty(ref fourPlayer, value); }

        public Command CreateRoomCommand { get; }


        public CreateRoomPageViewModel()
        {
            services = new RoomService();
            CreateRoomCommand = new Command(async () => await CreateRoomClicked("Something"));
        }

        public async Task CreateRoomClicked(string FirstName)
        {
            if(TwoPlayer || ThreePlayer || FourPlayer)
            {
                IsBusy = true;

                var noPlayer = TwoPlayer ? 2 : ThreePlayer ? 3 : FourPlayer ? 4 : 0;
                await services.CreateRoom(new Room() { NoPlayers = noPlayer, ShareId = "Something" }) ;

                IsBusy = false;
            }
        }
    }
}
