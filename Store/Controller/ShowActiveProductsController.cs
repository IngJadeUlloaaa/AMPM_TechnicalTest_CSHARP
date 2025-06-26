using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data;
using Store.Model;
using System.Data.SqlClient;
using System.Data;

namespace Store.Controller
{
    public class ShowActiveProductsController
    {
        public static List<ShowProductsActiveModels> ObtenerProductosActivos()
        {
            List<ShowProductsActiveModels> lista = new List<ShowProductsActiveModels>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"
                    SELECT
                        p.Codigo AS Codigo,
                        p.Nombre AS Nombre,
                        p.Existencias AS Existencias,
                        pa.Opcion AS Opcion
                    FROM ProductosActivos pa
                    INNER JOIN Productos p ON pa.Producto_Id = p.Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShowProductsActiveModels producto = new ShowProductsActiveModels
                        {
                            Codigo = reader["Codigo"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Existencias = reader["Existencias"].ToString(),
                            Opcion = reader["Opcion"].ToString()
                        };
                        lista.Add(producto);
                    }
                }
            }

            return lista;
        }

        // search method
        public static DataTable SearchActProducts(string termino)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                string query = @"
                    SELECT 
                        p.Codigo AS Codigo, 
                        p.Nombre AS Nombre, 
                        p.Existencias AS Existencias,
                        pa.Opcion AS Opcion
                    FROM ProductosActivos pa
                    INNER JOIN Productos p ON pa.Producto_Id = p.Id
                    WHERE p.Nombre LIKE @term OR p.Codigo LIKE @term";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@term", $"%{termino}%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
