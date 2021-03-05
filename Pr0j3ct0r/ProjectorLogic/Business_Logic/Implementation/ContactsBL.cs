using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Data_Access_Layer.Implementaion;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Implementation
{
    public class ContactsBL : IContactsBL
    {
        private ContactsDal contactsDal;

        public ContactsBL()
        {
            contactsDal = new ContactsDal();
        }

        public void DeleteContact(PlayerEntity player, PlayerEntity contactToDelete)
        {
            contactsDal.DeleteContactForUser(player.Username, contactToDelete.Username);
        }

        public ContactEntity GetPlayerInfo(string username)
        {
            ContactEntity value = new ContactEntity(username);
            List<ContactInfo> contactInfoFromDB =  contactsDal.GetPlayerInfo(username);
            foreach (ContactInfo item in contactInfoFromDB)
            {
                value.ContactInfo.Add(item.Type +" "+item.Val);
            }
            return value;
        }

        public List<ContactEntity> GetUserContactsOfPlayer(PlayerEntity player)
        {
            List<ContactInfo> fromDb = contactsDal.GetAllContactsOfUsername(player.Username);//contact info from the db, need to convert to contact entity
            List<ContactEntity> hirarchial = new List<ContactEntity>();
            foreach (ContactInfo item in fromDb)
            {
                ContactEntity temp = hirarchial.FirstOrDefault(person => person.UserName == item.Username);
                if (temp == null)
                {//not exsits in the hirarchial, add him in the hirarchial and his info
                    temp = new ContactEntity(item.Username);
                    temp.Add(item.Type +" "+ item.Val);
                    hirarchial.Add(temp);
                }
                else
                {//exsits in the hirarchial, add his info
                    temp.Add(item.Type +" "+ item.Val);
                }
            }
            return hirarchial;
        }

        public void InsertNewContactForPlayer(PlayerEntity player, PlayerEntity newContact)
        {
            contactsDal.InsertNewContact(player, newContact);
        }

        public void UpdatePlayerInfo(string username, string type, string value)
        {
            bool found = false;
            List<ContactInfo> list = contactsDal.GetPlayerInfo(username);
            for (int i = 0; i < list.ToArray().Length && found==false; i++)
            {
                if (list[i].Type == type)
                {
                    found = true;
                }
            }
            if (found)
            {
                contactsDal.UpdatePlayerInfo(username, type, value);
            }
            else
            {
                contactsDal.InsertPlayerInfo(username, type, value);
            }            
        }
    }
}
