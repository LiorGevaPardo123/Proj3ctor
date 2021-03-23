using Pr0j3ct0r.Memory;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    class CriticalPathVM
    {
        IProjectsBL projectsBL;
        ICriticalPath criticalPath;       
       
        public List<ProjectChartItem> Data;
        public CriticalPathVM(List<int> projectsId)
        {
            Project p;            
            criticalPath = new CriticalPath();
            Data = new List<ProjectChartItem>();
            projectsBL = new ProjectsBL();
            foreach (var item in projectsId)
            {
                p = projectsBL.GetProjectEntityById(item);                
                if (p.EndDate != DateTime.MinValue)
                {                    
                    Data.Add(new ProjectChartItem()
                    {
                        Name = p.Name,
                        Common = Duration(p.StartDate, p.EndDate),
                        Desirable = criticalPath.CalcLongestPath(item)
                });
                }                
            }
        }
            

        private int Duration(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days;
        }         
    }
}
