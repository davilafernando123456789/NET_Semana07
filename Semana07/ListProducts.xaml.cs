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
    public partial class ListProducts : Window
    {
        private ProductBusiness productBusiness;
        private Product productoSeleccionado;

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
                CargarDatos();
            }
        }
        private void EditarProducto_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el producto seleccionado
            productoSeleccionado = ((FrameworkElement)sender).DataContext as Product;

            // Mostrar los campos de edición
            editPanel.Visibility = Visibility.Visible;

            // Cargar los valores del producto seleccionado en los campos de edición
            txtProductName.Text = productoSeleccionado.name;
            txtPrice.Text = productoSeleccionado.price.ToString();
            txtStock.Text = productoSeleccionado.stock.ToString();
            chkActive.IsChecked = productoSeleccionado.active;
        }

        private void GuardarEdicion_Click(object sender, RoutedEventArgs e)
        {
            productoSeleccionado.name = txtProductName.Text;
            productoSeleccionado.price = decimal.Parse(txtPrice.Text);
            productoSeleccionado.stock = int.Parse(txtStock.Text);
            productoSeleccionado.active = chkActive.IsChecked ?? false;

            bool success = productBusiness.UpdateProduct(productoSeleccionado);
            if (success)
            {
                MessageBox.Show("El producto se actualizó correctamente.");
            }
            else
            {
                MessageBox.Show("Hubo un error al actualizar el producto. Por favor, inténtalo de nuevo.");
            }

            editPanel.Visibility = Visibility.Collapsed;
            CargarDatos();
        }

        private void EliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            Product productoSeleccionado = ((FrameworkElement)sender).DataContext as Product;
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool success = productBusiness.DeleteProduct(productoSeleccionado.productid);
                if (success)
                {
                    MessageBox.Show("El producto se eliminó correctamente.");
                }
                else
                {
                    MessageBox.Show("Hubo un error al eliminar el producto. Por favor, inténtalo de nuevo.");
                }
                CargarDatos();
            }
        }


    }
}