using LibraryManagementSystem.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Book : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int id;
        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        private string title = "";
        public string Title
        {
            get => title;
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }

        private string author = "";
        public string Author
        {
            get => author;
            set { author = value; OnPropertyChanged(nameof(Author)); }
        }

        private bool isBorrowed;
        public bool IsBorrowed
        {
            get => isBorrowed;
            set
            {
                isBorrowed = value;
                OnPropertyChanged(nameof(IsBorrowed));
                OnPropertyChanged(nameof(Status));
            }
        }

        private int? memberId;
        public int? MemberId
        {
            get => memberId;
            set { memberId = value; OnPropertyChanged(nameof(MemberId)); }
        }

        private Member? borrowedBy;
        public Member? BorrowedBy
        {
            get => borrowedBy;
            set { borrowedBy = value; OnPropertyChanged(nameof(BorrowedBy)); }
        }

        public string Status => IsBorrowed ? "NO" : "YES";

        private DateTime? borrowedDate;
        public DateTime? BorrowedDate
        {
            get => borrowedDate;
            set
            {
                borrowedDate = value;
                OnPropertyChanged(nameof(BorrowedDate));
                OnPropertyChanged(nameof(DueDate));
            }
        }
        public DateTime? DueDate => BorrowedDate?.AddDays(14);

        public Book() { }

        public Book(int id, string title, string author)
        {
            Id = id;
            Title = title;
            Author = author;
            IsBorrowed = false;
        }
    }
}
