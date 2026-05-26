using LibraryManagementSystem.Model;
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
        public ObservableCollection<Book> Books => library.getBooks();
        public ObservableCollection<Member> Members => library.GetMembers();

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
        public int TotalBooks => Books.Count;
        public int AvailableBooks => Books.Count(b => !b.IsBorrowed);
        public int BorrowedBooks => Books.Count(b => b.IsBorrowed);

        public MainViewModel()
        {
            filteredBooks = new ObservableCollection<Book>(Books);
            Books.CollectionChanged += (s, e) =>
            {
                Search(_lastSearch);
                OnPropertyChanged(nameof(TotalBooks));
                OnPropertyChanged(nameof(AvailableBooks));
                OnPropertyChanged(nameof(BorrowedBooks));
            };
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

        public async Task<bool> EditBook(int id, string newTitle, string newAuthor)
        {
            if (string.IsNullOrWhiteSpace(newTitle) || string.IsNullOrWhiteSpace(newAuthor)) return false;

            if (newTitle.Length > 70 || newAuthor.Length > 70) return false;

            return await library.editBook(id, newTitle, newAuthor);
        }

        public async Task<bool> RemoveBook(int id)
        {
            return await library.removeBook(id);
        }

        public async Task<bool> BorrowBook(int id, int memberId)
        {
            var result = await library.borrowBook(id, memberId);
            OnPropertyChanged(nameof(AvailableBooks));
            OnPropertyChanged(nameof(BorrowedBooks));
            return result;
        }

        public async Task<bool> ReturnBook(int id)
        {
            var result = await library.returnBook(id);
            OnPropertyChanged(nameof(AvailableBooks));
            OnPropertyChanged(nameof(BorrowedBooks));
            return result;
        }
        public async Task<bool> AddMember(string name, string email, string phone)
        {
            return await library.AddMember(name, email, phone);
        }

        public async Task<bool> RemoveMember(int id)
        {
            return await library.RemoveMember(id);
        }

        public async Task<bool> EditMember(int id, string name, string email, string phone)
        {
            return await library.EditMember(id, name, email, phone);
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
    }
}
