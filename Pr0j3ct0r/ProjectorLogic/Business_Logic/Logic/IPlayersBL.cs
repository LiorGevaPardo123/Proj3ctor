using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Logic
{
    public interface IPlayersBL
    {
        PlayerEntity GetPlayerByUsername(string username);
        List<PlayerEntity> GetAllPlayers();
        List<PlayerEntity> GetAllPlayers(List<string> except);
        bool AccessPermission(string username, string pass);
        void CreateNewPlayer(PlayerEntity p);
    }
}
