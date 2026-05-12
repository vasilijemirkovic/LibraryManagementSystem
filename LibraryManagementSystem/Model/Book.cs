using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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

        public string Status => IsBorrowed ? "NO" : "YES";

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
