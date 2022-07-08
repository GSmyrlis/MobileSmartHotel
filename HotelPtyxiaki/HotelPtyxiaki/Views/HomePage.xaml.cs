using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public IList<Page> Pages { get; private set; }
        public HomePage()
        {
            InitializeComponent();
            
        }

        public async void ReceptionImageClicked(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("//PageReception");
        }

        public async void RestaurantImageClicked(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("//PageRestaurant");
        }

        public async void AboutImageClicked(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("//AboutPage");
        }

        public async void CleaningImageClicked(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("//PageCleaningService");
        }

        public async void RateImageClicked(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("//PageRatings");
        }
    }
}