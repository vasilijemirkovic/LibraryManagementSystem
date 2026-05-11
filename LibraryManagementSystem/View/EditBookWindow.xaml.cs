using System.Windows;

namespace LibraryManagementSystem.View
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        public string NewTitle { get; private set; }
        public string NewAuthor { get; private set; }
        public EditBookWindow(string currentTitle, string currentAuthor)
        {
            InitializeComponent();
            TitleBox.Text = currentTitle;
            AuthorBox.Text = currentAuthor;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text) || string.IsNullOrWhiteSpace(AuthorBox.Text))
            {
                MessageBox.Show("Please enter both title and author!");
                return;
            }

            if(TitleBox.Text.Length > 70 || AuthorBox.Text.Length > 70)
            {
                MessageBox.Show("Title and author cannot exceed 70 characters!");
                return;
            }

            NewTitle = TitleBox.Text;
            NewAuthor = AuthorBox.Text;
            DialogResult = true;
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
