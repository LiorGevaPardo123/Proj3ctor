using Pr0j3ct0r.Memory;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    public class MissionsByStatusVM : INotifyPropertyChanged
    {
        private ObservableCollection<MissionVM> inProcessMissionsList;
        public ObservableCollection<MissionVM> InProcessMissionsList
        {
            get { return inProcessMissionsList; }
            set { inProcessMissionsList = value; OnPropertyRaised("InProcessMissionsList"); }
        }

        private ObservableCollection<MissionVM> pendingMissionsList;
        public ObservableCollection<MissionVM> PendingMissionsList
        {
            get { return pendingMissionsList; }
            set { pendingMissionsList = value; OnPropertyRaised("PendingMissionsList"); }
        }

        private ObservableCollection<MissionVM> doneMissionsList;
        public ObservableCollection<MissionVM> DoneMissionsList
        {
            get { return doneMissionsList; }
            set { doneMissionsList = value; OnPropertyRaised("DoneMissionsList"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private IMissionsBL missionsBL;
        private ProjectVM curProject;
        public MissionsByStatusVM(ProjectVM curProject)
        {
            this.curProject = curProject;
            missionsBL = new MissionsBL();
            inProcessMissionsList = new ObservableCollection<MissionVM>();
            doneMissionsList = new ObservableCollection<MissionVM>();
            pendingMissionsList = new ObservableCollection<MissionVM>();
            Init();
        }
        public void Init()
        {
            InProcessMissionsList.Clear();
            DoneMissionsList.Clear();
            pendingMissionsList.Clear();
            List<Mission> processListFromBL = missionsBL.GetInProcessMissions(switchProjectVMToProject(curProject));
            List<Mission> doneListFromBL = missionsBL.GetDoneMissions(switchProjectVMToProject(curProject));
            List<Mission> pendingListFromBL = missionsBL.GetPendingMissions(switchProjectVMToProject(curProject));
            foreach (Mission mission in processListFromBL)
            {
                MissionVM m = new MissionVM()
                {
                    Code = mission.Id,
                    Duration = mission.Duration,                   
                    Description = mission.Description,
                    Name = mission.Name,
                    ProjectId = mission.ProjectId,
                    Status = mission.StatusId,
                    Progress = mission.Progress
                };               
                InProcessMissionsList.Add(m);
            }

            foreach (Mission mission1 in doneListFromBL)
            {
                MissionVM m = new MissionVM()
                {
                    Code = mission1.Id,
                    Duration = mission1.Duration,
                    Description = mission1.Description,
                    Name = mission1.Name,
                    ProjectId = mission1.ProjectId,
                    Status = mission1.StatusId,
                    Progress = mission1.Progress
                };
                DoneMissionsList.Add(m);
            }

            foreach (Mission mission2 in pendingListFromBL)
            {
                MissionVM m = new MissionVM()
                {
                    Code = mission2.Id,
                    Duration = mission2.Duration,
                    Description = mission2.Description,
                    Name = mission2.Name,
                    ProjectId = mission2.ProjectId,
                    Status = mission2.StatusId,
                    Progress = mission2.Progress
                };
                pendingMissionsList.Add(m);
            }
            OnPropertyRaised("CountInProcessMissions");
            OnPropertyRaised("CountPendingMissions");
            OnPropertyRaised("CountDoneMissions");
        }
        public Project switchProjectVMToProject(ProjectVM vm)
        {
            Project result = new Project();
            result.Code = vm.Code;
            result.Description = vm.Description;
            result.Name = vm.Name;            
            return result;
        }
        public int CountInProcessMissions
        {
            get { return inProcessMissionsList.Count; }
        }
        public int CountPendingMissions
        {
            get { return pendingMissionsList.Count; }
        }
        public int CountDoneMissions
        {
            get { return doneMissionsList.Count; }
        }
    }
}
