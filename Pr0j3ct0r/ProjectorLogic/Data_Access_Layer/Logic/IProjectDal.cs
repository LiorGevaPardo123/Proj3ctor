using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Logic
{
    interface IProjectDal
    {
        void CreateNewProject(Project project);
        bool IsProjectExists(Project project);
        void UpdateProject(Project project);
        void SetEndDate(int id, DateTime end);
        void DeleteProject(Project project);
        List<Project> GetAllProjects();
        Project GetProjectEntity(int id);
        List<Project> GetAllProjectsManagedBy(string username);
        List<string> GetProjectParticipants(int code);
        List<int> GetProjectIdOfParticipantList(string username);
        void AddUserToProjectParticipants(string username, int id);
        void RemoveParticipantFromAProject(string username, int id);
    }
}
