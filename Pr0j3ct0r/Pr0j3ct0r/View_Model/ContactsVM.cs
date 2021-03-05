using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pr0j3ct0r.Memory;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;

namespace Pr0j3ct0r.View_Model
{
    public class ContactsVM : INotifyPropertyChanged
    {
        private ObservableCollection<ContactVM> personalContactList;
        public ObservableCollection<ContactVM> PersonalContactList 
        {
            get { return personalContactList; }
            set { personalContactList = value; OnPropertyRaised("PersonalContactList"); }
        }

        private ObservableCollection<PlayerVM> allPlayers;
        public ObservableCollection<PlayerVM> AllPlayers
        {
            get { return allPlayers; }
            set { allPlayers = value; OnPropertyRaised("AllPlayers"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        #region BLPropeties 
        private IContactsBL contactsBL;
        private IPlayersBL playersBL;
        #endregion 

        public ContactsVM()
        {
            contactsBL = new ContactsBL();
            playersBL = new PlayersBL();
            PersonalContactList = new ObservableCollection<ContactVM>();
            AllPlayers = new ObservableCollection<PlayerVM>();
            InitializeVm();
        }
        public void InitializeVm()
        {
            PersonalContactList.Clear();
            AllPlayers.Clear();

            List<ContactEntity> contactsFromBL = contactsBL.GetUserContactsOfPlayer(
               new PlayerEntity() { Username = Cache.Instance.username }
               );
            foreach (ContactEntity contactBL in contactsFromBL)
            {
                PlayerEntity p = playersBL.GetPlayerByUsername(contactBL.UserName);
                PersonalContactList.Add(new ContactVM(contactBL.UserName, contactBL.ContactInfo, p.FirstName, p.LastName));
            }

            List<PlayerEntity> playersFromBL = playersBL.GetAllPlayers(PersonalContactList.Select(c => c.UserName).Concat(new List<string>() { Cache.Instance.username }).ToList());
            foreach (PlayerEntity playerBL in playersFromBL)
            {
                AllPlayers.Add(new PlayerVM(playerBL.Username, playerBL.FirstName, playerBL.LastName));
            }
        }        
        public void ExcludeUsernames(List<string> usernames)
        {
            foreach (string username in usernames)
            {
                var contact = personalContactList.Where(p => p.UserName.Equals(username)).FirstOrDefault();
                if (contact == null)
                {
                    continue;
                }
                personalContactList.Remove(contact);
            }
        }
        public void AddContact(PlayerVM temp)
        {
            contactsBL.InsertNewContactForPlayer(new PlayerEntity() { Username = Cache.Instance.username },
                new PlayerEntity() { FirstName = temp.FirstName, LastName = temp.LastName, Username = temp.UserName } );
        }
        public void DeleteContact(ContactVM temp)
        {
            contactsBL.DeleteContact(new PlayerEntity() { Username = Cache.Instance.username },
                new PlayerEntity() { Username = temp.UserName });
        }
    }
}
