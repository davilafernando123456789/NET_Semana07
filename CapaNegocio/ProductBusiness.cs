using CapaDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductBusiness
    {
        public List<Product> GetProducts()
        {
            DProduct data = new DProduct();
            var products = data.getProducts();
            return products;
        }
        public List<Product> GetProductByName(string productName)
        {
            DProduct data = new DProduct();
            var products = data.getProducts();
            var result = products.Where(x => x.name == productName).ToList();
            return result;
        }

        public bool UpdateProduct(Product product)
        {
            DProduct data = new DProduct();
            return data.UpdateProduct(product);
        }

        public bool DeleteProduct(int productId)
        {
            DProduct data = new DProduct();
            return data.DeleteProduct(productId);
        }

    }
}
