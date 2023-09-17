using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Controllers
{
	public class CategoryController : Controller
	{
		private readonly HttpClient client = null;
		private string ProductApiUrl = "";

		public CategoryController()
		{
			client = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			ProductApiUrl = "http://localhost:5046/api/Product";
		}

		public async Task<IActionResult> Index()
		{
			HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
			string strData = await response.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};
			List<Category> list = JsonSerializer.Deserialize<List<Category>>(strData, options);
			return View(list);
		}
	}
}
