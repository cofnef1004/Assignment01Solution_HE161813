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

		public OrderController(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
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
