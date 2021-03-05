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
    class ProjectParticipantsVM : INotifyPropertyChanged
    {
        private ProjectVM projectVM;
        public ProjectVM ProjectVM
        {
            get { return projectVM; }
            set { projectVM = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        private IProjectsBL projectsBL;
        public ProjectParticipantsVM(ProjectVM pvm)
        {
            projectsBL = new ProjectsBL();
            this.ProjectVM = pvm;           
            initParticipantsView();
        }
        public void AddParticipants(List<string> usernames)
        {
            foreach (string username in usernames)
            {
                projectsBL.AddUserToProjectParticipants(username, this.projectVM.Code);
            }
            initParticipantsView();
        }
        public void RemoveParticipant(string username)
        {
            projectsBL.RemoveParticipantFromAProject(username, this.projectVM.Code);
            initParticipantsView();
        }
        public void initParticipantsView()
        {
            Project p = new Project();
            p.Code = this.projectVM.Code;
            projectVM.Participants.Clear();
            List<PlayerEntity> l = projectsBL.GetProjectParticipantsEntity(p.Code);
            foreach (PlayerEntity item in l)
            {
                PlayerVM player = new PlayerVM()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    UserName = item.Username,

                };
                projectVM.Participants.Add(player);
            }
        }
    }
}
