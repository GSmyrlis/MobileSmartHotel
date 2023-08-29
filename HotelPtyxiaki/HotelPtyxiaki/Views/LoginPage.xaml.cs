using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        }

        public async void BtnLoginClicked(object sender, EventArgs e)
        {
            Models.LoginCredentials credentials = new Models.LoginCredentials();
            credentials.username = TxtUsername.Text;
            credentials.password = TxtPassword.Text;
            Services.HotelAPIService _api = new Services.HotelAPIService();
            (bool,string) loggedin = await _api.Login(credentials);
            if (loggedin.Item1)
            {
                new AppShell();
            }
            else
            {
                await DisplayAlert("Failure", loggedin.Item2, "OK");
            }
        }
    }
}