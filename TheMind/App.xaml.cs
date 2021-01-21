using System;
using TheMind.Services;
using TheMind.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMind
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DBClient.Init();

            MainPage = new GamePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
