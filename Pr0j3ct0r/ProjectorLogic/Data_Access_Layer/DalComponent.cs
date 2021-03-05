using ProjectorLogic.Data_Access_Layer.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer
{
    public abstract class DalComponent
    {
        private IDBCommunication dbCommunication;
        public IDBCommunication DBCommunication
        {
            get { return dbCommunication; }
            set { dbCommunication = value; }
        }

        public DalComponent()
        {
            DBCommunication = new DBCommunication();
        }
    }
}
