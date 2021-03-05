using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Communication
{
    class DBCommunication : IDBCommunication
    {
        private string conStr;
        public DBCommunication()
        {
            conStr = GetConnectionString();
        }
        public bool ExecuteSql(SqlCommand sql)
        {
            //מקשר
            SqlConnection connection = new SqlConnection();
            //הצבת מחרוזת הקישור במקשר - שימוש בפעולת עזר למציאת מחרוזת זאת
            connection.ConnectionString = conStr;
            //ההוראה
            SqlCommand command = sql;
            command.Connection = connection;           

            //בגלל שיש גישה לקבצים חיצוניים וכן בגלל פתיחת קשר לקובץ חיצוני - "עוטפים" במנגנון טיפול בשגיאות"
            try
            {
                //פתיחת הקשר
                connection.Open();

                //הפעלת הפקודה
                command.ExecuteNonQuery();

                //סגירת הקשר
                connection.Close();

                return true;
            }
            catch (Exception e)
            {
                //משמש רק לצרכי בקרה במקרה של תקלה - חשוב להשאיר כאן נקודת עצירה
                e.ToString();
            }

            return false;
        }
        public void FillDataSet(DataSet dataSet, string tableName, string orderBy = "")
        {           
            SqlConnection connection = new SqlConnection();
            //הצבת מחרוזת הקישור במקשר
            connection.ConnectionString = conStr;

            //מבצע פעולה
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            if (orderBy != "")
                command.CommandText = "SELECT * FROM " + tableName + " ORDER BY " + orderBy;
            else
                command.CommandText = "SELECT * FROM " + tableName;

            //מתאם
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, tableName);
        }
        public void FillDataSet(DataSet dataSet, SqlCommand sql)
        {
            SqlConnection connection = new SqlConnection();
            //הצבת מחרוזת הקישור במקשר
            connection.ConnectionString = conStr;

            //מבצע פעולה
            SqlCommand command = sql;
            command.Connection = connection;
            
          
            //מתאם
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataSet);
        }
        public string GetConnectionString()
        {            
            SqlConnectionStringBuilder cString = new SqlConnectionStringBuilder();

            cString.DataSource = @"(LocalDB)\MSSQLLocalDB";
            cString.AttachDBFilename = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName 
                + @"\ProjectorLogic\Proj3ctorDB.mdf";


            return cString.ToString();
            
            //return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user1\source\repos\ProjectorLogic\ProjectorLogic\Proj3ctorDB.mdf;Integrated Security=True";
        }
    }
}
