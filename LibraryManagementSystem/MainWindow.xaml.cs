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
        Library library = new Library();

        public MainWindow()
        {
            InitializeComponent();
            BooksGrid.ItemsSource = library.GetBooks();
        }
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            library.addBook(TitleBox.Text, AuthorBox.Text);
            BooksGrid.ItemsSource = library.GetBooks();
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e) {

            var selected = (Book)BooksGrid.SelectedItem;

            if (selected != null) {
                library.removeBook(selected.Id);
                BooksGrid.ItemsSource = library.GetBooks();
            } else {
                MessageBox.Show("Select a book first!");
                return;
            }
        }
        private void BorrowBook_Click(object sender, RoutedEventArgs e) {

            var selected = (Book)BooksGrid.SelectedItem;

            if(selected != null && selected.IsBorrowed) {
                MessageBox.Show("The book is already borrowed!");
                return;
            }

            if (selected != null) {
                library.borrowBook(selected.Id);
                BooksGrid.ItemsSource = library.GetBooks();
            }

            if(selected == null) {
                MessageBox.Show("Select a book first!");
                return;
            }
        }

        private void ReturnBook_Click(object sender, RoutedEventArgs e) {

            var selected = (Book)BooksGrid.SelectedItem;

            if (selected != null && !selected.IsBorrowed)
            {
                MessageBox.Show("The book is already returned!");
                return;
            }

            if (selected != null)
            {
                library.returnBook(selected.Id);
                BooksGrid.ItemsSource = library.GetBooks();
            }

            if (selected == null)
            {
                MessageBox.Show("Select a book first!");
                return;
            }
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e){

            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                BooksGrid.ItemsSource = library.GetBooks();
                return;
            }

            string search = SearchBox.Text.ToLower();

            BooksGrid.ItemsSource = library.GetBooks()
                .Where(b => b.Title.ToLower().Contains(search)
                         || b.Author.ToLower().Contains(search))
                .ToList();
        }
    }
}
