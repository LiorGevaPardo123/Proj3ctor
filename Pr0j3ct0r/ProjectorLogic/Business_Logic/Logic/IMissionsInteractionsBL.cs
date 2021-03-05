using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Logic
{
    public interface IMissionsInteractionsBL
    {
        List<Mission> GetNextMissions(Mission m);
        List<Mission> GetPreviousMissions(Mission m);
        void CreateInteractionsGraph(int projectId);
    }
}
