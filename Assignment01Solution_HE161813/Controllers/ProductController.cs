using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Repository;

namespace eStoreAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private IProductRepository repository = new ProductRepository();

		[HttpGet]
		public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();

		[HttpGet("{categoryId}")]
		public ActionResult<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
		{
			return repository.GetProductsByCateId(categoryId);
		}

        [HttpPost]
        public IActionResult PostProduct(Product p)
        {
            repository.SaveProduct(p);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
                return NotFound();
            repository.DeleteProduct(p);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var p = repository.GetProductById(id);
            if (p == null)
                return NotFound();
            repository.UpdateProduct(product);
            return NoContent();
        }
    }
}
