using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eStoreClient.Controllers
{
	public class ProductController : Controller
	{
		private readonly HttpClient client = null;
		private string ProductApiUrl = "";
		private string CategoryApiUrl = "";

		public ProductController()
		{
			client = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			ProductApiUrl = "http://localhost:5046/api/Product";
			CategoryApiUrl = "http://localhost:5046/api/Category";
		}

        private async Task<List<Category>> GetCategories()
		{
			HttpResponseMessage categoryResponse = await client.GetAsync(CategoryApiUrl);
			string categoryStrData = await categoryResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};
			List<Category> categoryList = JsonSerializer.Deserialize<List<Category>>(categoryStrData, options);
			return categoryList;
		}

		//View
		public async Task<IActionResult> Index(int? categoryId)
		{
			HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl);
			string productStrData = await productResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};
			List<Product> productList = JsonSerializer.Deserialize<List<Product>>(productStrData, options);

			List<Category> categoryList = await GetCategories();

			if (categoryId != null)
			{
				productList = productList.Where(p => p.CategoryId == categoryId).ToList();
			}

			foreach (var product in productList)
			{
				Category category = categoryList.FirstOrDefault(c => c.CategoryId == product.CategoryId);
				product.Category = category;
			}
			ViewData["Categories"] = new SelectList(categoryList, "CategoryId", "CategoryName");
			ViewData["CurrentCategoryId"] = categoryId;

			return View(productList);
		}

		//Create
        public async Task<IActionResult> Create()
        {
            List<Category> categoryList = await GetCategories();
            ViewData["Categories"] = new SelectList(categoryList, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl, product);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }


		//Update
		public async Task<IActionResult> Edit(int id)
		{
			List<Category> categoryList = await GetCategories();
			ViewData["Categories"] = new SelectList(categoryList, "CategoryId", "CategoryName");

			HttpResponseMessage productResponse = await client.GetAsync($"{ProductApiUrl}/{id}");
			if (productResponse.IsSuccessStatusCode)
			{
				string productStrData = await productResponse.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				};
				Product product = JsonSerializer.Deserialize<Product>(productStrData, options);
				return View(product);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Product product)
		{
			HttpResponseMessage response = await client.PutAsJsonAsync($"{ProductApiUrl}/{id}", product);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return BadRequest();
		}


		//Delete
		[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
