using System;
using Xamarin.Forms;

namespace HotelPtyxiaki.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            this.Appearing += RefreshPage;
        }

        public async void RefreshPage(object sender, EventArgs e)
        {
        Services.HotelAPIService _api = new Services.HotelAPIService();
        Models.About about = await _api.GetAboutAsync();
            if (about != null)
            {
                GetValuesToPage(about);
            }
        return;
        }

    public void GetValuesToPage(Models.About about)
        {
            if (about.AboutHotelHtmlContent != null && about.AboutHotelHtmlContent != string.Empty)
            {

                // Create a WebView to display HTML content
                var webView = new WebView
                {
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                // HTML content for the "About our Hotel" page
                var htmlContent = about.AboutHotelHtmlContent;

                // Set the HTML content source for the WebView
                webView.Source = new HtmlWebViewSource
                {
                    Html = htmlContent
                };

                // Add the WebView to the content of the page
                Content = new StackLayout
                {
                    Children = { webView }
                };

            }
        }
    protected override bool OnBackButtonPressed()
        {
            System.Threading.Tasks.Task.Run(async () => { await Shell.Current.GoToAsync("//HomePage"); }).Wait();
            return true;
        }
    }
}