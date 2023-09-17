using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
	public interface IProductRepository
	{
		List<Product> GetProducts();
        Product GetProductById(int productId);
        List<Product> GetProductsByCateId(int cateId);
        void SaveProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
