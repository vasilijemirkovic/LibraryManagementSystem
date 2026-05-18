using LibraryManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repository
{
    internal class BookRepository : IBookRepository
    {
        private readonly LibraryContext context;
        private ObservableCollection<Book> books;
        public BookRepository(LibraryContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
            books = new ObservableCollection<Book>(context.Books.ToList());
        }

        public async Task<bool> Add(Book bookToAdd)
        {
            bool duplicate = books.Any(b =>
                b.Title.ToLower() == bookToAdd.Title.ToLower() &&
                b.Author.ToLower() == bookToAdd.Author.ToLower());

            if (duplicate) return false;

            context.Books.Add(bookToAdd);
            await context.SaveChangesAsync();
            books.Add(bookToAdd);
            return true;
        }
        public async Task<bool> Remove(int id)
        {
            var bookToRemove = books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove == null) return false;

            context.Books.Remove(bookToRemove);
            await context.SaveChangesAsync();
            books.Remove(bookToRemove);
            return true;
        }
        public async Task<bool> Edit(int id, string newTitle, string newAuthor)
        {
            var bookToEdit = books.FirstOrDefault(b => b.Id == id);
            if (bookToEdit == null) return false;

            bookToEdit.Title = newTitle;
            bookToEdit.Author = newAuthor;
            await context.SaveChangesAsync();
            return true;
        }
        public Task<bool> Borrow(int id)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Book> GetAll()
        {
            return books;
        }

        public Task<bool> Return(int id)
        {
            throw new NotImplementedException();
        }
    }
}
