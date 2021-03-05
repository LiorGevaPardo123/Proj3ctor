using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Data_Access_Layer.Implementaion;
using ProjectorLogic.Data_Access_Layer.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Implementation
{
    public class MissionsBL : IMissionsBL
    {
        IMissionDal missionDal;
        IMissionsStatusDal missionsStatusDal;
        public MissionsBL()
        {
            missionDal = new MissionDal();
            missionsStatusDal = new MissionsStatusDal();
        }

        public void CreateMission(Mission m)
        {
            missionDal.CreateNewMission(m);            
        }

        public void DeleteMission(Mission m)
        {
            missionDal.DeleteMission(m);
        }

        public List<Mission> GetAllMissions(Project project)
        {
           return missionDal.GetAllMissions().Where(mission => mission.ProjectId.Equals(project.Code)).ToList();
        }

        public List<Mission> GetDoneMissions(Project project)
        {
            int doneStatusId = missionsStatusDal.GetAllStatus().First(status => status.Name.Equals("Done")).Id;
            return missionDal.GetAllMissions().Where(mission => mission.ProjectId.Equals(project.Code) && 
            mission.StatusId.Equals(doneStatusId)).ToList();
        }

        public List<Mission> GetInProcessMissions(Project project)
        {
            int inProcessStatusId = missionsStatusDal.GetAllStatus().First(status => status.Name.Equals("In Process")).Id;
            return missionDal.GetAllMissions().Where(mission => mission.ProjectId.Equals(project.Code) &&
            mission.StatusId.Equals(inProcessStatusId)).ToList();
        }

        public List<Mission> GetPendingMissions(Project project)
        {
            int pendingStatusId = missionsStatusDal.GetAllStatus().First(status => status.Name.Equals("Pending")).Id;
            return missionDal.GetAllMissions().Where(mission => mission.ProjectId.Equals(project.Code) &&
            mission.StatusId.Equals(pendingStatusId)).ToList();
        }

        public void UpdateMission(Mission m)
        {
            List<MissionsStatus> list = missionsStatusDal.GetAllStatus();
            if (m.Progress == 0)
            {
                m.StatusId = list.First(a => a.Name == "Pending").Id;
            }
            else if (m.Progress == 100)
            {
                m.StatusId = list.First(a => a.Name == "Done").Id;
            }
            else
            {
                m.StatusId = list.First(a => a.Name == "In Process").Id;
            }
            missionDal.UpdateMission(m);
        }
    }
}
