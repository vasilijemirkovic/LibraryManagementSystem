using LibraryManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;


namespace LibraryManagementSystem
{
    internal class Library
    {

        private readonly LibraryContext context;
        private ObservableCollection<Book> books;


        public Library() {
            context = new LibraryContext();
            context.Database.EnsureCreated();
            books = new ObservableCollection<Book>(context.Books.ToList());
        }

        public bool addBook(string title, string author) {

            bool duplicate = books.Any(book => book.Title.ToLower() == title.ToLower() && book.Author.ToLower() == author.ToLower());
            if (duplicate) return false;

            var bookToAdd = new Book { Title = title, Author = author };
            context.Books.Add(bookToAdd);
            context.SaveChanges();
            books.Add(bookToAdd);
            return true;
        }

        public bool removeBook(int Id)
        {
            var bookToDelete = books.FirstOrDefault(b => b.Id == Id);

            if (bookToDelete == null) return false;

            context.Books.Remove(bookToDelete);
            context.SaveChanges();
            books.Remove(bookToDelete);
            return true;
        }

        public bool borrowBook(int Id)
        {
            var bookToBorrow = books.FirstOrDefault(b => b.Id == Id);

            if (bookToBorrow == null || bookToBorrow.IsBorrowed) return false;

            bookToBorrow.IsBorrowed = true;
            context.SaveChanges();
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

        public bool EditBook(int id, string newTitle, string newAuthor)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            book.Title = newTitle;
            book.Author = newAuthor;
            saveToFile();
            return true;
        }
    }
}
