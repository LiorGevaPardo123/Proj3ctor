using ProjectorLogic.Data_Access_Layer.Communication;
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
    public class PlayerDal : DalComponent, IPlayerDal
    {
        public bool AccessPermission(string username, string pass)
        {            
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Select Username From Player where Username = @name and Password = HASHBYTES('SHA2_512', '"+pass+"')";
            DataSet d = new DataSet();
            sql.Parameters.AddWithValue("name", username);  
            //sql.Parameters.AddWithValue("pass", pass);           
            this.DBCommunication.FillDataSet(d, sql);
            int count = d.Tables[0].Rows.Count;
            if (count == 0)
            {
                return false;
            }
            if (!d.Tables[0].Rows[0]["Username"].ToString().Trim().Equals(username))// to make it case sensitive (Prog1, prog1)
            {
                return false;
            }
            return true;            
        }

        public void DeletePlayer(PlayerEntity player)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Delete from Player WHERE Username= @username";
            sql.Parameters.AddWithValue("username", player.Username);
            this.DBCommunication.ExecuteSql(sql);
        }
        public List<PlayerEntity> GetAllPlayers()
        {
            List<PlayerEntity> list = new List<PlayerEntity>();
            DataSet d = new DataSet();
            this.DBCommunication.FillDataSet(d, "Player");
            foreach (DataRow row in d.Tables[0].Rows)
            {
                list.Add(new PlayerEntity()
                {
                    Username = row["Username"].ToString().Trim(),
                    FirstName = row["First_Name"].ToString().Trim(),
                    LastName = row["Last_Name"].ToString().Trim()
                });
            }
            return list;
        }
        public PlayerEntity GetPlayerEntity(string username)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "SELECT * FROM Player WHERE UserName = @name";
            DataSet d = new DataSet();
            sql.Parameters.AddWithValue("name", username);            
            this.DBCommunication.FillDataSet(d, sql);
            DataRow row = d.Tables[0].Rows[0];
            return new PlayerEntity()
            {
                Username = row["UserName"].ToString().Trim(),
                FirstName = row["First_Name"].ToString().Trim(),
                LastName = row["Last_Name"].ToString().Trim()
            };
        }
        public void InsertNewPlayer(PlayerEntity player)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Insert into Player values (@username, @fname, @lname, HASHBYTES('SHA2_512', '"+player.Password+"'))";
            sql.Parameters.AddWithValue("username", player.Username);
            sql.Parameters.AddWithValue("fname", player.FirstName);
            sql.Parameters.AddWithValue("lname", player.LastName);            
            this.DBCommunication.ExecuteSql(sql);
        }
        public bool IsPlayerExists(PlayerEntity player)
        {
            throw new NotImplementedException();
        }
        public void UpdatePlayer(PlayerEntity player)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "UPDATE Player SET Password = HASHBYTES('SHA2_512', @pass) WHERE Username = @username";
            sql.Parameters.AddWithValue("username", player.Username);                    
            sql.Parameters.AddWithValue("pass", player.Password);
            this.DBCommunication.ExecuteSql(sql);
        }
    }
}
