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
using System.Windows.Shapes;

namespace LibraryManagementSystem.View
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow(ObservableCollection<Book> books) {
            InitializeComponent();
            LoadReport(books);
        }

        private void LoadReport(ObservableCollection<Book> books)
        {
            int total = books.Count;
            int borrowedBooks = books.Count(eachBook => eachBook.IsBorrowed);
            int availableBooks = total - borrowedBooks;

            TotalCount.Text = total.ToString();
            AvailableCount.Text = availableBooks.ToString();
            BorrowedCount.Text = borrowedBooks.ToString();

            if (total == 0) {
                AvailablePercent.Text = "0%";
                BorrowedPercent.Text = "0%";
                return;
            }

            double availableRatio = (double)availableBooks / total;
            double borrowedRatio = (double)borrowedBooks / total;

            AvailablePercent.Text = $"{(int)(availableRatio * 100)}%";
            BorrowedPercent.Text = $"{(int)(borrowedRatio * 100)}%";

            AvailableBar.Width = availableRatio * 340;
            BorrowedBar.Width = borrowedRatio * 340;
        }
    }
}
