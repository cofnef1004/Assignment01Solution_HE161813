using AutoMapper.Execution;
using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Repository;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Member = BusinessObject.Models.Member;

namespace eStoreClient.Controllers
{
    public class MemberController : Controller
    {
        private readonly HttpClient client = null;
        private string MemberApiUrl = "";

        public MemberController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
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

        private async Task<Member> GetMemberById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{MemberApiUrl}/{id}");
            string StrData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Member p = JsonSerializer.Deserialize<Member>(StrData, options);
            return p;
        }

        private async Task<Member> GetMemberLogin(string email)
        {
            HttpResponseMessage response = await client.GetAsync($"{MemberApiUrl}/email/{email}");
            string strData = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Member member = JsonSerializer.Deserialize<Member>(strData, options);
                return member;
            }
            else
            {
                throw new Exception("Failed to retrieve member data.");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Member p)
        {
            if (p.Email == "admin" && p.Password == "123")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
				return RedirectToAction("Index", "Product");
            }
            else
            {
                try
                {
                    var customer = await GetMemberLogin(p.Email);
                    var customerJson = JsonSerializer.Serialize(customer);
                    HttpContext.Session.SetString("Customer", customerJson);
					return RedirectToAction("Index", "Product");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Failed to retrieve member data.");
                    return View();
                }
            }
        }

        public async Task<IActionResult> Profile()
        {
            string customerJson = HttpContext.Session.GetString("Customer");
            string admin = HttpContext.Session.GetString("isAdmin");

            if (customerJson != null)
            {
                Member member = JsonSerializer.Deserialize<Member>(customerJson);
                return View("Profile", member);
            }
            else if (admin == "true")
            {
                List<Member> members = await GetMembers();
                return View("MemberList", members);
            }
            else
            {
                return RedirectToAction("Login", "Member");
            }
        }

        public async Task<IActionResult> MemberList()
        {
            List<Member> members = await GetMembers();
            return View("MemberList", members);
        }

        [HttpGet]
        public IActionResult Update()
        {
            string customerJson = HttpContext.Session.GetString("Customer");

            if (customerJson != null)
            {
                Member existingMember = JsonSerializer.Deserialize<Member>(customerJson);
                return View(existingMember);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Member updatedMember)
        {
            string customerJson = HttpContext.Session.GetString("Customer");

            if (customerJson != null)
            {
                Member existingMember = JsonSerializer.Deserialize<Member>(customerJson);
                existingMember.Email = updatedMember.Email;
                existingMember.CompanyName = updatedMember.CompanyName;
                existingMember.City = updatedMember.City;
                existingMember.Country = updatedMember.Country;
                existingMember.Password = updatedMember.Password;

                var json = JsonSerializer.Serialize(existingMember);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{MemberApiUrl}/{existingMember.MemberId}", content);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString("Customer", JsonSerializer.Serialize(existingMember));
                    return RedirectToAction("Profile");
                }
                else
                {
                    return BadRequest();
                }
            }
            return RedirectToAction("Login");
        }
    }
}
