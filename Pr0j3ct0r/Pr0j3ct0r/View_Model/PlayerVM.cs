using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    public class PlayerVM
    {
        public PlayerVM(string userName, string firstName, string lastName)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }
        public PlayerVM()
        {
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value.Trim(); }
        }       

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value.Trim(); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value.Trim(); }
        }
    }
}
