using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Logic
{
    public interface IContactsBL
    {
        List<ContactEntity> GetUserContactsOfPlayer(PlayerEntity player);
        void InsertNewContactForPlayer(PlayerEntity player, PlayerEntity newContact);
        void DeleteContact(PlayerEntity player, PlayerEntity contactToDelete);
        ContactEntity GetPlayerInfo(string username);
        void UpdatePlayerInfo(string username, string type, string value);
    }
}
