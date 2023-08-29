using HotelPtyxiaki.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
            Task.Run(async () => await GetHotelData());
            if (Preferences.ContainsKey("Token"))
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
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
