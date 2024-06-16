using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WindowsFormsApp7
{
    public class DataBasecs
    {
        NpgsqlConnection Connection = new NpgsqlConnection("Server = localhost; port = 5432; DataBase = zooShop; User Id = postgres; Password = 123");

        public void OpenConnection()
        {
           if( Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
          
        }
        
        public void CloseConnection()
        {
            if(Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return Connection;
        }
    }
}
