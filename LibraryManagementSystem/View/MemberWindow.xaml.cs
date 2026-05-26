using LibraryManagementSystem.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;


namespace LibraryManagementSystem.View
{
    /// <summary>
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {

        private MainViewModel mainViewModel;

        public MemberWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.mainViewModel = mainViewModel;
            MembersGrid.ItemsSource = mainViewModel.Members;

            SetPlaceholder(NameBox, "Name");
            SetPlaceholder(EmailBox, "Email");
            SetPlaceholder(PhoneBox, "Phone number");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.Foreground = System.Windows.Media.Brushes.White;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text)) {
                    textBox.Text = placeholder;
                    textBox.Foreground = System.Windows.Media.Brushes.LightGray;
                }
            };

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholder;
                textBox.Foreground = System.Windows.Media.Brushes.LightGray;
            }
        }

        private async void AddMember_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "Name" || EmailBox.Text == "Email" || PhoneBox.Text == "Phone" ||
                string.IsNullOrWhiteSpace(NameBox.Text) || string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneBox.Text))
            {
                MessageBox.Show("Please enter name, email and phone!");
                return;
            }

            bool memberCanBeAdded = await mainViewModel.AddMember(NameBox.Text, EmailBox.Text, PhoneBox.Text);

            if (!memberCanBeAdded) {
                MessageBox.Show("This member already exists!");
                return;
            }

            NameBox.Text = "";
            EmailBox.Text = "";
            PhoneBox.Text = "";
        }

        private async void EditMember_Click(object sender, RoutedEventArgs e)
        {
            var selectedMember = MembersGrid.SelectedItem as Member;

            if (selectedMember == null)
            {
                //TODO: Convert it to the toast!
                MessageBox.Show("Select a member first!");
                return;
            }

            //TODO: create editMemberWindow!
            var editWindow = new EditMemberWindow(selectedMember.Name, selectedMember.Email, selectedMember.Phone);
            editWindow.Owner = this;

            if (editWindow.ShowDialog() == true)
            {
                bool memberEdited = await mainViewModel.EditMember(
                    selectedMember.Id,
                    editWindow.NewName!,
                    editWindow.NewEmail!,
                    editWindow.NewPhone!);

                if (!memberEdited)
                    MessageBox.Show("Failed to edit member!");
            }
        }
        private async void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            var selectedMember = MembersGrid.SelectedItem as Member;

            if (selectedMember == null)
            {
                //TODO: Convert it to the toast!
                MessageBox.Show("Select a member first!");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{selectedMember.Name}'?", "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes) await mainViewModel.RemoveMember(selectedMember.Id);

        }


    }
}
