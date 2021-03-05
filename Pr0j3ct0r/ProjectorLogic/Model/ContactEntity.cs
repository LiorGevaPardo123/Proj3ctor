using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Model
{
    public class ContactEntity
    {
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private List<string> contactInfo;
        public List<string> ContactInfo
        {
            get { return contactInfo; }
            set { contactInfo = value; }
        }

        public ContactEntity(string userName)
        {
            UserName = userName;
            ContactInfo = new List<string>();
        }

        public void Add(string info)
        {
            ContactInfo.Add(info);
        }
    }
}
