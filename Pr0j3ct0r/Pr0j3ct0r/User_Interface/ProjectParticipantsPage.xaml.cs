using Pr0j3ct0r.Memory;
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
    /// Interaction logic for ProjectParticipantsPage.xaml
    /// </summary>
    public partial class ProjectParticipantsPage : Window
    {
        ProjectParticipantsVM vm;
        ContactsVM cvm;
        bool addP;
        public ProjectParticipantsPage(ProjectVM pvm)
        {
            addP = false;
            InitializeComponent();            
            this.vm = new ProjectParticipantsVM(pvm);
            this.participantsLV.DataContext = vm;
            if (pvm.Manager.UserName == Cache.Instance.username)
            {
                visGrid.Visibility = Visibility.Visible;
            }
            else
            {
                visGrid.Visibility = Visibility.Collapsed;
            }            
        }

        private void UpdateUserMsg()
        {
            if (this.currentPickListView.SelectedItems.Count == 0)
            {
                this.participantsMsg.Visibility = Visibility.Visible;
                this.currentContacts.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.participantsMsg.Visibility = Visibility.Collapsed;
                this.currentContacts.Visibility = Visibility.Visible;
            }            
        }
        private void switchParticipantWithContactViewBtnClick(object sender, RoutedEventArgs e)
        {            
            if (!addP)
            {
                this.cvm = new ContactsVM();
                this.cvm.ExcludeUsernames(this.vm.ProjectVM.Participants.Select(u=>u.UserName).ToList());              
                this.currentContacts.DataContext = cvm;  
                currentContacts.Visibility = Visibility.Visible;
                participantsLV.Visibility = Visibility.Collapsed;
                UpdateUserMsg();
                switchParticipantWithContactViewBtn.Content = "Save";                  
                currentPickListView.UnselectAll();
            }
            else
            {
                var selectedContacts = currentPickListView.SelectedItems;
                List<string> usernames = new List<string>();
                foreach (var item in selectedContacts)
                {
                    ContactVM temp = item as ContactVM;
                    if (temp != null)
                    {
                        usernames.Add((temp).UserName);
                    }                    
                }
                this.vm.AddParticipants(usernames);
                this.participantsMsg.Visibility = Visibility.Collapsed;                
                currentContacts.Visibility = Visibility.Collapsed;
                participantsLV.Visibility = Visibility.Visible;                
                switchParticipantWithContactViewBtn.Content = "Add Participant";
            }
            addP = !addP;
        }

        private void RemoveParticipantBtnClick(object sender, RoutedEventArgs e)
        {
           PlayerVM player = participantsList.SelectedItem as PlayerVM;
            if (player != null)
            {
                this.vm.RemoveParticipant(player.UserName);
            }
        }
    }
}
