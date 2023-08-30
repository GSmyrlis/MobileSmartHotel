using Xamarin.Forms;

namespace HotelPtyxiaki.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            System.Threading.Tasks.Task.Run(async () => { await Shell.Current.GoToAsync("//HomePage"); }).Wait();
            return true;
        }
    }
}