using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Services;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace TheMind.ViewModels
{
    public class GamePageViewModel : BaseViewModel
    {
        private PlayerService services;
        private GameService gameServices;

        private int DECKSIZE = 100;

        public List<Card> Deck { get; set; }

        public Game Game { get; set; }

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

        public GamePageViewModel(INavigation navigation, string tableName)
        {
            gameServices = new GameService();
            services = new PlayerService();
            StartGameCommand = new Command(async () => await StartGameClicked());
            DealMoreCardsCommand = new Command(async () => await DealMoreCardsClicked());

            var gameDBBind = gameServices.GetGameData(tableName);
            gameDBBind.Subscribe(item =>
            {
                Game = ((Game)item.Object);
                Cards = Game.PlayedCards;
            });
        }
        
        public Command StartGameCommand { get; }

        public async Task StartGameClicked()
        {
            //Players = new List<Player>();
            //players.Add(new Player() { NickName = "Francisco", IsSeated = "true" });
            //players.Add(new Player() { NickName = "Lala", IsSeated = "true" });

            Deck = InitDeck(DECKSIZE);
            Deck = Shuffle(Deck);

            await DealCards(Deck, 5);
        }

        public Command DealMoreCardsCommand { get; }

        public async Task DealMoreCardsClicked()
        {
            Players[0].CardsInHand.RemoveAt(0);
            await services.UpdatePlayerState(Players[0]);
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////
        /// </summary>

        private List<Card> InitDeck(int size)
        {
            var deck = new List<Card>();
            for (int i = 0; i < size; i++)
            {
                deck.Add(new Card() { Value = i + 1 });
            }

            return deck;
        }

        public List<Card> Shuffle(List<Card> deck)
        {
            Random rng = new Random();
            int n = deck.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }

            return deck;
        }

        public async Task DealCards(List<Card> deck, int noOfCards)
        {
            foreach (var player in Game.Players)
            {
                var dealthCards = deck.Take(noOfCards);

                if (Game.DealtDeck == null) Game.DealtDeck = new List<Card>();

                Game.DealtDeck.AddRange(dealthCards);
                player.CardsInHand = dealthCards.OrderByDescending(c => c.Value).ToList();
                deck.RemoveRange(0, noOfCards);
            }

            await gameServices.UpdateGameState(Game);
        }
    }
}
