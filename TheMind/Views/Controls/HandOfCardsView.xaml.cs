using System;
using System.Collections.Generic;
using TheMind.Models;
using Xamarin.Forms;

namespace TheMind.Views.Controls
{
    public partial class HandOfCardsView : ContentView
    {
        public static readonly BindableProperty NoOfCardsProperty = BindableProperty.Create(
           nameof(NoOfCards),
           typeof(int),
           typeof(HandOfCardsView),
           0,
           propertyChanged: NoOfCardsPropertyPropertyChanged);


        static void NoOfCardsPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable as HandOfCardsView, (int)newValue);
        }

        static void AttachEffect(HandOfCardsView element, int numberOfCards)
        {
            var effect = element.NoOfCards;

            var cards = new List<HandOfCard>();
            for (int i = 0; i < element.NoOfCards; i++)
            {
                cards.Add(new HandOfCard() {
                    Card = new Card() { Value = i },
                    Displacement = new Thickness(0,i*25,0,0)
                });
            }

            element.Cards = cards;
        }

        public int NoOfCards
        {
            get => (int)GetValue(HandOfCardsView.NoOfCardsProperty);
            set => SetValue(HandOfCardsView.NoOfCardsProperty, value);
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

        public HandOfCardsView()
        {
            InitializeComponent();
        }
    }
}
