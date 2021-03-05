using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    public class RegisterVM : INotifyPropertyChanged
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyRaised("FirstName"); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyRaised("LastName"); }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyRaised("Username"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                ConfirmPassword();
                OnPropertyRaised("Password");
            }
        }

        private string conPassword;
        public string ConPassword
        {
            get { return conPassword; }
            set
            {
                conPassword = value;
                ConfirmPassword();
                OnPropertyRaised("ConPassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }               
               
        private void ConfirmPassword()
        {
            string usermsg = "";
            if (!string.IsNullOrEmpty(this.firstName) && password.Contains(this.firstName))
                usermsg += "password contains first Name\n";
            if (!string.IsNullOrEmpty(this.lastName) && password.Contains(this.lastName))
                usermsg += "password contains last Name\n";
            IsAlertOnPassword = !string.IsNullOrEmpty(usermsg);
            UserAlertOnPassword = usermsg;
        }
        private bool isAlertOnPassword;
        public bool IsAlertOnPassword
        {
            get { return isAlertOnPassword; }
            set
            {
                isAlertOnPassword = value;
                OnPropertyRaised("IsAlertOnPassword");
            }
        }
        private string userAlertOnPassword;
        public string UserAlertOnPassword
        {
            get { return userAlertOnPassword; }
            set
            {
                userAlertOnPassword = value;
                OnPropertyRaised("UserAlertOnPassword");
            }
        }

        public bool ConfirmMatchPassword()
        {
            return conPassword.Equals(password);
        }
        private bool isAlertOnConPassword;
        public bool IsAlertOnConPassword
        {
            get { return isAlertOnConPassword; }
            set
            {
                isAlertOnConPassword = value;
                OnPropertyRaised("IsAlertOnConPassword");
            }
        }
        private string userAlertOnConfirmPass;
        public string UserAlertOnConfirmPass
        {
            get { return userAlertOnConfirmPass; }
            set
            {
                userAlertOnConfirmPass = value;
                OnPropertyRaised("UserAlertOnConfirmPass");
            }
        }

        public void sendDataForNewOPlayer()
        {
            playersBL.CreateNewPlayer(new PlayerEntity(firstName, lastName, username, password));
        }
        PlayersBL playersBL;
        public RegisterVM()
        {
            playersBL = new PlayersBL();
        }
    }
}
