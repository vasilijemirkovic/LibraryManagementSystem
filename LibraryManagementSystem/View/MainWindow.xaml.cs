using LibraryManagementSystem.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;

        public MainWindow() {
            InitializeComponent();
            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;

            SetPlaceholder(TitleBox, "Title");
            SetPlaceholder(AuthorBox, "Author");
            SetPlaceholder(SearchBox, "Search...");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder) {

            textBox.GotFocus += (s, e) => {
                if (textBox.Text == placeholder) {
                    textBox.Text = "";
                    textBox.Foreground = Brushes.White;
                }
            };

            textBox.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(textBox.Text)) {
                    textBox.Text = placeholder;
                    textBox.Foreground = Brushes.Gray;
                }
            };

            if (string.IsNullOrWhiteSpace(textBox.Text)) {
                textBox.Text = placeholder;
                textBox.Foreground = Brushes.Gray;
            }
        }
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text) || string.IsNullOrWhiteSpace(AuthorBox.Text)) {
                MessageBox.Show("Please enter both title and author!");
                return;
            }

            if (TitleBox.Text.Length > 70 || AuthorBox.Text.Length > 70) {
                MessageBox.Show("Title and author cannot exceed 70 characters!");
                return;
            }

            bool bookAdded = mainViewModel.AddBook(TitleBox.Text, AuthorBox.Text);

            if (!bookAdded) {
                MessageBox.Show("This book already exists!");
                return;
            }

            TitleBox.Text = "";
            AuthorBox.Text = "";
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel.SelectedBook == null)
            {
                MessageBox.Show("Select a book first!");
                return;
            }

            var selected = mainViewModel.SelectedBook;
            var editWindow = new EditBookWindow(selected.Title, selected.Author);
            editWindow.Owner = this;

            if(editWindow.ShowDialog() == true)
            {
                bool success = mainViewModel.EditBook(selected.Id, editWindow.NewTitle, editWindow.NewAuthor);

                if (!success) MessageBox.Show("Failed to edit book!");
            }
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel.SelectedBook == null)
            {
                MessageBox.Show("Select a book first!");
                return;
            }

            var selected = mainViewModel.SelectedBook;
            var result = MessageBox.Show($"Are you sure you want to delete '{selected.Title}' by {selected.Author}?",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes) {
                mainViewModel.RemoveBook(mainViewModel.SelectedBook.Id);
            }
        }

        private void BorrowReturn_Click(object sender, RoutedEventArgs e){
            if (mainViewModel.SelectedBook == null) {
                MessageBox.Show("Select a book first!");
                return;
            }

            var selected = mainViewModel.SelectedBook;

            if (selected.IsBorrowed) {
                mainViewModel.ReturnBook(selected.Id);
            }
            else {
                mainViewModel.BorrowBook(selected.Id);
            }
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (SearchBox.Text == "Search...") return;

            mainViewModel.Search(SearchBox.Text);
        }

        private void StatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainViewModel == null) return;
            var selected = (StatusFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string searchText = SearchBox.Text == "Search ..." ? "" : SearchBox.Text;
            mainViewModel.Search(searchText, selected);
        }
    }
}
