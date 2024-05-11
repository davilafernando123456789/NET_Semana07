using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DProduct
    {
        private string connectionString = "Data Source=DAVILA-FERNANDO\\SQLEXPRESS;Initial Catalog=FacturaDB;User Id=Davila;Password=Davila12";

        //public List<Product> getProducts()
        //{

        //    List<Product> productos = new List<Product>();


        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            SqlCommand command = new SqlCommand("USP_LISTPRODUCTS", connection);
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                int productid = reader.GetInt32(reader.GetOrdinal("product_id"));
        //                string name = reader.GetString(reader.GetOrdinal("name"));
        //                decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
        //                int stock = reader.GetInt32(reader.GetOrdinal("stock"));
        //                bool active = reader.GetBoolean(reader.GetOrdinal("active"));
        //                productos.Add(new Product { productid = productid, name = name, price = price, stock = stock, active = active });
        //            }

        //            connection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones
        //        Console.WriteLine("Error al obtener productos: " + ex.Message);
        //    }

        //    return productos;
        //}
        public List<Product> getProducts()
        {
            List<Product> productos = new List<Product>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_LISTPRODUCTS", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int productid = reader.GetInt32(reader.GetOrdinal("product_id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                        int stock = reader.GetInt32(reader.GetOrdinal("stock"));
                        bool active = reader.GetBoolean(reader.GetOrdinal("active"));

                        // Filtrar solo los productos activos
                        if (active)
                        {
                            productos.Add(new Product { productid = productid, name = name, price = price, stock = stock, active = active });
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("Error al obtener productos: " + ex.Message);
            }

            return productos;
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_UPDATEPRODUCTO", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@product_id", product.productid);
                    command.Parameters.AddWithValue("@name", product.name);
                    command.Parameters.AddWithValue("@price", product.price);
                    command.Parameters.AddWithValue("@stock", product.stock);
                    command.Parameters.AddWithValue("@active", product.active);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0; // Devuelve true si se actualizaron filas
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar producto: " + ex.ToString());
                return false;
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_DELETEPRODUCTO", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@product_id", productId);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0; // Devuelve true si se eliminaron filas
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar producto: " + ex.ToString());
                return false;
            }
        }

    }
}
