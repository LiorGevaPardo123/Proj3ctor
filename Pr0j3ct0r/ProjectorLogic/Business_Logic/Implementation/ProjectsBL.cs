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
    public class ProjectsBL : IProjectsBL
    {
        private IProjectDal projectDal;
        private PlayersBL playersBL;
        public ProjectsBL()
        {
            projectDal = new ProjectDal();
            playersBL = new PlayersBL();
        }

        public void AddUserToProjectParticipants(string username, int id)
        {
            if (projectDal.GetProjectEntity(id).Manager != username)
            {
                projectDal.AddUserToProjectParticipants(username, id);
            }
            else
            {
                throw new Exception("the user cant be added as a participant because the user is a manager");
            }
        }

        public void CreateNewProject(Project p)
        {
            projectDal.CreateNewProject(p);
        }

        public void DeleteProject(Project project)
        {
            projectDal.DeleteProject(project);
        }

        public List<Project> GetPersonalManagerProjects(string username, bool loadParticipants = false)
        {
            List<Project> userProjects = projectDal.GetAllProjectsManagedBy(username);
            if (!loadParticipants)
            {
                foreach (Project userProject in userProjects)
                {
                    List<string> participants = projectDal.GetProjectParticipants(userProject.Code);
                    foreach (var participant in participants)
                    {
                        userProject.addParticipant(participant);
                    }
                }
            }
            return userProjects;
        }

        public List<Project> GetPersonalParticipantProjects(string username)
        {
            List<int> projectsId = projectDal.GetProjectIdOfParticipantList(username);
            List<Project> projectsList = new List<Project>();
            foreach (int id in projectsId)
            {
                projectsList.Add(projectDal.GetProjectEntity(id));
            }
            return projectsList;
        }

        public List<PlayerEntity> GetProjectParticipantsEntity(int code)
        {
            List<PlayerEntity> result = new List<PlayerEntity>();
            foreach (string item in projectDal.GetProjectParticipants(code))
            {
                result.Add(playersBL.GetPlayerByUsername(item));
            }
            return result;
        }

        public void RemoveParticipantFromAProject(string username, int id)
        {
            if (GetProjectParticipantsEntity(id).FindIndex(p=>p.Username.Equals(username)) != -1)
            {
                projectDal.RemoveParticipantFromAProject(username, id);
            }
            else
            {
                throw new Exception("the user cant be removed from the project participants list because the user doesnt exist in the list");
            }
        }

        public void UpdateProject(Project project)
        {
            projectDal.UpdateProject(project);
        }
    }
}
