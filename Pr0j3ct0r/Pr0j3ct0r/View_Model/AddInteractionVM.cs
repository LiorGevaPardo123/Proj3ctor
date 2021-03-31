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
    public class AddInteractionVM : INotifyPropertyChanged
    {
        IMissionsInteractionsBL missionsInteractionsBL;
        IMissionsBL missionsBL;
        Mission m;
        Mission mNext;

        private ObservableCollection<MissionVM> missionsList;
        public ObservableCollection<MissionVM> MissionsList
        {
            get { return missionsList; }
            set { missionsList = value; OnPropertyRaised("MissionsList"); }
        }
        
        private MissionVM curruntMission;
        public MissionVM CurruntMission
        {
            get { return curruntMission; }
            set
            {
                m = new Mission();
                mNext = new Mission();
                curruntMission = value; OnPropertyRaised("CurruntMission");
                NextMissionsList.Clear();
                m.Id = curruntMission.Code;
                foreach (var mission in MissionsList)
                {
                    mNext.Id = mission.Code;
                    if (mission.Code != CurruntMission.Code && !IsThereAnInteraction(CurruntMission,mission))
                    {
                        NextMissionsList.Add(mission);
                    }                    
                }                
            }
        }              

        private MissionVM nextMission;
        public MissionVM NextMission
        {
            get { return nextMission; }
            set { nextMission = value; OnPropertyRaised("NextMission"); }
        }

        private ObservableCollection<MissionVM> nextMissionsList;
        public ObservableCollection<MissionVM> NextMissionsList
        {
            get { return nextMissionsList; }
            set { nextMissionsList = value; OnPropertyRaised("NextMissionsList"); }
        }

        private ObservableCollection<MissionVM> curruntMissionsList;
        public ObservableCollection<MissionVM> CurruntMissionsList
        {
            get { return curruntMissionsList; }
            set { curruntMissionsList = value; OnPropertyRaised("CurruntMissionsList"); }
        }

        public AddInteractionVM()
        {
            NextMissionsList = new ObservableCollection<MissionVM>();
            CurruntMissionsList = new ObservableCollection<MissionVM>();
            MissionsList = new ObservableCollection<MissionVM>();
            missionsInteractionsBL = new MissionsInteractionsBL();
            missionsBL = new MissionsBL();

            Project temp = new Project();
            temp.Code = Cache.Instance.currentProject.Code;
            MissionVM missionVM;
            MissionVM m = new MissionVM();
            foreach (var mission in missionsBL.GetAllMissions(temp))
            {                
                missionVM=new MissionVM() { Code = mission.Id,
                    Description = mission.Description,
                    Name = mission.Name
                };                
                MissionsList.Add(missionVM);
                CurruntMissionsList.Add(missionVM);
                NextMissionsList.Add(missionVM);
            } 
        }       

        private bool IsThereAnInteraction(MissionVM m1, MissionVM m2)
        {
            Mission m = new Mission();
            m.Id = m1.Code;
            foreach (var mission in missionsInteractionsBL.GetNextMissions(m))
            {
                if (mission.Id == m2.Code)
                {
                    return true;
                }
            }
            foreach (var mission2 in missionsInteractionsBL.GetPreviousMissions(m))
            {
                if (mission2.Id == m2.Code)
                {
                    return true;
                }
            }
            return false;
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
