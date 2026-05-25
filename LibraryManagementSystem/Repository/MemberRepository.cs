using LibraryManagementSystem.Data;
using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repository
{
    class MemberRepository : IMemberRepository
    {
        private readonly LibraryContext context;
        private ObservableCollection<Member> members;

        public MemberRepository(LibraryContext context)
        {
            this.context = context;
            members = new ObservableCollection<Member>(context.Members.ToList());
        }

        public async Task<bool> Add(Member member)
        {
            bool duplicate = members.Any(m =>
                m.Name.ToLower() == member.Name.ToLower() &&
                m.Email.ToLower() == member.Email.ToLower());

            if (duplicate) return false;

            context.Members.Add(member);
            await context.SaveChangesAsync();
            members.Add(member);
            return true;
        }

        public async Task<bool> Edit(int id, string name, string email, string phone)
        {
            var member = members.FirstOrDefault(m => m.Id == id);
            if (member == null) return false;

            member.Name = name;
            member.Email = email;
            member.Phone = phone;
            await context.SaveChangesAsync();
            return true;
        }

        public ObservableCollection<Member> GetAll()
        {
            return members;
        }

        public async Task<bool> Remove(int id)
        {
            var member = members.FirstOrDefault(m => m.Id == id);
            if (member == null) return false;

            context.Members.Remove(member);
            await context.SaveChangesAsync();
            members.Remove(member);
            return true;
        }
    }
}
