using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LibraryManagementSystem
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Library library = new Library();
        public Book SelectedBook {  get; set; }

        public ObservableCollection<Book> FilteredBooks { get; set; }

        public ObservableCollection<Book> Books { get; set; }

        public MainViewModel()
        {
            Books = library.GetBooks();
            FilteredBooks = new ObservableCollection<Book>(Books);
        }
        public ObservableCollection<Book> GetBooks()
        {
            return Books;
        }
        public void AddBook(string title, string author)
        {
            library.addBook(title, author);
        }
        public void RemoveBook(int id)
        {
            library.removeBook(id);
        }
        public void BorrowBook(int id)
        {
            library.borrowBook(id);
        }
        public void ReturnBook(int id)
        {
            library.returnBook(id);
        }

        public void Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                FilteredBooks = new ObservableCollection<Book>(Books);
            }
            else
            {
                var filtered = Books
                    .Where(b => b.Title.ToLower().Contains(search.ToLower())
                             || b.Author.ToLower().Contains(search.ToLower()));

                FilteredBooks = new ObservableCollection<Book>(filtered);
            }
        }


    }
}
