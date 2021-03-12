using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Logic
{
    public interface ICriticalPath
    {
        int CalcLongestPath(int projectId);
    }
}
