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
		List<OrderDetailDTO> GetOrderDetailDTOs();

		List<OrderDetailDTO> GetOrderDetailDTOsByOrderId(int orderId);
		void DeleteOrderDetail(int id);
	}
}
