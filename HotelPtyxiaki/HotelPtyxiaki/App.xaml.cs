using HotelPtyxiaki.Services;
using HotelPtyxiaki.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace HotelPtyxiaki
{
    public partial class App : Application
    {
        private bool CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet) { return true; }
            else
                return false;
        }
        public async Task APITest()
        {
            Models.Hotel testhotel = new Models.Hotel();
            testhotel.HotelWebsite = "JerrikoHotel.com";
            testhotel.HotelName = "Jerriko Hotel";
            testhotel.HotelInfo = "This is my test for the fucking API, and this is the info of the fucking Jerriko. If you arrived up here it means that Android app can post!!! Bravo";
            testhotel.ReceptionTelephone = 251235125;
            testhotel.HotelAddress = "Vasilissis Sofias 68";
            testhotel.HotelEmail = "info@JerrikoHotel.com.mk";
            testhotel.CleaningServiceActivate = true;
            testhotel.RestaurantMenuLink = "JerrikoHotel.SexiestHotel.com.mk";
            testhotel.CleaningServiceReservDateTime = "2023-08-23 10:35:00, 2023-08-24 12:30:00, 2023-08-25 14:45:00";
            testhotel.RestaurantReservDateTime = "2023 - 08 - 23 21:00:00";
            testhotel.RestaurantReservPeopleNumber = 2;
            Services.HotelAPIService _api = new Services.HotelAPIService();
            await _api.UpdateHotelDataAsync(testhotel);
            Models.CleaningService cleanex = await _api.GetCleaningServiceDataAsync();
            Console.WriteLine("CLEANEEEEX!!!: " + cleanex.ToString());
        }//only for tests when debugging
        public App()
        {
            InitializeComponent();
            Task.Run(async() => await APITest());
            if (CheckConnection()) { MainPage = new AppShell(); }
            else
            { MainPage = new NetworkErrorPage(); }
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
