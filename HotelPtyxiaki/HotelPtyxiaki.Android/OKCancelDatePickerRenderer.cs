using Android.App;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
// Replace these with YOUR namespaces.
using HotelPtyxiaki.Controls;   // Contains OKCancelDatePicker.
using HotelPtyxiaki.Droid;   

[assembly: ExportRenderer(typeof(OKCancelDatePicker), typeof(OKCancelDatePickerRenderer))]
// Replace this with YOUR Android namespace.
namespace HotelPtyxiaki.Droid
{
    /// <summary>
    /// Based on Nick Kovalsky's https://stackoverflow.com/a/60786875/199364.
    /// </summary>
    public class OKCancelDatePickerRenderer : DatePickerRenderer
    {
        public OKCancelDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            //Disposing
            if (e.OldElement != null)
            {
                _element = null;
            }

            //Creating
            if (e.NewElement != null)
            {
                _element = e.NewElement as OKCancelDatePicker;
            }
        }

        protected OKCancelDatePicker _element;

        protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            // This mimics what the original renderer did.
            var dialog = new DatePickerDialog(Context, (o, e) =>
            {
                _element.Date = e.Date;
                ((IElementController)_element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
            }, year, month, day);

            // These use our custom actions when buttons pressed.
            dialog.SetButton((int)DialogButtonType.Positive, Context.Resources.GetString(global::Android.Resource.String.Ok), OnOk);
            dialog.SetButton((int)DialogButtonType.Negative, Context.Resources.GetString(global::Android.Resource.String.Cancel), OnCancel);

            return dialog;
        }

        private void OnCancel(object sender, DialogClickEventArgs e)
        {
            // This is what the original renderer did when Cancel pressed.
            _element.Unfocus();

            // This is our custom logic.
            _element.UserCancelled = true;
            _element?.OnPickerClosed();
        }
        private void OnOk(object sender, DialogClickEventArgs e)
        {
            // This is what the original renderer did when OK pressed.
            _element.Date = ((DatePickerDialog)sender).DatePicker.DateTime;
            _element.Unfocus();

            // This is our custom logic.
            _element.UserCancelled = false;
            _element?.OnPickerClosed();
        }
    }
}