using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
	public interface IProductRepository
	{
        void CreateProduct(ProductDTO product);
        List<ProductDTO> GetProducts();

        ProductDTO GetProductById(int id);
        void UpdateProduct(ProductDTO product);
        void DeleteProduct(int id);
    }
}
