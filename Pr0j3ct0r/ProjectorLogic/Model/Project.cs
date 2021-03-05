using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Model
{
    public class Project
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string manager;
        public string Manager
        {
            get { return manager;}
            set { manager = value; }
        }

        private ICollection<string> participants;
        public ICollection<string> Participants
        {
            get { return participants.ToList();/*deep copy*/ }
            private set { participants = value; }
        }

        private ICollection<Mission> missions;
        public ICollection<Mission> Missions
        {
            get { return missions.ToList(); }
            private set { missions = value; }
        }

        private Node<Mission> missionTree;
        public Node<Mission> MissionTree
        {
            get { return missionTree; }
            set { missionTree = value; }
        }

        public void addParticipant(string username)
        {
            this.participants.Add(username);
        }

        public Project()
        {
            participants = new List<string>();
        }

        public Project(int code, DateTime startDate, string description)
        {
            StartDate = startDate;
            Code = code;
            Description = description;
        }

        public Project(DataRow row)
        {
            Code = int.Parse(row["Id"].ToString());
            Name = row["Name"].ToString();
            Description = row["Description"].ToString();
            StartDate = DateTime.Parse(row["Start_Date"].ToString());
            EndDate = row["End_Date"] == DBNull.Value ? new DateTime() : DateTime.Parse(row["End_Date"].ToString());
            Manager = row["Manager_Id"].ToString();
            participants = new List<string>();
        }

        public Project(string name, DateTime startDate, DateTime endDate, int code, string description, string manager, ICollection<string> participants, ICollection<Mission> missions, Node<Mission> missionTree)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Code = code;
            Description = description;
            Manager = manager;
            Participants = participants;
            Missions = missions;
            MissionTree = missionTree;
        }
    }
}
