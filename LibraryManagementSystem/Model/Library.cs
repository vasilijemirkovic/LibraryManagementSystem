using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;


namespace LibraryManagementSystem
{
    internal class Library
    {
        private int nextId = 1;
        private string filePath = "books.json";
        private ObservableCollection<Book> books = new ObservableCollection<Book>();

        public Library() {
            loadFromFile();
        }

        public void addBook(string title, string author) {
            books.Add(new Book(nextId, title, author));
            nextId++;
            saveToFile();
        }

        public string removeBook(int Id) {
            var bookToDelete = books.FirstOrDefault(b => b.Id == Id);

            if (bookToDelete == null)
            {
                saveToFile();
                return "Not found!";
            }

            else
            {
                books.Remove(bookToDelete);
                saveToFile();
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

            if (bookToBorrow == null)
            {
                saveToFile();
                return "Not found!";
            }

            else
            {
                bookToBorrow.IsBorrowed = true;
                saveToFile();
                return "Borrowed!";
            }
        }

        public string returnBook(int Id)
        {
            var bookToReturn = books.FirstOrDefault(b => b.Id == Id);

            if (bookToReturn == null)
            {
                saveToFile();
                return "Not found!";
            }

            else if (bookToReturn.IsBorrowed == false)
            {
                saveToFile();
                return "Book is already in library!";
            }

            else
            {
                bookToReturn.IsBorrowed = false;
                saveToFile();
                return "Borrowed!";
            }

        }
        public ObservableCollection<Book> GetBooks() {
            return books;
        }

        private void saveToFile()
        {
            var json = JsonSerializer.Serialize(books);
            File.WriteAllText(filePath, json);
        }

        private void loadFromFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var loadedBooks = JsonSerializer.Deserialize<ObservableCollection<Book>>(json);

                if (loadedBooks != null) {
                    books = loadedBooks;
                }
            }
            if (books.Any())
            {
                nextId = books.Max(b => b.Id) + 1;
            }
        }
    }
}
