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
        public Task<bool> Add(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Borrow(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(int id, string newTitle, string newAuthor)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Return(int id)
        {
            throw new NotImplementedException();
        }
    }
}
