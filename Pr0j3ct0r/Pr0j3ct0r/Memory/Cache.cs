using Pr0j3ct0r.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.Memory
{
    public class Cache
    {
        private static readonly Cache instance = new Cache();

        private Cache()
        {
        }

        public static Cache Instance
        {
            get
            {
                return instance;
            }
        }

        public string username { get; set; }
        public ProjectVM currentProject { get; set; }
        public bool isManagerOfCurrentProject { get; set; }
    }
}
