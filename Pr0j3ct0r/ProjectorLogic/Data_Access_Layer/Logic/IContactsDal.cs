using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Logic
{
    interface IContactsDal
    {
        List<ContactInfo> GetAllContactsOfUsername(string username);
        void InsertNewContact(PlayerEntity player, PlayerEntity player2);
        void DeleteContactForUser(string username1, string username2);
        List<ContactInfo> GetPlayerInfo(string username);
        void InsertPlayerInfo(string username, string type, string value);
        void UpdatePlayerInfo(string username, string type, string value);
    }
}
