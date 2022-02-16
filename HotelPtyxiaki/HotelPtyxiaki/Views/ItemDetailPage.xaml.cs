using HotelPtyxiaki.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace HotelPtyxiaki.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}