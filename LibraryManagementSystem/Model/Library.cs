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
        private readonly string filePath = "books.json";
        private ObservableCollection<Book> books = new ObservableCollection<Book>();

        public Library() {
            loadFromFile();
        }

        public void addBook(string title, string author) {
            books.Add(new Book(nextId, title, author));
            nextId++;
            saveToFile();
        }

        public bool removeBook(int Id)
        {
            var bookToDelete = books.FirstOrDefault(b => b.Id == Id);

            if (bookToDelete == null) return false;

            books.Remove(bookToDelete);
            saveToFile();
            return true;
        }

        public bool borrowBook(int Id)
        {
            var bookToBorrow = books.FirstOrDefault(b => b.Id == Id);

            if (bookToBorrow == null || bookToBorrow.IsBorrowed) return false;

            bookToBorrow.IsBorrowed = true;
            saveToFile();
            return true;
        }

        public bool returnBook(int Id)
        {
            var bookToReturn = books.FirstOrDefault(b => b.Id == Id);

            if (bookToReturn == null || !bookToReturn.IsBorrowed) return false;

            bookToReturn.IsBorrowed = false;

            saveToFile();
            return true;

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
            if (!File.Exists(filePath)) return;
            
            var json = File.ReadAllText(filePath);
            
            var loadedBooks = JsonSerializer.Deserialize<ObservableCollection<Book>>(json);

            if (loadedBooks != null)
            {
                books = loadedBooks;
                nextId = books.Max(b => b.Id) + 1;
            }
        }
    }
}
