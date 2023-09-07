using Xamarin.Forms;

// Replace with YOUR namespace.
namespace HotelPtyxiaki.Controls
{
    /// <summary>
    /// NOTE: Requires custom renderer on each platform.
    /// </summary>
    public class OKCancelDatePicker : DatePicker
    {

        public static readonly BindableProperty UserCancelledProperty = BindableProperty.Create(nameof(UserCancelled), typeof(bool), typeof(OKCancelDatePicker), false);
        /// <summary>
        /// Bind to "UserCancelled", to propagate this change elsewhere (e.g. to a VM, or to trigger some logic).
        /// </summary>
        public bool UserCancelled
        {
            get => (bool)GetValue(UserCancelledProperty);
            set => SetValue(UserCancelledProperty, value);
        }

        /// <summary>
        /// Optionally add code here. Though usually you'll detect the change by binding to UserCancelled.
        /// </summary>
        public void OnPickerClosed()
        {
                this.UserCancelled = UserCancelled;
        }
    }
}