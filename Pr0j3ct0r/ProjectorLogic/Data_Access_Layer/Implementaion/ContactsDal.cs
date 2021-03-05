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
    class ContactsDal : DalComponent, IContactsDal
    {
        public void DeleteContactForUser(string username1, string username2)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Delete FROM Contacts WHERE Username1= @username1 AND Username2 = @username2";
            sql.Parameters.AddWithValue("username1", username1);
            sql.Parameters.AddWithValue("username2", username2);
            this.DBCommunication.ExecuteSql(sql);
        }

        public List<ContactInfo> GetAllContactsOfUsername(string username)
        {
            List<ContactInfo> list = new List<ContactInfo>();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "SELECT Username2, Type, Value FROM Contacts Left JOIN Contact_info on Contacts.Username2 = Contact_info.Username WHERE Username1 = @u1";
            DataSet d = new DataSet();
            sql.Parameters.AddWithValue("u1", username);
            this.DBCommunication.FillDataSet(d, sql);
            foreach (DataRow row in d.Tables[0].Rows)
            {
                list.Add(new ContactInfo()
                {
                    Username = row["Username2"].ToString().Trim(),                    
                    Type = row["Type"] == DBNull.Value ? string.Empty: row["Type"].ToString().Trim(),
                    Val = row["Value"] == DBNull.Value ? string.Empty: row["Value"].ToString().Trim()
                });
            }
            return list;
        }

        public List<ContactInfo> GetPlayerInfo(string username)
        {
            List<ContactInfo> infoList = new List<ContactInfo>();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "SELECT Type, Value FROM Contact_info where Username = @username";
            DataSet d = new DataSet();
            sql.Parameters.AddWithValue("username", username);
            this.DBCommunication.FillDataSet(d, sql);
            foreach (DataRow row in d.Tables[0].Rows)
            {
                infoList.Add(new ContactInfo()
                {       
                    Type = row["Type"] == DBNull.Value ? string.Empty : row["Type"].ToString().Trim(),
                    Val = row["Value"] == DBNull.Value ? string.Empty : row["Value"].ToString().Trim()
                });
            }
            return infoList;
        }

        public void InsertNewContact(PlayerEntity player, PlayerEntity player2)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Insert into Contacts values (@username1, @username2)";
            sql.Parameters.AddWithValue("username1", player.Username);
            sql.Parameters.AddWithValue("username2", player2.Username);
            this.DBCommunication.ExecuteSql(sql);
        }

        public void InsertPlayerInfo(string username, string type, string value)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Insert into Contact_info values(@username, @type, @value)";
            DataSet d = new DataSet();
            sql.Parameters.AddWithValue("username", username);
            sql.Parameters.AddWithValue("type", type);
            sql.Parameters.AddWithValue("value", value);
            this.DBCommunication.FillDataSet(d, sql);
        }

        public void UpdatePlayerInfo(string username, string type, string value)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "Update Contact_info set Value = @value where Type = @type and Username = @username";
            DataSet d = new DataSet();
            sql.Parameters.AddWithValue("username", username);
            sql.Parameters.AddWithValue("type", type);
            sql.Parameters.AddWithValue("value", value);
            this.DBCommunication.FillDataSet(d, sql);
        }
    }
}
