using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Logic
{
    interface IMissionDal
    {
        void CreateNewMission(Mission m);
        bool IsMissionExists(Mission m);
        void UpdateMission(Mission m);
        void DeleteMission(Mission m);
        List<Mission> GetAllMissions();
        Mission GetMission(string name, int pId);             
    }
}
