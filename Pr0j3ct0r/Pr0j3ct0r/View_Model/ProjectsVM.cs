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
    public class ProjectsVM : INotifyPropertyChanged
    {
        private IProjectsBL projectsBL;

        private ObservableCollection<ProjectVM> personalParticipantProjectsList;
        public ObservableCollection<ProjectVM> PersonalParticipantProjectsList
        {
            get { return personalParticipantProjectsList; }
            set { personalParticipantProjectsList = value; OnPropertyRaised("PersonalParticipantProjectsList"); }
        }

        private ObservableCollection<ProjectVM> personalManagerProjectsList;
        public ObservableCollection<ProjectVM> PersonalManagerProjectsList
        {
            get { return personalManagerProjectsList; }
            set { personalManagerProjectsList = value; OnPropertyRaised("PersonalManagerProjectsList"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }         
        public ProjectsVM()
        {
            projectsBL = new ProjectsBL();
            personalManagerProjectsList = new ObservableCollection<ProjectVM>();
            personalParticipantProjectsList = new ObservableCollection<ProjectVM>();
            Init();
        }
        public void Init()
        {
            PersonalManagerProjectsList.Clear();
            PersonalParticipantProjectsList.Clear();
            List<Project> managerListFromBL = projectsBL.GetPersonalManagerProjects(Cache.Instance.username, true);
            List<Project> participantListFromBL = projectsBL.GetPersonalParticipantProjects(Cache.Instance.username);
            //List<Project> managerListFromBL = projectsBL.GetPersonalManagerProjects("BotBurg3r", true);
            //List<Project> participantListFromBL = projectsBL.GetPersonalParticipantProjects("BotBurg3r");
            foreach (Project project in managerListFromBL)
            {
                ProjectVM p = new ProjectVM()
                {
                    Code = project.Code,
                    Name = project.Name,
                    StartDate = project.StartDate.ToShortDateString(),
                    EndDate = project.EndDate.ToShortDateString(),
                    Description = project.Description,
                    Manager = new PlayerVM() { UserName = project.Manager },

                };
                foreach (string username in project.Participants)
                {
                    p.Participants.Add(new PlayerVM() { UserName = username });
                }
                PersonalManagerProjectsList.Add(p);
            }

            foreach (Project project1 in participantListFromBL)
            {
                ProjectVM p = new ProjectVM()
                {
                    Code = project1.Code,
                    Name = project1.Name,
                    StartDate = project1.StartDate.ToShortDateString(),
                    EndDate = project1.EndDate.ToShortDateString(),
                    Description = project1.Description,
                    Manager = new PlayerVM() { UserName = project1.Manager }
                };
                PersonalParticipantProjectsList.Add(p);
            }
            OnPropertyRaised("PersonalManagerProjectsList");
            OnPropertyRaised("PersonalParticipantProjectsList");
            OnPropertyRaised("CountManagerProjects");
            OnPropertyRaised("CountParticipantProjects");
        }       
        public int CountParticipantProjects
        {
            get { return personalParticipantProjectsList.Count; }
        }
        public int CountActiveParticipantProjects
        {
            get { return personalParticipantProjectsList.Where(p => p.IsFinish).Count(); }
        }
        public int CountManagerProjects
        {
            get { return personalManagerProjectsList.Count; }            
        }
        public int CountActiveManagerProjects
        {
            get { return personalManagerProjectsList.Where(p => p.IsFinish).Count(); }
        }             
    }
}
