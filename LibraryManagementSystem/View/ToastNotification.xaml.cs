using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LibraryManagementSystem.View
{
    public partial class ToastNotification : UserControl
    {
        public ToastNotification() {
            InitializeComponent();
        }

        public void Show(string message, bool success = true)
        {
            ToastMessage.Text = message;
            ToastBorder.Background = success
                ? new SolidColorBrush(Color.FromRgb(76, 175, 80))
                : new SolidColorBrush(Color.FromRgb(244, 67, 54));

            var storyboard = (Storyboard)Resources["ShowToast"];
            storyboard.Begin(this);
        }
    }
}