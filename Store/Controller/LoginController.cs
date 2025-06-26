using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data;
using Store.Model;
using System.Data;
using System.Data.SqlClient;

namespace Store.Controller
{
    public class LoginController
    {
        public static bool ValidarUsuario(string UserInput, string PasswordInput, out UsersModel usuarioEncontrado)
        {
            usuarioEncontrado = null;

            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"
                    DECLARE @SecretPhrase VARCHAR(100) = 'HalaMadrid';

                    SELECT 
                        Usuario,
                        CONVERT(NVARCHAR(100), DECRYPTBYPASSPHRASE(@SecretPhrase, Contrasena)) AS DecryptedPassword,
                    Nombre,
                    Apellido,
                    Correo,
                    Telefono
                    FROM Usuarios WHERE Usuario = @usuario"
                ;

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", UserInput);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string passwordDB = reader["DecryptedPassword"].ToString();

                            if (passwordDB == PasswordInput)
                            {
                                usuarioEncontrado = new UsersModel
                                {
                                    Usuario = reader["Usuario"].ToString(),
                                    Contrasena = passwordDB,
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Telefono = reader["Telefono"].ToString()
                                };

                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

    }
}
