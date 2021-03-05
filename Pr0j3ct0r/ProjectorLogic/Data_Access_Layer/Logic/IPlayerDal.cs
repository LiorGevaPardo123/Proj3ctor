using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Logic
{
    interface IPlayerDal
    {
        void InsertNewPlayer(PlayerEntity player);
        bool IsPlayerExists(PlayerEntity player);
        void UpdatePlayer(PlayerEntity player);
        void DeletePlayer(PlayerEntity player);
        List<PlayerEntity> GetAllPlayers();
        PlayerEntity GetPlayerEntity(string username);
        bool AccessPermission(string username, string pass);
    }
}
