using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelPtyxiaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCleaningService : ContentPage
    {
        public PageCleaningService()
        {
            InitializeComponent();
        }

        public void CheckImageClicked(object sender, EventArgs args)
        {
            try
            {
                if (LblStateQuestion.Text == "Clean")
                {
                    LblStateQuestion.Text = "Not Clean";
                    ImgCheckClean.Source = "/Assets/deactivate.png";
                    ImgClean.Source = "/Assets/closedclean.png";

                    return;
                }
                LblStateQuestion.Text = "Clean";
                ImgCheckClean.Source = "/Assets/activate.png";
                ImgClean.Source = "/Assets/openclean.png";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}