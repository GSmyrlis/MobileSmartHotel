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
        List<DateTime> dates = new List<DateTime>();
        List<TimeSpan> times = new List<TimeSpan>();
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

        private List<DateTime> UniteDatesWithTimes()
        {
            List<DateTime> datetimes = new List<DateTime>();
            for (int i = 0; i < dates.Count; i++)
            {
                try
                {
                    datetimes.Add(new DateTime(day: dates[i].Day, month: dates[i].Month, hour: times[i].Hours, minute: times[i].Minutes, year: DateTime.Now.Year, second: 0));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            return datetimes;
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
            for (int i = 0; i < dates.Count; i++)
            {
                SwipeItem deleteSwipeItem = new SwipeItem
                {
                    Text = "Delete",
                    IconImageSource = "delete.png",
                    BackgroundColor = Color.IndianRed,
                    BindingContext = datetimes[i]
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
                    BindingContext = datetimes[i],
                    Text = "   " + datetimes[i].ToString("dd/MM - HH:mm"),
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
            times = new List<TimeSpan>();
            foreach (DateTime dt in _datetimes)
            {
                times.Add(dt.TimeOfDay);
            }
        }

        public void SpecificTimeSelected(object sender, EventArgs args)
        {
            dates.Add(datePicker.Date);
            times.Add(timePicker.Time);
            datetimes = UniteDatesWithTimes();
            SpecificDateTimesShow();
            datePicker.Date = DateTime.Today;
        }

        public void swipeDeleteItemClicked(object sender, EventArgs args)
        {
            if (typeof(SwipeItem) == sender.GetType())
            {
                SwipeItem jackson = (SwipeItem)sender;
                {
                    Element something = (Element)jackson.Parent.Parent;
                    SwipeView jacky = (SwipeView)something;
                    jacky.IsVisible = false;
                }
                foreach (View view in GridSpecificDateTimes.Children)
                {
                    if (view.GetType() == typeof(SwipeView))
                    {
                        if (view.IsVisible == false)
                        {
                            SwipeView swv = (SwipeView)view;
                            Grid swvgrid = (Grid)swv.Content;
                            foreach (View swvview in swvgrid.Children)
                            {
                                if (swvview.GetType() == typeof(Label))
                                {
                                    foreach (DateTime dt in datetimes)
                                    {
                                        if (dt == (DateTime)swvview.BindingContext)
                                        {
                                            try
                                            {
                                                dates.RemoveAt(datetimes.IndexOf(dt));
                                            }
                                            catch (Exception dateex)
                                            {
                                                Console.WriteLine(dateex.Message);
                                            }
                                            try
                                            {
                                                times.Remove(dt.TimeOfDay);
                                            }
                                            catch (Exception datetimesex)
                                            {
                                                Console.WriteLine(datetimesex.Message);
                                            }
                                            try
                                            {
                                                datetimes.Remove(dt);
                                            }
                                            catch (Exception dtex)
                                            {
                                                Console.WriteLine(dtex.Message);
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            SpecificDateTimesShow();
        }

        public void CheckImageClicked(object sender, EventArgs args)
        {
            try
            {
                if (SwitchEnabled.IsToggled)
                {
                    //ImgCheckClean.Source = "/Assets/deactivate.png";
                    ImgClean.Source = "/Assets/openclean.png";
                    return;
                }
                //ImgCheckClean.Source = "/Assets/activate.png";
                ImgClean.Source = "/Assets/closedclean.png";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}