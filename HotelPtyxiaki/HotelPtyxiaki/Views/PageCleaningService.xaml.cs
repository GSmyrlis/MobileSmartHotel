using System;
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

        public PageCleaningService() { InitializeComponent(); }
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
            ObservableCollection<string> itemList = new ObservableCollection<string>();
            int i = 0;
            List<Label> labels_specific_datetimes = new List<Label>();
            foreach (DateTime date in dates)
            {
                itemList.Add(date.Day.ToString() + "/" + date.Month.ToString() + " " + times[i].Hours.ToString() + ":" + times[i].Minutes.ToString());
                Label label = new Label();
                label.Text = itemList[i];
                label.FontSize = 20;
                label.TextColor = Color.Black;
                label.Margin = new Thickness(10,0);
                label.HorizontalTextAlignment = TextAlignment.Start;
                label.VerticalTextAlignment = TextAlignment.Start;
                labels_specific_datetimes.Add(label);
                i++;
            }
            GridSpecificDateTimes.Children.Clear();
            GridSpecificDateTimes.RowSpacing = 10;
            short x = 0;
            foreach (Label lab in labels_specific_datetimes)
            {
                GridSpecificDateTimes.Children.Add(lab);
                Grid.SetRow(lab, x);
                x++;
            }
        }
        public void CheckImageClicked(object sender, EventArgs args)
        {
            try
            {
                if (SwitchEnabled.IsToggled)
                {
                    //ImgCheckClean.Source = "/Assets/deactivate.png";
                    ImgClean.Source = "/Assets/closedclean.png";
                    return;
                }
                //ImgCheckClean.Source = "/Assets/activate.png";
                ImgClean.Source = "/Assets/openclean.png";
                SwitchEnabled.IsToggled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}