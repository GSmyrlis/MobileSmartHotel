using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetworkErrorPage : ContentPage
    {

        public NetworkErrorPage()
        {
            InitializeComponent();
        }

        public bool NetCheck()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                Navigation.PushModalAsync(new LoginPage());
                return true;
            }
            return false;
        }

        public void GotTapped(object sender, EventArgs e)
        {
            NetCheck();
        }
    }
}