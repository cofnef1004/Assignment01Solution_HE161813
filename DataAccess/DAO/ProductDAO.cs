using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        Prn231As1Context _context;
        public ProductDAO(Prn231As1Context context)
        {
            _context = context;
        }
        public List<Product> GetProducts()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public List<Product> GetProductsByName(string name)
        {
            return _context.Products.Include(p => p.Category).Where(p=>p.ProductName.Contains(name)).ToList();
        }

        public List<Product> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice)
                .ToList();
        }

        public Product GetProductById(int productId)
        {
                return _context.Products.SingleOrDefault(p => p.ProductId == productId);
        }
        public void CreateProduct(Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            var p = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (p != null)
            {
                p.ProductName = product.ProductName;
                p.UnitPrice = product.UnitPrice;
                p.UnitsInStock = p.UnitsInStock;
                p.Weight = product.Weight;
                p.CategoryId = product.CategoryId;
                _context.SaveChanges();
            }
        }
		public void DeleteProduct(int id)
		{
			var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
			if (product != null)
			{
				var orderDetails = _context.OrderDetails.Where(od => od.ProductId == id);
				_context.OrderDetails.RemoveRange(orderDetails);

				_context.Products.Remove(product);
				_context.SaveChanges();
			}
		}
	}
}
