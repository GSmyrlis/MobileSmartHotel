using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace HotelPtyxiaki.Views
{
    public partial class PageRestaurant : ContentPage
    {
        List<Models.RestaurantReservation> requests = new List<Models.RestaurantReservation>();
        public PageRestaurant()
        {
            InitializeComponent();
            this.Appearing += RefreshPage;
        }

        public async void RefreshPage(object sender, EventArgs e)
        {
            Services.HotelAPIService _api = new Services.HotelAPIService();
            List<Models.RestaurantReservation> reqs = await _api.GetRestaurantReservationAsync();
            GetValuesToPage(reqs);
            return;
        }

        public async void BtnMenu_Click(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri(App.RestaurantMenu));
        }

        public void GetValuesToPage(List<Models.RestaurantReservation> reqs)
        {
            requests = reqs;
            ShowLoadedRequests();
        }
        
        public void ShowLoadedRequests()
        {
            ObservableCollection<string> itemList = new ObservableCollection<string>();
            List<SwipeView> swipeViewDateTimes = new List<SwipeView>();
            foreach (Models.RestaurantReservation req in requests)
            {
                SwipeItem deleteSwipeItem = new SwipeItem
                {
                    Text = "Delete",
                    IconImageSource = "delete.png",
                    BackgroundColor = Color.IndianRed,
                    BindingContext = req
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
                    Text = "   DateTime:" + req.RestaurantReservDateTime + " for " + req.RestaurantReservPeopleNumber + " people, with comment:" + req.RestaurantReservComment,
                    TextColor = Color.Black,
                    FontSize = 13,
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
            GridRequests.Children.Clear();
            GridRequests.RowSpacing = 10;
            ushort x = 0;
            foreach (SwipeView swipeView in swipeViewDateTimes)
            {
                GridRequests.Children.Add(swipeView);
                Grid.SetRow(swipeView, x);
                x++;
            }
        }

        public async void swipeDeleteItemClicked(object sender, EventArgs args)
        {
            if (sender is SwipeItem jackson)
            {
                if (jackson.BindingContext is Models.RestaurantReservation dt)
                {
                    try
                    {
                            // Remove from request list
                            requests.Remove(dt);
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Problem", ex.Message, "OK");
                    }
                    Services.HotelAPIService _api = new Services.HotelAPIService();
                    bool Deleted = await _api.DeleteRestaurantRequestAsync(new Models.RestaurantReservationDeleteRequest { RestaurantReservComment = dt.RestaurantReservComment, RestaurantReservDateTime = dt.RestaurantReservDateTime, RestaurantReservPeopleNumber = dt.RestaurantReservPeopleNumber });
                    ShowLoadedRequests();
                }
            }
        }
      
        public async void BtnAddRequestClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new PageRestaurantNewRequest());
        }
        protected override bool OnBackButtonPressed()
        {
            System.Threading.Tasks.Task.Run(async () =>{ await Shell.Current.GoToAsync("//HomePage"); }).Wait();
            return true;
        }
    }
}