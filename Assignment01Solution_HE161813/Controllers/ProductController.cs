using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Repository.Interface;
using Repository.Repository;

namespace eStoreAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
        private IProductRepository _productRepository;
		private IOrderDetailRepository _orderDetailRepository;

		public ProductController(IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        //get all
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productRepository.GetProducts();
                return Ok(products);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        //Search
        [HttpGet("Search")]
        public IActionResult SearchProductByName(string name)
        {
            try
            {
                var products = _productRepository.GetProductsByName(name);
                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Filter
        [HttpGet("Filter")]
        public IActionResult FilterProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            try
            {
                var products = _productRepository.GetProductsByPrice(minPrice, maxPrice);
                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //create new product
        [HttpPost]
        public IActionResult CreateProduct(ProductDTO productDTO)
        {
            try
            {
                _productRepository.CreateProduct(productDTO);
                return Ok(productDTO);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductDTO productDTO)
        {
            productDTO.ProductId = id;
            try
            {
                _productRepository.UpdateProduct(productDTO);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
