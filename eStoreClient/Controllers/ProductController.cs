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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product p)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonSerializer.Serialize(p);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(ProductApiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error");
                }
            }
            return View(p);
        }

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
