using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageReception : ContentPage
    {
        string ReceptionTelephone = "00306980386727";
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
            return;
        }

        public void GetValuesToPage(Models.Hotel hot)
        {
            LblHotelName.Text = hot.HotelName;
            LblHotelEmail.Text = hot.HotelEmail;
            LblHotelAddress.Text = hot.HotelAddress;
            LblWelcome.Text = hot.HotelInfo;
            ReceptionTelephone = hot.ReceptionTelephone.ToString();
        }

        public async void BtnCallReceptionClicked(object sender, EventArgs args)
        {
            PlacePhoneCall(ReceptionTelephone);
        }

        public async void PlacePhoneCall(string number)
        {
            try
            {
                await Launcher.OpenAsync(number);
                //PhoneDialer.Open(number);
            }
            catch (ArgumentNullException ex)
            {
                await DisplayAlert("Cant make call", "Something went wrong!", "OK");
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Cant make call", "Please check your device properties", "OK");
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                await DisplayAlert("Cant make call", "Something went wrong!", "OK");
                // Other error has occurred.
            }
        }
    }
}