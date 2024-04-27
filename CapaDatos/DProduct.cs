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
                        productos.Add(new Product { productid = productid, name = name, price = price, stock = stock, active = active });
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

    }
}
