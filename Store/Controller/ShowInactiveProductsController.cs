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
    public class ShowInactiveProductsController
    {
        public static List<ShowInactiveProductsModels> ObtenerProductosInactivos()
        {
            List<ShowInactiveProductsModels> lista = new List<ShowInactiveProductsModels>();

            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"
                   SELECT
	                    p.Codigo AS Codigo,
	                    p.Nombre AS Nombre,
	                    p.Existencias AS Existencias,
	                    pii.Opcion AS Opcion
                    FROM ProductosInactivos pii
                    INNER JOIN Productos p ON pii.Producto_Id = p.Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShowInactiveProductsModels producto = new ShowInactiveProductsModels
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

        // seach method
        public static DataTable SearchIncProducts(string termino)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                string query = @"
                    SELECT 
                        p.Codigo AS Codigo, 
                        p.Nombre AS Nombre, 
                        p.Existencias AS Existencias,
                        pii.Opcion AS Opcion
                    FROM ProductosInactivos pii
                    INNER JOIN Productos p ON pii.Producto_Id = p.Id
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
