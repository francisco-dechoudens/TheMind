using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMind.Models;
using Xamarin.Forms;

namespace TheMind.Views.Controls
{
    public partial class HandOfCardsView : ContentView
    {
        public static readonly BindableProperty CardsInHandProperty = BindableProperty.Create(
           nameof(CardsInHand),
           typeof(List<Card>),
           typeof(HandOfCardsView),
           null,
           propertyChanged: CardsInHandPropertyPropertyChanged);


        static void CardsInHandPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable as HandOfCardsView, (List<Card>)newValue);
        }

        static void AttachEffect(HandOfCardsView element, List<Card> cardsInHand)
        {
            var effect = element.CardsInHand;

            var cards = new List<HandOfCard>();

            if (element.CardsInHand != null)
            {
                for (int i = 0; i < element.CardsInHand.Count; i++)
                {
                    cards.Add(new HandOfCard()
                    {
                        Card = element.CardsInHand[i],
                        Displacement = new Thickness(0, i * 25, 0, 0)
                    });
                }
            }

            element.Cards = cards;
        }

        public List<Card> CardsInHand
        {
            get => (List<Card>)GetValue(HandOfCardsView.CardsInHandProperty);
            set => SetValue(HandOfCardsView.CardsInHandProperty, value);
        }

        public static readonly BindableProperty CardsProperty = BindableProperty.Create(
        nameof(Cards),
        typeof(List<HandOfCard>),
        typeof(HandOfCardsView),
        null);

        public List<HandOfCard> Cards
        {
            get => (List<HandOfCard>)GetValue(HandOfCardsView.CardsProperty);
            set => SetValue(HandOfCardsView.CardsProperty, value);
        }

        #region ActionCommand

        public static readonly BindableProperty ActionCommandProperty =
            BindableProperty.Create(nameof(ActionCommand), typeof(ICommand), typeof(HandOfCardsView), null);

        public ICommand ActionCommand
        {
            get => (ICommand)GetValue(ActionCommandProperty);
            set => SetValue(ActionCommandProperty, value);
        }

        #endregion ActionCommand

        #region ActionCommandParameter

        public static readonly BindableProperty ActionCommandParameterProperty =
            BindableProperty.Create(nameof(ActionCommandParameter), typeof(object), typeof(HandOfCardsView), null);

        public object ActionCommandParameter
        {
            get => GetValue(ActionCommandParameterProperty);
            set => SetValue(ActionCommandParameterProperty, value);
        }

        #endregion ActionCommandParameter

        // Helper method for invoking commands safely
        public static void Execute(ICommand command)
        {
            if (command == null) return;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        public HandOfCardsView()
        {
            InitializeComponent();
        }

        async void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    await ((CardView)sender).TranslateTo(-1000, 0, 500);
                    break;
                case SwipeDirection.Right:
                    await ((CardView)sender).TranslateTo(1000, 0, 500);
                    break;
                case SwipeDirection.Up:
                    await ((CardView)sender).TranslateTo(0, -1000, 500);
                    break;
                case SwipeDirection.Down:
                    await ((CardView)sender).TranslateTo(0, 1000, 500);
                    break;
            }

            Execute(ActionCommand);
        }
    }
}
