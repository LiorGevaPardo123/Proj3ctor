using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    public class ContactVM
    {
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private ObservableCollection<string> info;
        public ObservableCollection<string> Info
        {
            get { return info; }
            set { info = value; }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        
        public ContactVM(string userName, List<string> info, string firstName, string lastName)
        {
            UserName = userName;
            Info = new ObservableCollection<string>(info);
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactVM(string username, string firstName, string lastName)
        {
            userName = username;
            this.firstName = firstName;
            this.lastName = lastName;
            Info = new ObservableCollection<string>();
        }

        public string DisplayName
        {
            get
            {
                return FirstName[0] +""+ LastName[0];
            }
        }
    }
}
