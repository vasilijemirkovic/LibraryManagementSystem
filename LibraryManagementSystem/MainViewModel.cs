    using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace LibraryManagementSystem
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly Library library = new Library();

        private string _lastSearch = "";
        private string _lastStatus = "All";

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

        private Book? selectedBook;
        public Book? SelectedBook
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

        public async Task<bool> AddBook(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)){
                return false;
            }
            if (title.Length > 70 || author.Length > 70){
                return false;
            }
            return await library.addBook(title, author);
        }

        public async Task<bool> RemoveBook(int id)
        {
            return await library.removeBook(id);
        }

        public async Task<bool> BorrowBook(int id)
        {
            return await library.borrowBook(id);
        }

        public async Task<bool> ReturnBook(int id)
        {
            return await library.returnBook(id);
        }

        public void Search(string search, string? status = null)
        {
            _lastSearch = search ?? "";
            _lastStatus = status ?? _lastStatus;

            var filtered = Books.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(_lastSearch))
            {
                var lower = _lastSearch.ToLower();
                filtered = filtered.Where(b =>
                    b.Title.ToLower().Contains(lower) ||
                    b.Author.ToLower().Contains(lower));
            }

            if (_lastStatus == "Available")
                filtered = filtered.Where(b => !b.IsBorrowed);
            else if (_lastStatus == "Borrowed")
                filtered = filtered.Where(b => b.IsBorrowed);

            FilteredBooks = new ObservableCollection<Book>(filtered);
        }
        public async Task<bool> EditBook(int id, string newTitle, string newAuthor)
        {
            if (string.IsNullOrWhiteSpace(newTitle) || string.IsNullOrWhiteSpace(newAuthor)) return false;

            if(newTitle.Length > 70 || newAuthor.Length > 70) return false;

            return await library.EditBook(id, newTitle, newAuthor);
        }
    }
}
