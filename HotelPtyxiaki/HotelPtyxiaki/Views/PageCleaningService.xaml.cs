using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCleaningService : ContentPage
    {
        List<DateTime> datetimes = new List<DateTime>();
        List<DateTime> _givendts = new List<DateTime>();
        public PageCleaningService()
        {
            InitializeComponent();
            this.Appearing += RefreshPage;
        }

        public async void RefreshPage(object sender, EventArgs e)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            Models.CleaningService ourcleaning = await _api.GetCleaningServiceDataAsync();
            GetValuesToPage(ourcleaning);
            return;
        }

        public void GetValuesToPage(Models.CleaningService ourcleaning)
        {
            SwitchEnabled.IsToggled = ourcleaning.CleaningServiceActivate;
            _givendts = HotelPtyxiaki.PublicMethods.ConvertStringToListOfDateTime(ourcleaning.CleaningServiceReservDateTime);
            SpecificDateTimesShow(_givendts);
        }

        public async void BtnSubmitClicked(object sender, EventArgs args)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            bool submittedsuccess = await _api.PostCleaningAsync(GetCurrentCleaningServiceData());
            if (!submittedsuccess)
            {
                await DisplayAlert("Failure", "Failed to submit your request", "OK");
                return;
            }
            await DisplayAlert("Posted", "Successfully submitted your request", "OK");
        }

        public Models.CleaningService GetCurrentCleaningServiceData()
        {
            Models.CleaningService _cleandata = new Models.CleaningService();
            _cleandata.CleaningServiceActivate = SwitchEnabled.IsToggled;
            _cleandata.CleaningServiceReservDateTime = PublicMethods.ConvertDateTimeListToString(datetimes);
            return _cleandata;
        }

        public void SpecificDateTimeClicked(object sender, EventArgs args)
        {
            if (SwitchEnabled.IsToggled)
            {
                datePicker.MinimumDate = DateTime.Today.AddDays(+1);
                datePicker.Date = DateTime.Today.AddDays(-1);
                datePicker.IsVisible = true;
                datePicker.Focus();
                datePicker.IsVisible = false;
            }
        }

        public void SpecificDateSelected(object sender, EventArgs args)
        {
            timePicker.Focus();
        }

        public void SpecificDateTimesShow()
        {
            ObservableCollection<string> itemList = new ObservableCollection<string>();
            List<SwipeView> swipeViewDateTimes = new List<SwipeView>();
            foreach(DateTime dt in datetimes)
            {
                SwipeItem deleteSwipeItem = new SwipeItem
                {
                    Text = "Delete",
                    IconImageSource = "delete.png",
                    BackgroundColor = Color.IndianRed,
                    BindingContext = dt
                };
                deleteSwipeItem.Clicked += swipeDeleteItemClicked;
                List<SwipeItem> swipeItems = new List<SwipeItem>() { deleteSwipeItem };

                Grid grid = new Grid
                {
                    HeightRequest = 60,
                    WidthRequest = 200,
                    BackgroundColor = Color.LightGray
                };
                grid.Children.Add(new Label
                {
                    BindingContext = dt,
                    Text = "   " + dt.ToString("dd/MM - HH:mm"),
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                });

                SwipeView swipeView = new SwipeView
                {
                    LeftItems = new SwipeItems(swipeItems),
                    Content = grid
                };
                swipeViewDateTimes.Add(swipeView);
            }
            GridSpecificDateTimes.Children.Clear();
            GridSpecificDateTimes.RowSpacing = 10;
            ushort x = 0;
            foreach (SwipeView swipeView in swipeViewDateTimes)
            {
                GridSpecificDateTimes.Children.Add(swipeView);
                Grid.SetRow(swipeView, x);
                x++;
            }
        }
        public void SpecificDateTimesShow(List<DateTime> _datetimes)
        {
            ObservableCollection<string> itemList = new ObservableCollection<string>();
            List<SwipeView> swipeViewDateTimes = new List<SwipeView>();
            foreach (DateTime dt in _datetimes)
            {
                SwipeItem deleteSwipeItem = new SwipeItem
                {
                    Text = "Delete",
                    IconImageSource = "delete.png",
                    BackgroundColor = Color.IndianRed,
                    BindingContext = dt
                };
                deleteSwipeItem.Clicked += swipeDeleteItemClicked;
                List<SwipeItem> swipeItems = new List<SwipeItem>() { deleteSwipeItem };

                Grid grid = new Grid
                {
                    HeightRequest = 60,
                    WidthRequest = 200,
                    BackgroundColor = Color.LightGray
                };
                grid.Children.Add(new Label
                {
                    BindingContext = dt,
                    Text = "   " + dt.ToString("dd/MM - HH:mm"),
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                });

                SwipeView swipeView = new SwipeView
                {
                    LeftItems = new SwipeItems(swipeItems),
                    Content = grid
                };
                swipeViewDateTimes.Add(swipeView);
            }
            GridSpecificDateTimes.Children.Clear();
            GridSpecificDateTimes.RowSpacing = 10;
            ushort x = 0;
            foreach (SwipeView swipeView in swipeViewDateTimes)
            {
                GridSpecificDateTimes.Children.Add(swipeView);
                Grid.SetRow(swipeView, x);
                x++;
            }
            datetimes = _datetimes;
        }

        public void SpecificTimeSelected(object sender, EventArgs args)
        {
            datetimes.Add(new DateTime(year:datePicker.Date.Year, month:datePicker.Date.Month, day:datePicker.Date.Day, hour:timePicker.Time.Hours, minute:timePicker.Time.Minutes, second: 0));
            SpecificDateTimesShow();
            datePicker.Date = DateTime.Today;
        }


        public void swipeDeleteItemClicked(object sender, EventArgs args)
        {
            if (sender is SwipeItem jackson)
            {
                if (jackson.BindingContext is DateTime dt)
                {
                    int index = datetimes.IndexOf(dt);
                    if (index >= 0)
                    {
                        datetimes.RemoveAt(index);
                        SpecificDateTimesShow();
                    }
                }
            }
        }

        public void CheckImageClicked(object sender, EventArgs args)
        {
            try
            {
                if (SwitchEnabled.IsToggled)
                {
                    ImgClean.Source = "/Assets/openclean.png";
                    return;
                }
                ImgClean.Source = "/Assets/closedclean.png";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override bool OnBackButtonPressed()
        {
            System.Threading.Tasks.Task.Run(async () =>{ await Shell.Current.GoToAsync("//HomePage"); }).Wait();
            return true;
        }
    }
}