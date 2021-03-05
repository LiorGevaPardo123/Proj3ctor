using Pr0j3ct0r.Memory;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    public class ProfilePageVM : INotifyPropertyChanged
    {
        private string phone;
        public string Phone
        {
            get {
                return phone;
            }
            set {
                phone = value;
                contactsBL.UpdatePlayerInfo(Cache.Instance.username, "Phone", phone);
                OnPropertyRaised("Phone");
            }
        }

        private string email;
        
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                contactsBL.UpdatePlayerInfo(Cache.Instance.username, "Email", email);
                OnPropertyRaised("Email");
            }
        }

        private ContactVM contact;
        public ContactVM Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        private PlayerVM player;
        public PlayerVM Player
        {
            get { return player; }
            set { player = value; OnPropertyRaised("Player"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private IPlayersBL playersBL;
        private IContactsBL contactsBL;
        public ProfilePageVM()
        {
            playersBL = new PlayersBL();
            contactsBL = new ContactsBL();

            PlayerEntity p = playersBL.GetPlayerByUsername(Cache.Instance.username);
            //PlayerEntity p = playersBL.GetPlayerByUsername("BotBurg3r");
            this.Player = new PlayerVM(p.Username, p.FirstName, p.LastName);  
            
            ContactEntity itemFromBL = this.contactsBL.GetPlayerInfo(p.Username);
            this.Contact = new ContactVM(p.Username ,p.FirstName, p.LastName);
            foreach (string infoItem in itemFromBL.ContactInfo)
            {
                this.Contact.Info.Add(infoItem);
            }
            this.phone = GetInfoProp("Phone");
            this.email = GetInfoProp("Email");
        }

        private string GetInfoProp(string name)
        {
            string s = contact.Info.FirstOrDefault(x => x.StartsWith(name));
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            else
            {
                return s.Split(' ')[1];
            }
        }
        private void SetInfoProp(string name, string value)
        {
            int place = contact.Info.ToList().FindIndex(x => x.StartsWith("Phone"));
            if (place == -1)
            {
                contact.Info.Add("Phone " + value);
            }
            else
            {
                contact.Info[place] = "Phone " + value;
            }
        }


    }
}
