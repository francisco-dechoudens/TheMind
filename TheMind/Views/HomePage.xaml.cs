using System;
using System.Collections.Generic;
using TheMind.ViewModels;
using Xamarin.Forms;

namespace TheMind.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel(this.Navigation);
        }
    }
}
