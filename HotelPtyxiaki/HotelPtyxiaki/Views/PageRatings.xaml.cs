using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRatings : ContentPage
    {
        public PageRatings()
        {
            InitializeComponent();
            this.Appearing += RefreshPage;
        }

        public async void RefreshPage(object sender, EventArgs e)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            Models.Rating ourRate = await _api.GetRatingAsync();
            GetValuesToPage(ourRate);
            return;
        }

        public void GetValuesToPage(Models.Rating rates)
        {
            RtbarHospitality.SelectedStarValue = rates.RateHospitality;
            RtbarComfort.SelectedStarValue = rates.RateComfort;
            RtbarLocation.SelectedStarValue = rates.RateLocation;
            RtbarFacilities.SelectedStarValue = rates.RateFacilities;
            RtbarOverall.SelectedStarValue = rates.RateOverall;
        }

        public async void BtnSubmitClicked(object sender, EventArgs e)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            bool submittedsuccess = await _api.PostRatingAsync(GetCurrentRates());
            if (!submittedsuccess)
            {
                await DisplayAlert("Failure", "Failed to submit rates", "OK");
                return;
            }
            await DisplayAlert("Posted", "Successfully submitted rates", "OK");
        }

        public Models.Rating GetCurrentRates()
        {
            Models.Rating _rate = new Models.Rating();
            _rate.RateComfort = RtbarComfort.SelectedStarValue;
            _rate.RateFacilities = RtbarFacilities.SelectedStarValue;
            _rate.RateHospitality = RtbarHospitality.SelectedStarValue;
            _rate.RateLocation = RtbarLocation.SelectedStarValue;
            _rate.RateOverall = RtbarOverall.SelectedStarValue;
            return _rate;
        }
    }
}