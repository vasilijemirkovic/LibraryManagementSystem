using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LibraryManagementSystem
{
    internal class Book : INotifyPropertyChanged
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }

        private bool isBorrowed;
        public string Status => IsBorrowed ? "NO" : "YES";



        public Book(int id, string title, string author)
        {
            this.Id = id;
            this.Title = title;
            this.Author = author;
            isBorrowed = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBorrowed
        {
            get => this.isBorrowed;
            set
            {
                isBorrowed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBorrowed)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

    }
}
