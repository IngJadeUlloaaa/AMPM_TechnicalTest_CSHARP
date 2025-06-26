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
    public class UpdateActiveProductsController
    {
        public static bool ActualizarProducto(UpdateActiveProductsModel producto)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"
                    UPDATE p
                    SET 
                        p.Nombre = @Nombre,
                        p.Existencias = @Existencias
                    FROM Productos p
                    INNER JOIN ProductosActivos pa ON pa.Producto_Id = p.Id
                    WHERE p.Codigo = @Codigo AND pa.Opcion = @Opcion";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Existencias", producto.Existencias);
                    cmd.Parameters.AddWithValue("@Opcion", producto.Opcion);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}
