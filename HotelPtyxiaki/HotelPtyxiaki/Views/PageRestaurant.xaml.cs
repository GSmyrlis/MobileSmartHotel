using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRestaurant : ContentPage
    {
        public PageRestaurant()
        {
            InitializeComponent();
        }
        public async void BtnMenu_Click(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://img.freepik.com/free-vector/creative-restaurant-menu-digital-use-with-photo_52683-45622.jpg?w=2000"));
        }
    }
}