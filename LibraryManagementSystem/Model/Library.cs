using LibraryManagementSystem.Data;
using LibraryManagementSystem.Repository;
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

        private readonly IBookRepository iBookRepository;


        public Library() {
            var context = new LibraryContext();
            iBookRepository = new BookRepository(context);
        }

        public async Task<bool> addBook(string title, string author) {
            var book = new Book { Title = title, Author = author };
            return await iBookRepository.Add(book);
        }

        public async Task<bool> removeBook(int Id)
        {
            return await iBookRepository.Remove(Id);
        }

        public async Task<bool> borrowBook(int Id)
        {
            return await iBookRepository.Borrow(Id);
        }

        public async Task<bool> returnBook(int Id)
        {
            return await iBookRepository.Return(Id);
        }
        public ObservableCollection<Book> GetBooks() {
            return iBookRepository.GetAll();
        }

        public async Task<bool> EditBook(int id, string newTitle, string newAuthor)
        {
            var bookToEdit = books.FirstOrDefault(b => b.Id == id);
            if (bookToEdit == null) return false;

            bookToEdit.Title = newTitle;
            bookToEdit.Author = newAuthor;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
