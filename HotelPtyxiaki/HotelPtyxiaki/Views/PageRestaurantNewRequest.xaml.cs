using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRestaurantNewRequest : ContentPage
    {
        public PageRestaurantNewRequest()
        {
            InitializeComponent();
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
        void SpecificTimeSelected(object sender, EventArgs e)
        {
            LblTime.Text = "Time: " + timePicker.Time.Hours.ToString("00") + ":" + timePicker.Time.Minutes.ToString("00");
        }
        public async void BtnSubmitClicked(object sender, EventArgs e)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            bool submittedsuccess = await _api.PostRestaurantReservationAsync(GetCurrentRestaurantReservationData());
            if (!submittedsuccess)
            {
                await DisplayAlert("Failure", "Failed to post request", "OK");
                return;
            }
            await DisplayAlert("Posted", "Successfully posted your Reservation", "OK");
            await Shell.Current.GoToAsync("//PageRestaurant");
        }
        public Models.RestaurantReservation GetCurrentRestaurantReservationData()
        {
            Models.RestaurantReservation _restreserve = new Models.RestaurantReservation();
            _restreserve.RestaurantReservPeopleNumber = Convert.ToInt32(StprPeople.Value);
            _restreserve.RestaurantReservComment = EdComment.Text;
            _restreserve.RestaurantReservDateTime = (new DateTime(hour: timePicker.Time.Hours, minute: timePicker.Time.Minutes, month: datePicker.Date.Month, day: datePicker.Date.Day, year: datePicker.Date.Year, second: 0)).ToString("dd/MM/yyyy hh:mm:ss");
            return _restreserve;
        }
        protected override bool OnBackButtonPressed()
        {
            bool leaving = false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                leaving = await DisplayAlert("Attention", "If you leave this page, this request data will not be saved!", "Leave", "Stay");
                if (leaving)
                {
                    await Shell.Current.GoToAsync("//PageRestaurant");
                }
            });

            return true;
        }
    }
}
