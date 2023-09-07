using Android.Content;
using HotelPtyxiaki.Controls;
using HotelPtyxiaki.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidX.CardView.Widget;
using Android.Widget;

[assembly: ExportRenderer(typeof(AndroidCardView), typeof(AndroidCardViewRenderer))]
namespace HotelPtyxiaki.Droid.Renderers
{
    public class AndroidCardViewRenderer : ViewRenderer<AndroidCardView, CardView>
    {
        public AndroidCardViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<AndroidCardView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    // Create a CardView instance
                    var cardView = new CardView(Context);
                    cardView.UseCompatPadding = true; // Enables compatibility padding
                    cardView.CardElevation = 8; // Set elevation
                    cardView.Radius = 8; // Set corner radius

                    // Create a LinearLayout to contain the content
                    var linearLayout = new LinearLayout(Context);
                    linearLayout.Orientation = Android.Widget.Orientation.Vertical;

                    // Set the background color of the LinearLayout (content) to transparent
                    linearLayout.SetBackgroundColor(Android.Graphics.Color.Transparent);

                    // Add the LinearLayout as the content of the CardView
                    cardView.AddView(linearLayout);

                    // Set the CardView as the native control
                    SetNativeControl(cardView);
                }
            }
        }
    }
}
