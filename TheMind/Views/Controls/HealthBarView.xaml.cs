using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TheMind.Views.Controls
{
    public partial class HealthBarView : ContentView
    {
        public static readonly BindableProperty NoOfHeartsProperty = BindableProperty.Create(
            nameof(NoOfHearts),        // the name of the bindable property
            typeof(int),     // the bindable property type
            typeof(HealthBarView),   // the parent object type
            0, // the default value for the property
            propertyChanged: NoOfHeartsPropertyPropertyChanged);      


        static void NoOfHeartsPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable as HealthBarView, (int)newValue);
        }

        static void AttachEffect(HealthBarView element, int numberOfHearts)
        {
            var effect = element.NoOfHearts;

            var hearts = new List<int>();
            for (int i = 0; i < element.NoOfHearts; i++)
            {
                hearts.Add(i);
            }

            element.Hearts = hearts;
        }

        public int NoOfHearts
        {
            get => (int)GetValue(HealthBarView.NoOfHeartsProperty);
            set =>SetValue(HealthBarView.NoOfHeartsProperty, value);
        }

        public static readonly BindableProperty HeartsProperty = BindableProperty.Create(
        nameof(Hearts),        // the name of the bindable property
        typeof(List<int>),     // the bindable property type
        typeof(HealthBarView),   // the parent object type
        null);      // the default value for the property

        public List<int> Hearts
        {
            get => (List<int>)GetValue(HealthBarView.HeartsProperty);
            set => SetValue(HealthBarView.HeartsProperty, value);
        }

        public HealthBarView()
        {
            InitializeComponent();
        }
    }
}
