using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;

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
            try
            {
                Models.LoginCredentials credentials = new Models.LoginCredentials
                {
                    username = TxtUsername.Text,
                    password = TxtPassword.Text
                };

                Services.HotelAPIService _api = new Services.HotelAPIService();
                (bool, string) loggedin = await _api.Login(credentials);

                if (loggedin.Item1)
                {
                    await Task.Delay(100);
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync("//HomePage");
                    });
                    return;
                }
                // If login fails, show an error message
                await DisplayAlert("Failure", loggedin.Item2, "OK");
            }
            catch (Exception ex)
            {
                // Handle and log any exceptions that occur during login
                await DisplayAlert("Error", "An error occurred during login.", "OK");
                Console.WriteLine($"Login error: {ex}");
            }
        }


        public void CkboxShowPassword(object sender, EventArgs e)
        {
                TxtPassword.IsPassword = !ChboxShowPassword.IsChecked;
                return;
        }
    }
}