 using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Logic
{
    public interface IProjectsBL
    {
        void DeleteProject(Project project);
        List<Project> GetPersonalManagerProjects(string username, bool loadParticipants = false);
        List<Project> GetPersonalParticipantProjects(string username);
        void CreateNewProject(Project project);
        List<PlayerEntity> GetProjectParticipantsEntity(int code);
        void UpdateProject(Project project);
        void AddUserToProjectParticipants(string username, int id);
        void RemoveParticipantFromAProject(string username, int id);
    }
}
