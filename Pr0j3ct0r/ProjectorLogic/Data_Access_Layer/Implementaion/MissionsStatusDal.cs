using ProjectorLogic.Data_Access_Layer.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Implementaion
{
    class MissionsStatusDal : DalComponent, IMissionsStatusDal
    {
        public List<MissionsStatus> GetAllStatus()
        {
            List<MissionsStatus> list = new List<MissionsStatus>();
            DataSet d = new DataSet();
            this.DBCommunication.FillDataSet(d, "Mission_Status");
            foreach (DataRow row in d.Tables[0].Rows)
            {
                list.Add(new MissionsStatus()
                {
                    Id = int.Parse(row["Id"].ToString()),
                    Name = row["Title"].ToString().Trim()
                });
            }
            return list;
        }
    }
}
