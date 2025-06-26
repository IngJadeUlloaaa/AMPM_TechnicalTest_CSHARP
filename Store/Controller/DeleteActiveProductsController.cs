using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data;
using System.Data.SqlClient;

namespace Store.Controller
{
    public class DeleteActiveProductsController
    {
        public static bool EliminarProducto(string codigo)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();

                // 1. Buscar el ID del producto
                string queryObtenerId = "SELECT Id FROM Productos WHERE Codigo = @Codigo";
                int productoId;

                using (SqlCommand cmd = new SqlCommand(queryObtenerId, conn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                        return false;

                    productoId = Convert.ToInt32(result);
                }

                // 2. Eliminar de ProductosActivos si existe
                string deleteActivos = "DELETE FROM ProductosActivos WHERE Producto_Id = @Id";
                using (SqlCommand cmd = new SqlCommand(deleteActivos, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", productoId);
                    cmd.ExecuteNonQuery();
                }

                // 3. Eliminar de ProductosInactivos si existe
                string deleteInactivos = "DELETE FROM ProductosInactivos WHERE Producto_Id = @Id";
                using (SqlCommand cmd = new SqlCommand(deleteInactivos, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", productoId);
                    cmd.ExecuteNonQuery();
                }

                // 4. Ahora sí, eliminar el producto
                string deleteProducto = "DELETE FROM Productos WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(deleteProducto, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", productoId);
                    int resultado = cmd.ExecuteNonQuery();
                    return resultado > 0;
                }
            }
        }
    }
}
