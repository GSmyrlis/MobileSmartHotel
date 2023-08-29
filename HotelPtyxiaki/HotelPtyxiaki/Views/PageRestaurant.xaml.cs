using System;
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
            this.Appearing += RefreshPage;
        }
        public async void RefreshPage(object sender, EventArgs e)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            Models.RestaurantReservation ourRest = await _api.GetRestaurantReservationAsync();
            GetValuesToPage(ourRest);
            return;
        }

        public void GetValuesToPage(Models.RestaurantReservation restran)
        {
            EdComment.Text = restran.RestaurantReservComment;
            if (restran.RestaurantReservPeopleNumber > 0)
            {
                StprPeople.Value = restran.RestaurantReservPeopleNumber;
            }
            else
            {
                StprPeople.Value = 0;
            }
            try
            {
                if (HotelPtyxiaki.PublicMethods.ConvertStringToListOfDateTime(restran.RestaurantReservDateTime)[0] != null)
                {
                    datePicker.Date = HotelPtyxiaki.PublicMethods.ConvertStringToListOfDateTime(restran.RestaurantReservDateTime)[0];
                    SpecificDateSelected(null, null);
                    timePicker.Time = HotelPtyxiaki.PublicMethods.ConvertStringToListOfDateTime(restran.RestaurantReservDateTime)[0].TimeOfDay;
                    SpecificTimeSelected(null, null);
                }
            }
            catch (Exception dtex)
            {
                Console.WriteLine(dtex.Message);
            }
        }

        public async void BtnMenu_Click(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri(App.RestaurantMenu));
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
        }
        public Models.RestaurantReservation GetCurrentRestaurantReservationData()
        {
            Models.RestaurantReservation _restreserve = new Models.RestaurantReservation();
            _restreserve.RestaurantReservPeopleNumber = Convert.ToInt32(StprPeople.Value);
            _restreserve.RestaurantReservComment = EdComment.Text;
            _restreserve.RestaurantReservDateTime = (new DateTime(hour: timePicker.Time.Hours, minute: timePicker.Time.Minutes, month: datePicker.Date.Month, day: datePicker.Date.Day, year: datePicker.Date.Year, second: 0)).ToString("dd/MM/yyyy hh:mm:ss");
            return _restreserve;
        }
    }
}
