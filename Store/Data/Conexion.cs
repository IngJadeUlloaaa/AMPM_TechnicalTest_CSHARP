using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Store.Data
{
    public class Conexion // public class to share the connection
    {
        private static readonly string connectionString = "Server=localhost;Database=AMPMTest;Trusted_Connection=True;"; // establish connection

        public static SqlConnection ObtenerConexion() // create the method
        {
            return new SqlConnection(connectionString); // return connection
        }
    }
}
