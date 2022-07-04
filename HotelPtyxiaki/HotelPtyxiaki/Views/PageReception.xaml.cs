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
        public PageReception()
        {
            InitializeComponent();
            //Map hotelmap = new Map();
            //hotelmap.Pins.Clear();
            //MyLayout.Children.Add(hotelmap, () => new Rectangle(150, 150, 150, 150));
        }

        public async void BtnCallReceptionClicked(object sender, EventArgs args)
        {
            PlacePhoneCall("6980386727");
        }

        public async void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
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