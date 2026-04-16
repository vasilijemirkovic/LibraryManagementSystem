using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LibraryManagementSystem
{
    internal class Library
    {
        private int nextId = 1;
        private ObservableCollection<Book> books = new ObservableCollection<Book>();

        public void addBook(string title, string author) { books.Add(new Book(nextId, title, author)); nextId++; }
        public string removeBook(int Id)
        {
            var bookToDelete = books.FirstOrDefault(b => b.Id == Id);

            if (bookToDelete == null) return "Not found!";

            else
            {
                books.Remove(bookToDelete);
                return "Deleted!";
            }

        }
        public void showBooks()
        {
            foreach (var eachBook in books)
            {
                Console.WriteLine("Id: "
                    + eachBook.Id + ", name: "
                    + eachBook.Title + ", author: "
                    + eachBook.Author + ", available: " +
                    (eachBook.IsBorrowed ? "NO" : "YES"));
            }
        }

        public string borrowBook(int Id)
        {
            var bookToBorrow = books.FirstOrDefault(b => b.Id == Id);

            if (bookToBorrow == null) return "Not found!";

            else
            {
                bookToBorrow.IsBorrowed = true;
                return "Borrowed!";
            }
        }

        public string returnBook(int Id)
        {
            var bookToReturn = books.FirstOrDefault(b => b.Id == Id);

            if (bookToReturn == null) return "Not found!";

            else if (bookToReturn.IsBorrowed == false) return "Book is already in library!";

            else
            {
                bookToReturn.IsBorrowed = false;
                return "Borrowed!";
            }

        }
        public ObservableCollection<Book> GetBooks() {
            return books;
        }
    }
}
