using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient client = null;
        private string OrderApiUrl = "";
        private string MemberApiUrl = "";

        public OrderController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "http://localhost:5046/api/Order";
            MemberApiUrl = "http://localhost:5046/api/Member";
        }

        private async Task<List<Member>> GetMembers()
        {
            HttpResponseMessage categoryResponse = await client.GetAsync(MemberApiUrl);
            string categoryStrData = await categoryResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Member> categoryList = JsonSerializer.Deserialize<List<Member>>(categoryStrData, options);
            return categoryList;
        }

        //View
        public async Task<IActionResult> Index()
        {
            string memId = HttpContext.Session.GetString("Customer");
            string admin = HttpContext.Session.GetString("isAdmin");
            TempData["IsAdmin"] = HttpContext.Session.GetString("IsAdmin");
            if (!string.IsNullOrEmpty(memId))
            {
                var member = JsonSerializer.Deserialize<Member>(memId);
                int memberId = member.MemberId;
                HttpResponseMessage orderResponse = await client.GetAsync($"{OrderApiUrl}/member/{memberId}");
                string orderStrData = await orderResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Order> ordersList = JsonSerializer.Deserialize<List<Order>>(orderStrData, options);

                return View(ordersList);
            }
            else
            {
                HttpResponseMessage productResponse = await client.GetAsync(OrderApiUrl);
                string productStrData = await productResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Order> ordersList = JsonSerializer.Deserialize<List<Order>>(productStrData, options);
                return View(ordersList);
            }
        }

        //Create
        public async Task<IActionResult> Create()
        {
            List<Member> members = await GetMembers();
            ViewData["Members"] = new SelectList(members, "MemberId", "Email");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(OrderApiUrl, order);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        //Update
        public async Task<IActionResult> Edit(int id)
        {
            List<Member> members = await GetMembers();
            ViewData["Members"] = new SelectList(members, "MemberId", "Email");
            HttpResponseMessage productResponse = await client.GetAsync($"{OrderApiUrl}/{id}");
            if (productResponse.IsSuccessStatusCode)
            {
                string productStrData = await productResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Order product = JsonSerializer.Deserialize<Order>(productStrData, options);
                return View(product);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{OrderApiUrl}/{id}", order);

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
            string isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin == "true")
            {
                HttpResponseMessage response = await client.DeleteAsync($"{OrderApiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

		private bool IsLoggedIn()
		{
			var isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
			return isLoggedIn == "true";
		}
	}
}
