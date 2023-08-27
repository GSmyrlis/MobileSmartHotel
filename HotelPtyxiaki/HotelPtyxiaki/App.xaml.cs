using HotelPtyxiaki.Services;
using HotelPtyxiaki.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Rating;

namespace HotelPtyxiaki
{
    public partial class App : Application
    {
        public static string RestaurantMenu = "https://img.freepik.com/free-vector/creative-restaurant-menu-digital-use-with-photo_52683-45622.jpg?w=2000";
        public async Task GetHotelData()
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            Models.Hotel hoteldata = await _api.GetHotelDataAsync();
            RestaurantMenu = hoteldata.RestaurantMenuLink;
        }

        public App()
        {
            InitializeComponent();
            Task.Run(async() => await GetHotelData());
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
            MainPage = new AppShell(); 
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                MainPage = new NetworkErrorPage();
            }
            else
            {
                MainPage = new AppShell();
            }
        }
    }
}
