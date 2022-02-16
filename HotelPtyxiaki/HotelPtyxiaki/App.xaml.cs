using HotelPtyxiaki.Services;
using HotelPtyxiaki.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace HotelPtyxiaki
{
    public partial class App : Application
    {
        private void CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (!(current == NetworkAccess.Internet)){ MainPage = new NetworkErrorPage(); }
            //await Navigation.PushAsync(new YourPageWhenThereIsNoConnection());
            else
                MainPage = new AppShell();
        }

        public App()
        {
            InitializeComponent();
            CheckConnection();
            DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
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
