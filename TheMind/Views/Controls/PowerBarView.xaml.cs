using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TheMind.Views.Controls
{
    public partial class PowerBarView : ContentView
    {
        public static readonly BindableProperty NoOfPowersProperty = BindableProperty.Create(
           nameof(NoOfPowers),       
           typeof(int),   
           typeof(PowerBarView),  
           0, 
           propertyChanged: NoOfPowersPropertyPropertyChanged);


        static void NoOfPowersPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable as PowerBarView, (int)newValue);
        }

        static void AttachEffect(PowerBarView element, int numberOfPowers)
        {
            var effect = element.NoOfPowers;

            var powers = new List<int>();
            for (int i = 0; i < element.NoOfPowers; i++)
            {
                powers.Add(i);
            }

            element.Powers = powers;
        }

        public int NoOfPowers
        {
            get => (int)GetValue(PowerBarView.NoOfPowersProperty);
            set => SetValue(PowerBarView.NoOfPowersProperty, value);
        }

        public static readonly BindableProperty PowersProperty = BindableProperty.Create(
        nameof(Powers),      
        typeof(List<int>),     
        typeof(PowerBarView),   
        null);      

        public List<int> Powers
        {
            get => (List<int>)GetValue(PowerBarView.PowersProperty);
            set => SetValue(PowerBarView.PowersProperty, value);
        }


        public PowerBarView()
        {
            InitializeComponent();
        }
    }
}
