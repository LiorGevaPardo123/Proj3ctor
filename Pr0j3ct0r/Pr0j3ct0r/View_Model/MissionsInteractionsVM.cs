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
    public class MissionsInteractionsVM : INotifyPropertyChanged
    {
        private MissionVM selectedMission;
        public MissionVM SelectedMission
        {
            get { return selectedMission; }
            set {
                selectedMission = value;
                OnPropertyRaised("SelectedMission");                
                NextMissionsList.Clear();
                missionsInteractionsBL.GetNextMissions(
                    new Mission(selectedMission.Name, selectedMission.Duration,
                                selectedMission.Description, selectedMission.ProjectId,
                                selectedMission.Status, selectedMission.Code, selectedMission.Progress)
                    ).Select(mission => new MissionVM()
                    {
                        Name = mission.Name,
                        Duration = mission.Duration,
                        Description = mission.Description,
                        Status = mission.StatusId,
                        ProjectId = mission.ProjectId,
                        Code = mission.Id,
                        Progress = mission.Progress
                    }).ToList().ForEach(missionVM => NextMissionsList.Add(missionVM));

                PreviousMissionsList.Clear();
                missionsInteractionsBL.GetPreviousMissions(
                    new Mission(selectedMission.Name, selectedMission.Duration,
                                selectedMission.Description, selectedMission.ProjectId,
                                selectedMission.Status, selectedMission.Code, selectedMission.Progress)
                    ).Select(mission => new MissionVM()
                    {
                        Name = mission.Name,
                        Duration = mission.Duration,
                        Description = mission.Description,
                        Status = mission.StatusId,
                        ProjectId = mission.ProjectId,
                        Code = mission.Id,
                        Progress = mission.Progress
                    }).ToList().ForEach(missionVM => PreviousMissionsList.Add(missionVM));
            }
        }

        private ObservableCollection<MissionVM> missionsList;
        public ObservableCollection<MissionVM> MissionsList
        {
            get { return missionsList; }
            set { missionsList = value; OnPropertyRaised("MissionsList"); }
        }

        private ObservableCollection<MissionVM> nextMissionsList;
        public ObservableCollection<MissionVM> NextMissionsList
        {
            get { return nextMissionsList; }
            set { nextMissionsList = value; OnPropertyRaised("NextMissionsList"); }
        }

        private ObservableCollection<MissionVM> previousMissionsList;
        public ObservableCollection<MissionVM> PreviousMissionsList
        {
            get { return previousMissionsList; }
            set { previousMissionsList = value; OnPropertyRaised("PreviousMissionsList"); }
        }

        private IMissionsBL missionsBL;
        private IMissionsInteractionsBL missionsInteractionsBL;
        private ProjectVM curProject;
        public MissionsInteractionsVM(ProjectVM curProject)
        {
            this.curProject = curProject;
            missionsBL = new MissionsBL();
            missionsInteractionsBL = new MissionsInteractionsBL();
            NextMissionsList = new ObservableCollection<MissionVM>();
            PreviousMissionsList = new ObservableCollection<MissionVM>();
            MissionsList = new ObservableCollection<MissionVM>();
            ObservableCollection<MissionVM> tempMissionList = new ObservableCollection<MissionVM>();
            foreach (Mission mission in missionsBL.GetAllMissions(switchProjectVMToProject(curProject)))
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
                MissionsList.Add(m);               
            }
            foreach (var cur in MissionsList)
            {
                if (IsThereAnInteraction(cur))
                {
                    tempMissionList.Add(cur);
                }
            }
            MissionsList.Clear();
            foreach (var mission in tempMissionList)
            {
                MissionsList.Add(mission);
            }
        }
        private bool IsThereAnInteraction(MissionVM m1)
        {
            Mission m = new Mission();
            m.Id = m1.Code;
            return !(missionsInteractionsBL.GetNextMissions(m).Count==0 && missionsInteractionsBL.GetPreviousMissions(m).Count == 0);
        }

        public Project switchProjectVMToProject(ProjectVM vm)
        {
            Project result = new Project();
            result.Code = vm.Code;
            result.Description = vm.Description;
            result.Name = vm.Name;
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
