using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eStoreAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MemberController : ControllerBase
	{
        private IMemberRepository memberRepository;
		private IOrderRepository orderRepository;
		public MemberController(IMemberRepository memberRepository,IOrderRepository orderRepository)
        {
            this.memberRepository = memberRepository;
			this.orderRepository = orderRepository;
		}
        [HttpGet]
        public IActionResult GetMembers()
        {
            try
            {
                var members = memberRepository.GetMembers();
                return Ok(members);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

		[HttpGet("{id}")]
		public ActionResult<MemberDTO> GetMemberById(int id)
		{
			return memberRepository.GetMemberById(id);
		}

        //Search
        [HttpGet("email/{email}")]
        public IActionResult SearchMemberByEmail(string email)
		{
			try
			{
				var products = memberRepository.GetMemberByEmail(email);
				return Ok(products);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpPost]
        public IActionResult CreateMember(MemberDTO m)
        {
            try
            {
                memberRepository.CreateMember(m);
                return Ok(m);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, MemberDTO m)
        {
            m.MemberId = id;
            try
            {
                memberRepository.UpdateMember(m);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            try
            {
				memberRepository.DeleteMember(id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var result = memberRepository.Login(email, password);
                if (result != null)
                {
                    var user = memberRepository.GetMemberByEmail(email);
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
