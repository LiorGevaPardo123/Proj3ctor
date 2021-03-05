using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    public class MissionVM : INotifyPropertyChanged
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

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int projectId;
        public int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private int progress;
        public int Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyRaised("Progress"); }
        }
        public MissionVM()
        {
                
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
