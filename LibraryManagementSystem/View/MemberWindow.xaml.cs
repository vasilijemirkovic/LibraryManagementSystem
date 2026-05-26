using System.Windows;
using System.Windows.Controls;


namespace LibraryManagementSystem.View
{
    /// <summary>
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {

        private MainViewModel mainViewModel;

        public MemberWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.mainViewModel = mainViewModel;
            MembersGrid.ItemsSource = mainViewModel.Members;

            SetPlaceholder(NameBox, "Name");
            SetPlaceholder(EmailBox, "Email");
            SetPlaceholder(PhoneBox, "Phone number");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.Foreground = System.Windows.Media.Brushes.White;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text)) {
                    textBox.Text = placeholder;
                    textBox.Foreground = System.Windows.Media.Brushes.LightGray;
                }
            };

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholder;
                textBox.Foreground = System.Windows.Media.Brushes.LightGray;
            }
        }



    }
}
