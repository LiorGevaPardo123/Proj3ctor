using Pr0j3ct0r.Memory;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pr0j3ct0r.View_Model
{
    public class ProjectVM
    {       
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private PlayerVM manager;
        public PlayerVM Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        private ObservableCollection<PlayerVM> participants;
        public ObservableCollection<PlayerVM> Participants
        {
            get { return participants; }
            set { participants = value;}
        }

        private string startDate;
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private string  endDate;
        public string  EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        
        public string EndDateStr {
            get
            {
                if (endDate == "01/01/0001")
                {
                    return "";
                }
                else
                {
                    return endDate;
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private bool isFinish;
        public bool IsFinish
        {
            get { return isFinish; }
            set { isFinish = value; }
        }
        public ProjectVM()
        {            
            Participants = new ObservableCollection<PlayerVM>();                           
           
        }
    }
}