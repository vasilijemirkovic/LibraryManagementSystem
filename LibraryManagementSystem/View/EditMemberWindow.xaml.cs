using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryManagementSystem.View
{
    /// <summary>
    /// Interaction logic for EditMemberWindow.xaml
    /// </summary>
    public partial class EditMemberWindow : Window
    {
        public string? NewName { get; private set; }
        public string? NewEmail { get; private set; }
        public string? NewPhone { get; private set; }

        public EditMemberWindow(string currentName, string currentEmail, string currentPhone)
        {
            InitializeComponent();
            NameBox.Text = currentName;
            EmailBox.Text = currentEmail;
            PhoneBox.Text = currentPhone;
        }

        private void Save_Click(object sender, RoutedEventArgs e) {

            if(string.IsNullOrEmpty(NameBox.Text) ||
               string.IsNullOrEmpty(EmailBox.Text) ||
               string.IsNullOrEmpty(PhoneBox.Text)){
                MessageBox.Show("Please enter all fields!");
                return;
            }

            NewName = NameBox.Text;
            NewEmail = EmailBox.Text;
            NewPhone = PhoneBox.Text;
            DialogResult = true;
            Close();
        }
    }
}
