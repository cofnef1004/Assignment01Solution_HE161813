using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Repository.Interface;
using Repository.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eStoreAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private IOrderRepository _orderRepository;
		private IOrderDetailRepository _orderdetailRepository;

		public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderdetailRepository)
		{
			_orderRepository = orderRepository;
			_orderdetailRepository = orderdetailRepository;
		}
		//get all
		[HttpGet]
		public IActionResult GetOrders()
		{
			try
			{
				var orders = _orderRepository.GetOrders();
				return Ok(orders);
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}

		[HttpGet("{id}")]
		public ActionResult<OrderDTO> GetOrderById(int id)
		{
			return _orderRepository.GetOrderById(id);
		}

        [HttpGet("member/{memberId}")]
        public IActionResult GetOrdersByMemberId(int memberId)
        {
            try
            {
                var orders = _orderRepository.GetOrdersByMemId(memberId);
                return Ok(orders);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //create new product
        [HttpPost]
		public IActionResult CreateOrder(OrderDTO orderDTO)
		{
			try
			{
				_orderRepository.CreateOrder(orderDTO);
				return Ok(orderDTO);
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}
		[HttpPut("{id}")]
		public IActionResult UpdateOrder(int id, OrderDTO orderDTO)
		{
			orderDTO.OrderId = id;
			try
			{
				_orderRepository.UpdateOrder(orderDTO);
				return Ok();
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteOrder(int id)
		{
			try
			{
				_orderdetailRepository.DeleteOrderDetail(id);
				_orderRepository.DeleteOrder(id);
				return Ok();
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}
	}
}
