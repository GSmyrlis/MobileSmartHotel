using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.Appearing += RefreshPage;
        }

        public void RefreshPage(object sender, EventArgs e)
        {
            if (Xamarin.Essentials.Preferences.ContainsKey("Token"))
            {
                Xamarin.Essentials.Preferences.Clear();
            }
        }

        public async void BtnLoginClicked(object sender, EventArgs e)
        {
            Models.LoginCredentials credentials = new Models.LoginCredentials();
            credentials.username = TxtUsername.Text;
            credentials.password = TxtPassword.Text;
            Services.HotelAPIService _api = new Services.HotelAPIService();
            (bool, string) loggedin = await _api.Login(credentials);
            if (loggedin.Item1)
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await DisplayAlert("Failure", loggedin.Item2, "OK");
            }
        }
    }
}