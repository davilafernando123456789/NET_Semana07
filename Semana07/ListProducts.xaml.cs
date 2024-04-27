using CapaNegocio;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Semana07
{
    //    /// <summary>
    //    /// Lógica de interacción para ListProducts.xaml
    //    /// </summary>
    //    public partial class ListProducts : Window
    //    {
    //        public ListProducts()
    //        {
    //            InitializeComponent();
    //            CargarDatos();
    //        }

    //        private void CargarDatos()
    //        {
    //            CapaDatos.DProduct data = new CapaDatos.DProduct(); 
    //            List<Product> getProducts = data.getProducts(); 
    //            dataGridEmpleados.ItemsSource = getProducts;
    //        }
    //        //private void CargarDatosByName()
    //        //{

    //        //}
    //    }
    //}

    public partial class ListProducts : Window
    {
        private ProductBusiness productBusiness;

        public ListProducts()
        {
            InitializeComponent();
            productBusiness = new ProductBusiness();
            CargarDatos();
        }

        private void CargarDatos()
        {
            List<Product> getProducts = productBusiness.GetProducts();
            dataGridEmpleados.ItemsSource = getProducts;
        }
        //private void CargarDatos()
        //{
        //    List<Product> getProducts = productBusiness.GetProducts();

        //    // Imprimir los datos recibidos antes de establecerlos en el DataGrid
        //    foreach (var product in getProducts)
        //    {
        //        Console.WriteLine($"ID: {product.productid}, Nombre: {product.name}, Precio: {product.price}, Stock: {product.stock}, Activo: {product.active}");
        //    }

        //    dataGridEmpleados.ItemsSource = getProducts;
        //}

        private void BuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            string productName = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(productName))
            {
                List<Product> productsByName = productBusiness.GetProductByName(productName);
                dataGridEmpleados.ItemsSource = productsByName;
            }
            else
            {
                // Restablecer el origen de datos a la lista completa de productos
                CargarDatos();
            }
        }

    }
}