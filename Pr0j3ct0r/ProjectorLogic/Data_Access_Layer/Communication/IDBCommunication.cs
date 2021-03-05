using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Communication
{
    public interface IDBCommunication
    {
        bool ExecuteSql(SqlCommand sql);
        string GetConnectionString();
        void FillDataSet(DataSet dataSet, string tableName, string orderBy = "");
        void FillDataSet(DataSet dataSet, SqlCommand sql);
    }
}
