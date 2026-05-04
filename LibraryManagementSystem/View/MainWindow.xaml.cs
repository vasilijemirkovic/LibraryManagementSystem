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
            mainViewModel.AddBook(TitleBox.Text, AuthorBox.Text);
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e) {


            var selected = mainViewModel.SelectedBook;

            if (selected == null) {
                MessageBox.Show("Select a book first!");
                return;
            }
            mainViewModel.RemoveBook(selected.Id);
        }
        private void BorrowBook_Click(object sender, RoutedEventArgs e) {

            var selected = mainViewModel.SelectedBook;

            if (selected == null)
            {
                MessageBox.Show("Select a book first!");
                return;
            }

            if (selected.IsBorrowed)
            {
                MessageBox.Show("The book is already borrowed!");
                return;
            }

            mainViewModel.BorrowBook(selected.Id);
        }

        private void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            var selected = mainViewModel.SelectedBook;

            if (selected == null)
            {
                MessageBox.Show("Select a book first!");
                return;
            }

            if (!selected.IsBorrowed)
            {
                MessageBox.Show("The book is already returned!");
                return;
            }

            mainViewModel.ReturnBook(selected.Id);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) {
            mainViewModel.Search(SearchBox.Text);
        }
    }
}
