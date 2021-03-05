using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    class LogInVM: INotifyPropertyChanged
    {       
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
                OnPropertyRaised("Password");
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
        
        private IPlayersBL playersBL;
        public LogInVM()
        {
            playersBL = new PlayersBL();
        }
        public bool AccessPermission()
        {
            return playersBL.AccessPermission(username, password);
        }
    }
}
