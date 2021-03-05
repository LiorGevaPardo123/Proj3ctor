using Pr0j3ct0r.View_Model;
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

namespace Pr0j3ct0r.User_Interface
{
    /// <summary>
    /// Interaction logic for ContactsPage.xaml
    /// </summary>
    public partial class ContactsPage : Window
    {
        int count = 0;
        public ContactsVM vm;
        public ContactsPage()
        {
            InitializeComponent();
            vm = new ContactsVM();
            this.DataContext = vm;           
        }

        private void UpdateUserMsg()
        {
            if (this.vm.PersonalContactList.Count == 0)
            {
                this.conMsg.Visibility = Visibility.Visible;
                this.conSV.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.conMsg.Visibility = Visibility.Collapsed;
                this.conSV.Visibility = Visibility.Visible;
            }            
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(searchUserNameTxt.Text))
                return true;
            else
                return ((item as PlayerVM).UserName.IndexOf(searchUserNameTxt.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void NewContactBtn_Click(object sender, RoutedEventArgs e)
        {
            count++;
            if (count%2 == 1)//מעבר לרשימת שחקנים במערכת
            {
                this.contactListGrid.Visibility = Visibility.Collapsed; 
                this.searchGrid.Visibility = Visibility.Visible;      
            }
            else //מעבר לרשימת אנשי קשר 
            {
                this.contactListGrid.Visibility = Visibility.Visible;
                this.searchGrid.Visibility = Visibility.Collapsed;
                UpdateUserMsg();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayerVM temp = this.allPlayerList.SelectedValue as PlayerVM;
            if (temp != null)
            {
                string selectedUserName = temp.UserName;
                this.vm.AddContact(temp);
                this.vm.AllPlayers.Remove(temp);
                vm.InitializeVm();
            }
        }

        private void SearchUserNameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.searchUserNameTxt.Equals(""))
            {
                CollectionViewSource.GetDefaultView(allPlayerList.ItemsSource).Refresh();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactVM temp = this.currentContacts.SelectedValue as ContactVM;
            if (temp != null)
            {
                this.vm.DeleteContact(temp);
                this.vm.PersonalContactList.Remove(temp);

                vm.InitializeVm();
            }
            UpdateUserMsg();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.allPlayerList.ItemsSource);
            view.Filter = UserFilter;
            UpdateUserMsg();
        }
    }
}
