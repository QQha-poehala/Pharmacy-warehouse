using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace database
{
    // Классс для работы с базой данных.
    public class DataB
    {
        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Apteka1.mdb");
        // Открытие соединения.
        public void openConnection()
        {
            myConnection.Open();
        }
        // Закрытие соединения.
        public void closeConnection()
        {
            if (myConnection != null)
            {
                myConnection.Close();
            }
        }
        // Получение соединения.
        public OleDbConnection getConnection()
        {
            return myConnection;
        }
    }
}
