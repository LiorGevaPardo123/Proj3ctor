using ProjectorLogic.Data_Access_Layer.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjectorLogic.Data_Access_Layer.Implementaion
{
    public class ProjectDal : DalComponent, IProjectDal
    {
        public void AddUserToProjectParticipants(string username, int id)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Insert into Project_Members values(@username,@id)";
            sql.Parameters.AddWithValue("id", id);
            sql.Parameters.AddWithValue("username", username);
            this.DBCommunication.ExecuteSql(sql);
        }

        public void CreateNewProject(Project project)
        {            
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Insert into ProjectT values(@name,convert(date, @startDate), @endDate, @description, @managerId)";
            sql.Parameters.AddWithValue("description", project.Description);           
            sql.Parameters.AddWithValue("name", project.Name);
            sql.Parameters.AddWithValue("startDate", project.StartDate);
            if (project.EndDate == DateTime.MinValue)
                sql.Parameters.AddWithValue("endDate", DBNull.Value);
            else
                sql.Parameters.AddWithValue("endDate", project.EndDate);
            sql.Parameters.AddWithValue("managerId", project.Manager);
            this.DBCommunication.ExecuteSql(sql);
        }       

        public void DeleteProject(Project project)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Delete from ProjectT WHERE Id = @Id";
            sql.Parameters.AddWithValue("Id", project.Code);
            this.DBCommunication.ExecuteSql(sql);
        }

        public List<Project> GetAllProjects()
        {
            List<Project> list = new List<Project>();
            DataSet d = new DataSet();
            this.DBCommunication.FillDataSet(d, "ProjectT");
            foreach (DataRow row in d.Tables[0].Rows)
            {
                list.Add(new Project()
                {
                    Code = int.Parse(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    StartDate = DateTime.Parse(row["Start_Date"].ToString()),
                    EndDate = row["End_Date"] == DBNull.Value ? new DateTime() : DateTime.Parse(row["End_Date"].ToString()),
                    Manager = row["Manager_Id"].ToString(),
                });
            }
            return list;
        }

        public List<Project> GetAllProjectsManagedBy(string username)
        {
            List<Project> list = new List<Project>();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Select * From ProjectT where Manager_Id = @username";            
            sql.Parameters.AddWithValue("username", username);
            DataSet d = new DataSet();
            DBCommunication.FillDataSet(d, sql);            
            foreach (DataRow row in d.Tables[0].Rows)
            {                
                list.Add(new Project()
                {
                    Code = int.Parse(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    StartDate = DateTime.Parse(row["Start_Date"].ToString()),
                    EndDate = row["End_Date"] == DBNull.Value ? new DateTime() : DateTime.Parse(row["End_Date"].ToString()),
                    Manager = row["Manager_Id"].ToString(),
                });
            }
            return list;
        }

        public Project GetProjectEntity(int id)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "SELECT * FROM ProjectT WHERE Id = @id";
            DataSet d = new DataSet();
            sql.Parameters.AddWithValue("id", id);            
            DBCommunication.FillDataSet(d, sql);
            DataRow row = d.Tables[0].Rows[0];
            return new Project(row);            
        }

        public List<int> GetProjectIdOfParticipantList(string username)
        {
            List<int> list = new List<int>();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Select Project_Id from Project_Members where Username = @username";
            sql.Parameters.AddWithValue("username", username);
            DataSet d = new DataSet();
            DBCommunication.FillDataSet(d, sql);
            foreach (DataRow row in d.Tables[0].Rows)
            {
                list.Add(int.Parse(row["Project_Id"].ToString()));
            }
            return list;
        }

        public List<string> GetProjectParticipants(int code)
        {
            List<string> list = new List<string>();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "select Username From Project_Members where Project_Id = @code";
            sql.Parameters.AddWithValue("code", code);
            DataSet d = new DataSet();
            DBCommunication.FillDataSet(d, sql);
            foreach (DataRow row in d.Tables[0].Rows)
            {
                list.Add(row["Username"].ToString().Trim());
            }
            return list;
        }

        public bool IsProjectExists(Project project)
        {
            throw new NotImplementedException();
        }

        public void RemoveParticipantFromAProject(string username, int id)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Delete From Project_Members where Username = @username and Project_Id = @id";
            sql.Parameters.AddWithValue("id", id);
            sql.Parameters.AddWithValue("username", username);
            this.DBCommunication.ExecuteSql(sql);
        }

        public void SetEndDate(int id, DateTime end)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "UPDATE ProjectT SET End_Date = convert(date, @endDate) where Id = @Id";
            sql.Parameters.AddWithValue("Id", id);
            sql.Parameters.AddWithValue("endDate", end);            
            this.DBCommunication.ExecuteSql(sql);
        }

        public void UpdateProject(Project project)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "UPDATE ProjectT SET Start_Date = convert(date, @startDate), Description = @description where Id = @Id";
            sql.Parameters.AddWithValue("Id", project.Code);
            sql.Parameters.AddWithValue("startDate", project.StartDate);            
            sql.Parameters.AddWithValue("description", project.Description);            
            this.DBCommunication.ExecuteSql(sql);
        }
    }
}
