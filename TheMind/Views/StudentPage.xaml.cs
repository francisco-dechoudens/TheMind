using System;
using System.Collections.Generic;
using TheMind.ViewModels;
using Xamarin.Forms;

namespace TheMind.Views
{
    public partial class StudentPage : ContentPage
    {
        public StudentPage()
        {
            InitializeComponent();
            BindingContext = new StudentPageViewModel();
        }
    }
}
