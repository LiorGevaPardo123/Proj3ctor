using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProjectorLogic.Model
{
    public class Mission
    {  
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int duration;
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int projectId;
        public int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }

        private int statusId;
        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int progress;
        public int Progress
        {
            get { return progress; }
            set { progress = value; }
        }


        public Mission()
        {

        }

        public Mission(string name, int duration, string description, int projectId, int statusId, int id)
        {
            Name = name;
            Duration = duration;
            Description = description;
            ProjectId = projectId;
            StatusId = statusId;
            Id = id;
            Progress = 0;
        }

        public Mission(string name, int duration, string description, int projectId, int statusId, int id, int progress)
        {
            Name = name;
            Duration = duration;
            Description = description;
            ProjectId = projectId;
            StatusId = statusId;
            Id = id;
            Progress = progress;
        }
    }
}