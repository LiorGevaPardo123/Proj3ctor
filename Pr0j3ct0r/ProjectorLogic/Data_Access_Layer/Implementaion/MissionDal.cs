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
    public class MissionDal : DalComponent, IMissionDal
    {
        public void CreateNewMission(Mission m)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "INSERT INTO Mission (Name, Duration, Description, Status, Project_Id) VALUES(@name, @duration, @description, @title, @id)";            

            sql.Parameters.AddWithValue("name", m.Name);
            sql.Parameters.AddWithValue("duration", m.Duration);
            sql.Parameters.AddWithValue("description", m.Description);
            sql.Parameters.AddWithValue("title", m.StatusId);
            sql.Parameters.AddWithValue("id", m.ProjectId);
            this.DBCommunication.ExecuteSql(sql);
        }

        public void DeleteMission(Mission m)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Delete from Mission WHERE Id = @Id";
            sql.Parameters.AddWithValue("Id", m.Id);
            this.DBCommunication.ExecuteSql(sql);
        }

        public List<Mission> GetAllMissions()
        {
            List<Mission> list = new List<Mission>();
            DataSet d = new DataSet();
            this.DBCommunication.FillDataSet(d, "Mission");
            foreach (DataRow row in d.Tables[0].Rows)
            {
                list.Add(new Mission() {
                    Id = int.Parse(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    Duration = int.Parse(row["Duration"].ToString()),
                    StatusId = int.Parse(row["Status"].ToString()),
                    ProjectId = int.Parse(row["Project_Id"].ToString()),
                    Progress = int.Parse(row["Progress"].ToString().Trim())
                });
            }
            return list;
        }      
          
        public Mission GetMission(string name, int pId)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "SELECT * FROM Mission WHERE Name = @name and Project_Id = @pId";
            DataSet d = new DataSet();
            sql.Parameters.AddWithValue("name", name);
            sql.Parameters.AddWithValue("pId", pId);
            this.DBCommunication.FillDataSet(d, sql);
            DataRow row = d.Tables[0].Rows[0];
            return new Mission()
            {
                Id = int.Parse(row["Id"].ToString()),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Duration = int.Parse(row["Duration"].ToString()),
                StatusId = int.Parse(row["Status"].ToString()),
                ProjectId = int.Parse(row["Project_Id"].ToString()),
                Progress = int.Parse(row["Progress"].ToString())
            };
        }       

        public bool IsMissionExists(Mission m)
        {
            throw new NotImplementedException();
        }

        public void UpdateMission(Mission m)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "UPDATE Mission SET Name = @name, Duration = @duration, Description = @des, Status = @status, Progress = @progress WHERE ID = @id";
            sql.Parameters.AddWithValue("name", m.Name);
            sql.Parameters.AddWithValue("duration", m.Duration);
            sql.Parameters.AddWithValue("des", m.Description);
            sql.Parameters.AddWithValue("status", m.StatusId);
            sql.Parameters.AddWithValue("id", m.Id);
            sql.Parameters.AddWithValue("progress", m.Progress);
            this.DBCommunication.ExecuteSql(sql);
        }       
    }
}
