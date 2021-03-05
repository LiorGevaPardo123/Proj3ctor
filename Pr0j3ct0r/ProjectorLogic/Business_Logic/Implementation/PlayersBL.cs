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
    public class PlayersBL : IPlayersBL
    {
        private PlayerDal playerDal;

        public PlayersBL()
        {
            playerDal = new PlayerDal();
        }

        public bool AccessPermission(string username, string pass)
        {
            return playerDal.AccessPermission(username, pass);
        }

        public void CreateNewPlayer(PlayerEntity p)
        {
            playerDal.InsertNewPlayer(p);
        }

        public List<PlayerEntity> GetAllPlayers()
        {
            return playerDal.GetAllPlayers();
        }

        public List<PlayerEntity> GetAllPlayers(List<string> except)
        {
            return playerDal.GetAllPlayers().Where(player => !(except.Contains(player.Username))).ToList();
        }

        public PlayerEntity GetPlayerByUsername(string username)
        {
            return playerDal.GetPlayerEntity(username);
        }
        
    }
}
