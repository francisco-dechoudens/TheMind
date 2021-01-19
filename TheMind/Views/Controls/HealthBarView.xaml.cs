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
            0);      // the default value for the property

        public int NoOfHearts
        {
            get => (int)GetValue(HealthBarView.NoOfHeartsProperty);
            set {
                SetValue(HealthBarView.NoOfHeartsProperty, value);

                var hearts = new List<int>();
                for (int i = 0; i < value; i++)
                {
                    hearts.Add(i);
                }

                Hearts = hearts;
            }
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
