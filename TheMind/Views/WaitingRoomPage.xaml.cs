using System;
using System.Collections.Generic;
using TheMind.ViewModels;
using Xamarin.Forms;

namespace TheMind.Views
{
    public partial class WaitingRoomPage : ContentPage
    {
        public WaitingRoomPage()
        {
            InitializeComponent();
            BindingContext = new WaitingRoomPageViewModel();
        }
    }
}
