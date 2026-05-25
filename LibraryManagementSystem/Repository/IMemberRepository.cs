using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repository
{
    interface IMemberRepository
    {
        ObservableCollection<Member> GetAll();
        Task<bool> Add(Member member);
        Task<bool> Remove(int id);
        Task<bool> Edit(int id, string name, string email, string phone);
    }
}
