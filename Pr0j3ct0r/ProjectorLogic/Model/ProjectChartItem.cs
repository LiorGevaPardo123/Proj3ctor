using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Model
{
    public class ProjectChartItem
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int desirable;
        public int Desirable
        {
            get { return desirable; }
            set { desirable = value; }
        }

        private int common;
        public int Common
        {
            get { return common; }
            set { common = value; }
        }
    }
}
