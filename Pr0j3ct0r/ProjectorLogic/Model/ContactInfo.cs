using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Model
{
    public class ContactInfo
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string val;
        public string Val
        {
            get { return val; }
            set { val = value; }
        }

        public ContactInfo()
        {

        }

        public ContactInfo(string username, string type, string val)
        {
            Username = username;
            Type = type;
            Val = val;
        }
    }
}
