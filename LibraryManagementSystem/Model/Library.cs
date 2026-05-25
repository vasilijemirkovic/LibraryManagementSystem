using LibraryManagementSystem.Data;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using static System.Reflection.Metadata.BlobBuilder;


namespace LibraryManagementSystem
{
    internal class Library
    {

        private readonly IBookRepository bookRepository;
        private readonly IMemberRepository memberRepository;


        public Library() {
            var context = new LibraryContext();
            bookRepository = new BookRepository(context);
            memberRepository = new MemberRepository(context);
        }

        public async Task<bool> addBook(string title, string author) {
            var book = new Book { Title = title, Author = author };
            return await bookRepository.Add(book);
        }

        public async Task<bool> removeBook(int Id)
        {
            return await bookRepository.Remove(Id);
        }

        public async Task<bool> editBook(int id, string newTitle, string newAuthor)
        {
            return await bookRepository.Edit(id, newTitle, newAuthor);
        }

        public async Task<bool> borrowBook(int Id, int memberId)
        {
            return await bookRepository.Borrow(Id, memberId);
        }

        public async Task<bool> returnBook(int Id)
        {
            return await bookRepository.Return(Id);
        }
        public ObservableCollection<Book> getBooks() {
            return bookRepository.GetAll();
        }

        public async Task<bool> AddMember(string name, string email, string phone)
        {
            var member = new Member { Name = name, Email = email, Phone = phone };
            return await memberRepository.Add(member);
        }

        public async Task<bool> RemoveMember(int id)
        {
            return await memberRepository.Remove(id);
        }

        public async Task<bool> EditMember(int id, string name, string email, string phone)
        {
            return await memberRepository.Edit(id, name, email, phone);
        }

        public ObservableCollection<Member> GetMembers()
        {
            return memberRepository.GetAll();
        }
    }
}
