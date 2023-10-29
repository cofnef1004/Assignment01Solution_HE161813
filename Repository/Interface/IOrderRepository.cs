using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
	public interface IOrderRepository
	{
		void CreateOrder(OrderDTO order);
		List<OrderDTO> GetOrders();

        List<OrderDTO> GetOrdersByMemId(int memId);

        OrderDTO GetOrderById(int id);
		void UpdateOrder(OrderDTO order);
		void DeleteOrder(int id);
	}
}
