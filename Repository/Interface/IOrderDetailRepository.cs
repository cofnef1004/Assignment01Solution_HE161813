using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
	public interface IOrderDetailRepository
	{
		void CreateOrderId(OrderDetailDTO orderDetailDTO);
		List<OrderDetailDTO> GetOrderDetailDTOs();

		List<OrderDetailDTO> GetOrderDetailDTOsByOrderId(int orderId);
	}
}
