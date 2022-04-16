﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCleaningService : ContentPage
    {
        List<DateTime> dates = new List<DateTime>();
        List<TimeSpan> times = new List<TimeSpan>();
        List<DateTime> datetimes = new List<DateTime>();
        public PageCleaningService() { InitializeComponent(); }
        private List<DateTime> UniteDatesWithTimes()
        {
            List<DateTime> datetimes = new List<DateTime>();
            for (int i = 0; i < dates.Count; i++)
            {
                datetimes.Add(new DateTime(day: dates[i].Day, month: dates[i].Month, hour: times[i].Hours, minute: times[i].Minutes, year: DateTime.Now.Year, second: 0));
            }
            return datetimes;
        }
        public void SpecificDateTimeClicked(object sender, EventArgs args)
        {
            datePicker.MinimumDate = DateTime.Today;
            datePicker.Date = DateTime.Today.AddDays(-1);
            datePicker.Focus();
        }
        public void SpecificDateSelected(object sender, EventArgs args)
        {
            timePicker.Focus();
        }
        public void SpecificTimeSelected(object sender, EventArgs args)
        {
            dates.Add(datePicker.Date);
            times.Add(timePicker.Time);
            datetimes =  UniteDatesWithTimes();
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
                //deleteSwipeItem.Invoked += OnDeleteSwipeItemInvoked;

                /* SwipeItem controlitem = new SwipeItem
                 {
                     Text = date.Day.ToString() + "/" + date.Month.ToString() + " " + times[i].Hours.ToString() + ":" + times[i].Minutes.ToString(),
                     IconImageSource = "daytime.png",
                     BackgroundColor = Color.LightBlue
                 }*/

                // SwipeView content
                List<SwipeItem> swipeItems = new List<SwipeItem>() { deleteSwipeItem };

                Grid grid = new Grid
                {
                    HeightRequest = 60,
                    WidthRequest = 200,
                    BackgroundColor = Color.LightGray
                };
                grid.Children.Add(new Label
                {
                    Text = "   " + datetimes[i].ToString("dd/MM - HH:mm"),
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                }) ;

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

        public void swipeDeleteItemClicked(object sender, EventArgs args)
        {

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