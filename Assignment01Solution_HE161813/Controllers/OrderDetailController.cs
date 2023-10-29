using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Repository;

namespace eStoreAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderDetailController : ControllerBase
	{
		private IOrderDetailRepository _orderDetailRepository;

		public OrderDetailController(IOrderDetailRepository orderDetailRepository)
		{
			_orderDetailRepository = orderDetailRepository;
		}

		//get all
		[HttpGet]
		public IActionResult GetOrderDetails()
		{
			try
			{
				var orderDetailDTOs = _orderDetailRepository.GetOrderDetailDTOs();
				return Ok(orderDetailDTOs);
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}

		[HttpGet("{orderid}")]
		public IActionResult GetOrderDetailByOrderId(int orderId)
		{
			try
			{
				var orders = _orderDetailRepository.GetOrderDetailDTOsByOrderId(orderId);
				return Ok(orders);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteOrderDetail(int id)
		{
			try
			{
				_orderDetailRepository.DeleteOrderDetail(id);
				return Ok();
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}
	}
}
