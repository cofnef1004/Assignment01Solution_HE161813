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
        public static List<Product> GetAllProducts()
        {
            using (var _context = new Prn231As1Context())
            {
                return _context.Products.ToList();
            }
        }

        public static Product GetProductById(int productId)
        {
            using (var _context = new Prn231As1Context())
            {
                return _context.Products.SingleOrDefault(p => p.ProductId == productId);
            }
        }

        public static List<Product> GetAllProductsByCateId(int cateId)
        {
            using (var _context = new Prn231As1Context())
            {
                return _context.Products.Where(p => p.CategoryId == cateId).ToList();
            }
        }

        public static void AddProduct(Product product)
        {
            using (var _context = new Prn231As1Context())
            {
                var category = _context.Categories.Find(product.CategoryId);

                if (category != null)
                {
                    var _product = new Product()
                    {
                        ProductId = product.ProductId,
                        CategoryId = product.CategoryId,
                        ProductName = product.ProductName,
                        Weight = product.Weight,
                        UnitPrice = product.UnitPrice,
                        UnitsInStock = product.UnitsInStock,
                        Category = category
                    };

                    _context.Products.Add(_product);
                    _context.SaveChanges();
                }
            }
        }

        public static void UpdateProduct(Product product)
        {
            using (var _context = new Prn231As1Context())
            {
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }

        public static void DeleteProduct(Product product)
        {
            using (var _context = new Prn231As1Context())
            {
                var product1 = _context.Products.SingleOrDefault(p => p.ProductId == product.ProductId);
                if (product1 != null)
                {
                    _context.Products.Remove(product1);
                    _context.SaveChanges();
                }
            }
        }
    }
}
