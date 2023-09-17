using BusinessObject.Models;
using DataAccess.DAO;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
	public class ProductRepository : IProductRepository
	{
        public void SaveProduct(Product product) => ProductDAO.AddProduct(product);

        public void DeleteProduct(Product product) => ProductDAO.DeleteProduct(product);

        public void UpdateProduct(Product product) => ProductDAO.UpdateProduct(product);

        public List<Product> GetProducts()
		{
			return ProductDAO.GetAllProducts();
		}

		public List<Product> GetProductsByCateId(int cateId)
		{
			return ProductDAO.GetAllProductsByCateId(cateId);
		}

        public Product GetProductById(int productId) => ProductDAO.GetProductById(productId);
    }
}
