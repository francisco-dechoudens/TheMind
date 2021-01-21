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

        private List<Player> players;
        public List<Player> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        private List<Card> cards;
        public List<Card> Cards
        {
            get => cards;
            set => SetProperty(ref cards, value);
        }

        private ObservableCollection<Player> currPlayer = new ObservableCollection<Player>();
        public ObservableCollection<Player> CurrPlayer
        {
            get { return currPlayer; }
            set
            {
                currPlayer = value;
                OnPropertyChanged();
            }
        }

        public PlayerGamePageViewModel()
        {
            services = new PlayerService();
            GetCardsCommand = new Command(async () => await GetCardsClicked());
        }

        void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Player item in e.NewItems)
                {
                    item.PropertyChanged += Player_PropertyChanged;
                    SetHand(item);
                }

            if (e.OldItems != null)
                foreach (Player item in e.OldItems)
                    item.PropertyChanged -= Player_PropertyChanged;

            
        }

        private void SetHand(Player player)
        {
            Cards = player.CardsInHand;
        }

        private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Player.NickName))
            {
                //Calculate();
            }
        }

        public Command GetCardsCommand { get; }

        public async Task GetCardsClicked()
        {
            CurrPlayer = services.getPlayerData();
            CurrPlayer.CollectionChanged += OnCollectionChanged;
        }

    }
}
