using ProjectorLogic.Data_Access_Layer.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Implementaion
{
    public class MissionsInteractionsDal : DalComponent, IMissionsInteractionsDal
    {
        public void CreateNewInteraction(int Id1, int Id2)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "INSERT INTO Missions_Interactions (Previous_Mission_Id, Next_Mission_Id) VALUES(@Id1,@Id2)";
            sql.Parameters.AddWithValue("Id1", Id1);
            sql.Parameters.AddWithValue("Id2",Id2);            
            this.DBCommunication.ExecuteSql(sql);
        }

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
