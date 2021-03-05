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
    public class MissionsInteractionsDal : DalComponent, IMissionsInteractionsDal
    {
        public List<MissionInteractionEntity> GetAllMissionsInteractions()
        {
            List<MissionInteractionEntity> list = new List<MissionInteractionEntity>();
            DataSet d = new DataSet();
            this.DBCommunication.FillDataSet(d, "Missions_Interactions");
            foreach (DataRow row in d.Tables[0].Rows)
            {
                list.Add(new MissionInteractionEntity()
                {                    
                    NextId = int.Parse(row["Next_Mission_Id"].ToString().Trim()),
                    PreviousId = int.Parse(row["Previous_Mission_Id"].ToString().Trim())
                });
            }
            return list;
        }
    }
}
