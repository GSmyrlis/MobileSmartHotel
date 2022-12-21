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
        void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            LblPeople.Text = String.Format("Table for: " + e.NewValue);
        }
        void BtnDateClicked(object sender, EventArgs args)
        {
                datePicker.MinimumDate = DateTime.Today.AddDays(+1);
                datePicker.Date = DateTime.Today.AddDays(-1);
                datePicker.IsVisible = true;
                datePicker.Focus();
                datePicker.IsVisible = false;
        }

        void BtnTimeClicked(object sender, EventArgs args)
        {
                timePicker.IsVisible = true;
                timePicker.Focus();
                timePicker.IsVisible = false;
        }
        void SpecificDateSelected(object sender, EventArgs e) { LblDate.Text = "Date: " + datePicker.Date.ToString("dd/MM"); }
        void SpecificTimeSelected(object sender, EventArgs e) { LblTime.Text = "Time: " + timePicker.Time.Hours + ":" + timePicker.Time.Minutes; }
    }
}