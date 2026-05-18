using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repository
{
    internal interface IBookRepository
    {
        ObservableCollection<Book> GetAll();
        Task<bool> Add(Book book);
        Task<bool> Remove(int id);
        Task<bool> Edit(int id, string newTitle, string newAuthor);
        Task<bool> Borrow(int id);
        Task<bool> Return(int id);
    }
}
