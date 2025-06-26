using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data;
using Store.Model;
using System.Data.SqlClient;

namespace Store.Controller
{
    public class RegisterController
    {
        public static bool RegistrarUsuarioBasico(RegisterModel nuevoUsuario)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"
                    INSERT INTO Usuarios (Nombre, Correo, Telefono)
                    VALUES (@nombre, @correo, @telefono)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nuevoUsuario.Nombre);
                    cmd.Parameters.AddWithValue("@correo", nuevoUsuario.Correo);
                    cmd.Parameters.AddWithValue("@telefono", nuevoUsuario.Telefono);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }
    }
}
