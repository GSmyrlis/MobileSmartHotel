using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageReception : ContentPage
    {
        private string ReceptionTelephone = "00306980386727";
        public PageReception()
        {
            InitializeComponent();
            this.Appearing += RefreshPage;
        }

        public async void RefreshPage(object sender, EventArgs e)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            Models.Hotel ourHotel = await _api.GetHotelDataAsync();
            GetValuesToPage(ourHotel);
            App.RestaurantMenu = ourHotel.RestaurantMenuLink;
            return;
        }

        public void GetValuesToPage(Models.Hotel hot)
        {
            LblHotelName.Text = hot.HotelName;
            LblHotelEmail.Text = hot.HotelEmail;
            LblHotelAddress.Text = hot.HotelAddress;
            LblWelcome.Text = hot.HotelInfo;
            ReceptionTelephone = hot.ReceptionTelephone;
        }

        public void BtnCallReceptionClicked(object sender, EventArgs args)
        {
            PlacePhoneCall(ReceptionTelephone);
        }

        public async void PlacePhoneCall(string number)
        {
            try
            {
                await Launcher.OpenAsync(new Uri("tel:" + number));
                //PhoneDialer.Open(number);
            }
            catch (ArgumentNullException ex)
            {
                await DisplayAlert("Cant make call", "There is no number", "OK");
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Cant make call", "Please check your device properties", "OK");
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                await DisplayAlert("Cant make call", "Wrong number!", "OK");
                // Other error has occurred.
            }
        }
    }
}