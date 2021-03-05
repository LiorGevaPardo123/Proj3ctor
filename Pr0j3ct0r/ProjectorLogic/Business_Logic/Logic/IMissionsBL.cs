using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Logic
{
    public interface IMissionsBL
    {
        List<Mission> GetInProcessMissions(Project project);
        List<Mission> GetDoneMissions(Project project);
        List<Mission> GetPendingMissions(Project project);
        void CreateMission(Mission m);
        void DeleteMission(Mission m);
        void UpdateMission(Mission m);
        List<Mission> GetAllMissions(Project project);        
    }
}
