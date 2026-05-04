using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace LibraryManagementSystem
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Library library = new Library();
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Book> filteredBooks;

        public ObservableCollection<Book> FilteredBooks
        {
            get => filteredBooks;
            set
            {
                filteredBooks = value;
                OnPropertyChanged(nameof(FilteredBooks));
            }
        }
        public ObservableCollection<Book> Books => library.GetBooks();

        private Book selectedBook;
        public Book SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }
        public MainViewModel()
        {
            filteredBooks = new ObservableCollection<Book>(Books);
            Books.CollectionChanged += (s, e) => Search(_lastSearch);
        }

        private string _lastSearch = "";

        public void AddBook(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                return;
            }
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
            _lastSearch = search ?? "";

            if (string.IsNullOrWhiteSpace(_lastSearch))
            {
                FilteredBooks = new ObservableCollection<Book>(Books);
                return;
            }

            var lower = _lastSearch.ToLower();
            var filtered = Books.Where(b =>
                b.Title.ToLower().Contains(lower) ||
                b.Author.ToLower().Contains(lower));

            FilteredBooks = new ObservableCollection<Book>(filtered);
        }
    }
}
