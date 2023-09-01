using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCleaningService : ContentPage
    {
        List<DateTime> datetimes = new List<DateTime>();
        List<Models.CleaningService> datetimeRequests = new List<Models.CleaningService>();
        public PageCleaningService()
        {
            InitializeComponent();
            this.Appearing += RefreshPage;
        }

        public async void RefreshPage(object sender, EventArgs e)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            List<Models.CleaningService> ourcleaning = await _api.GetCleaningServiceDataAsync();
            bool? CleaningStandard = await _api.GetCleaningServiceActivateAsync();
            GetValuesToPage(ourcleaning, CleaningStandard);
            return;
        }

        public async void GetValuesToPage(List<Models.CleaningService> ourcleaning, bool? StandardCleaningService)
        {
            if (StandardCleaningService == null)
            {
                await DisplayAlert("Data Failed", "Could not resolve if Standard Cleaning Service is activated or not!", "OK");
                SwitchEnabled.IsToggled = true;
            }
            else if (StandardCleaningService != null)
            {
                SwitchEnabled.IsToggled = StandardCleaningService.Value;
            }
            datetimeRequests = ourcleaning;
            SpecificDateTimesShow();
        }

        public async void BtnSubmitClicked(object sender, EventArgs args)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            foreach (Models.CleaningService newreq in datetimeRequests)
            {
                if (newreq.RequestState != 0)
                {
                    continue;
                }
                bool submittedsuccess = await _api.PostCleaningAsync(newreq);
                if (!submittedsuccess)
                {
                    await DisplayAlert("Failed", "Failed to submit the request for:" + newreq.CleaningServiceReservDateTime, "OK");
                    return;
                }
            }
            await DisplayAlert("Posted", "Successfully submitted your unsent requests", "OK");
            RefreshPage(null, null);
        }

        public void SpecificDateTimeClicked(object sender, EventArgs args)
        {
                datePicker.MinimumDate = DateTime.Today.AddDays(+1);
                datePicker.Date = DateTime.Today.AddDays(-1);
                datePicker.IsVisible = true;
                datePicker.Focus();
                datePicker.IsVisible = false;
        }

        public void SpecificDateSelected(object sender, EventArgs args)
        {
            if (datePicker.UserCancelled)
            {
                return;
            }
            timePicker.Focus();
        }

        public void SpecificDateTimesShow()
        {
            ObservableCollection<string> itemList = new ObservableCollection<string>();
            List<SwipeView> swipeViewDateTimes = new List<SwipeView>();
            datetimes = new List<DateTime>();
            foreach (Models.CleaningService req in datetimeRequests)
            {
                try
                {
                    datetimes.Add(PublicMethods.ConvertStringToDateTime(req.CleaningServiceReservDateTime));
                }
                catch (Exception ex)
                {
                    System.Threading.Tasks.Task.Run(async () => await DisplayAlert("Problem", ex.Message, "OK"));
                }
                SwipeItem deleteSwipeItem = new SwipeItem
                {
                    Text = "Delete",
                    IconImageSource = "delete.png",
                    BackgroundColor = Color.IndianRed,
                    BindingContext = req.CleaningServiceReservDateTime
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
                    BindingContext = req,
                    Text = "   " + req.CleaningServiceReservDateTime,
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                });
                if (req.RequestState > 0 && req.RequestState < 4)
                {

                    // Create a stack layout to hold the label and image
                    StackLayout stackLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };

                    // Add the label
                    stackLayout.Children.Add(new Label
                    {
                        Text = req.AdminMessage, // Replace with your label text
                        TextColor = Color.Black,
                        FontSize = 14,
                        FontAttributes = FontAttributes.Italic,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    });

                    Models.Enums.RequestState state = (Models.Enums.RequestState)req.RequestState;
                    Models.Enums myenums = new Models.Enums();
                    stackLayout.Children.Add(new Image
                    {
                        Source = myenums.GetImagePathForRequestState(state),
                        Aspect = Aspect.AspectFit
                    });

                    // Add the stack layout to the grid
                    grid.Children.Add(stackLayout);
                }

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

        public void SpecificTimeSelected(object sender, EventArgs args)
        {
            DateTime newJack = new DateTime(year: datePicker.Date.Year, month: datePicker.Date.Month, day: datePicker.Date.Day, hour: timePicker.Time.Hours, minute: timePicker.Time.Minutes, second: 0);
            datetimes.Add(newJack);
            datetimeRequests.Add(new Models.CleaningService
            {
                CleaningServiceReservDateTime = newJack.ToString("dd/MM/yy hh:mm:ss"),
                AdminMessage = string.Empty,
                RequestState = 0
            });
            SpecificDateTimesShow();
            datePicker.Date = DateTime.Today;
        }


        public async void swipeDeleteItemClicked(object sender, EventArgs args)
        {
            if (sender is SwipeItem jackson)
            {
                if (jackson.BindingContext is string dt)
                {
                    try
                    {
                        DateTime _dt = PublicMethods.ConvertStringToDateTime(dt);
                        int index = datetimes.IndexOf(_dt);
                        if (index >= 0)
                        {
                            // Remove from datetimes
                            datetimes.RemoveAt(index);

                            // Find the corresponding instance in datetimesRequests and remove it
                            var matchingRequest = datetimeRequests.FirstOrDefault(r => r.CleaningServiceReservDateTime == dt);
                            if (matchingRequest != null)
                            {
                                datetimeRequests.Remove(matchingRequest);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Problem", ex.Message, "OK");
                    }
                    Services.HotelAPIService _api = new Services.HotelAPIService();
                    bool Deleted = await _api.DeleteCleaningServiceRequestAsync(new Models.CleaningServiceDeleteRequest { CleaningServiceReservDateTime = dt });
                    SpecificDateTimesShow();
                }
            }
        }

        public async void CheckImageClicked(object sender, EventArgs args)
        {
            try
            {
                Services.HotelAPIService _api = new Services.HotelAPIService();
                if (SwitchEnabled.IsToggled)
                {
                    ImgClean.Source = "/Assets/openclean.png";
                    await _api.UpdateCleaningServiceActivateAsync(true);
                    return;
                }
                ImgClean.Source = "/Assets/closedclean.png";
                await _api.UpdateCleaningServiceActivateAsync(false);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", "Failed to Activate/Deactivate Standard Cleaning Service", "OK");
            }
        }
        protected override bool OnBackButtonPressed()
        {
            System.Threading.Tasks.Task.Run(async () =>{ await Shell.Current.GoToAsync("//HomePage"); }).Wait();
            return true;
        }
    }
}