using LibraryManagementSystem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text) || string.IsNullOrWhiteSpace(AuthorBox.Text))
            {
                MessageBox.Show("Please enter both title and author!");
                return;
            }

            mainViewModel.AddBook(TitleBox.Text, AuthorBox.Text);
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

            mainViewModel.RemoveBook(mainViewModel.SelectedBook.Id);
        }

        private void BorrowBook_Click(object sender, RoutedEventArgs e) {

            if (mainViewModel.SelectedBook == null)
            {
                MessageBox.Show("Select a book first!");
                return;
            }

            bool success = mainViewModel.BorrowBook(mainViewModel.SelectedBook.Id);

            if (!success) MessageBox.Show("The book is already borrowed!");
        }

        private void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel.SelectedBook == null)
            {
                MessageBox.Show("Select a book first!");
                return;
            }

            bool success = mainViewModel.ReturnBook(mainViewModel.SelectedBook.Id);

            if (!success) MessageBox.Show("The book is already returned!");
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) {
            mainViewModel.Search(SearchBox.Text);
        }
    }
}
