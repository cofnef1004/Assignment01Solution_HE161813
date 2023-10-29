using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.DTO;
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
        Prn231As1Context _prn231As1Context;
        IMapper mapper;
        ProductDAO productDAO;
        public ProductRepository(Prn231As1Context context, IMapper mapper)
        {
            _prn231As1Context = context;
            this.mapper = mapper;
        }

        public void CreateProduct(ProductDTO productDTO)
        {
            productDAO = new ProductDAO(_prn231As1Context);
            Product p = mapper.Map<Product>(productDTO);
            productDAO.CreateProduct(p);
        }

        public List<ProductDTO> GetProducts()
        {
            productDAO = new ProductDAO(_prn231As1Context);
            List<ProductDTO> productDTOs = mapper.Map<List<ProductDTO>>(productDAO.GetProducts());
            return productDTOs;
        }


        public ProductDTO GetProductById(int id)
        {
            productDAO = new ProductDAO(_prn231As1Context);
            ProductDTO productDTOs = mapper.Map<ProductDTO>(productDAO.GetProductById(id));
            return productDTOs;
        }
        public void UpdateProduct(ProductDTO productDTO)
        {
            productDAO = new ProductDAO(_prn231As1Context);
            Product p = mapper.Map<Product>(productDTO);
            productDAO.UpdateProduct(p);
        }
        public void DeleteProduct(int id)
        {
            productDAO = new ProductDAO(_prn231As1Context);
            productDAO.DeleteProduct(id);
        }

        public List<ProductDTO> GetProductsByName(string name)
        {
            productDAO = new ProductDAO(_prn231As1Context);
            List<ProductDTO> productDTOs = mapper.Map<List<ProductDTO>>(productDAO.GetProductsByName(name));
            return productDTOs;
        }

        public List<ProductDTO> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            productDAO = new ProductDAO(_prn231As1Context);
            List<ProductDTO> productDTOs = mapper.Map<List<ProductDTO>>(productDAO.GetProductsByPrice(minPrice,maxPrice));
            return productDTOs;
        }
    }
}
