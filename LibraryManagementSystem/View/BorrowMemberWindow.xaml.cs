using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BorrowMemberWindow.xaml
    /// </summary>
    public partial class BorrowMemberWindow : Window
    {
        public Member? SelectedMember { get; private set; }

        public BorrowMemberWindow(ObservableCollection<Member> members)
        {
            InitializeComponent();
            MembersGrid.ItemsSource = members;
        }

        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            SelectedMember = MembersGrid.SelectedItem as Member;

            if (SelectedMember == null)
            {
                MessageBox.Show("Select a member first!");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
