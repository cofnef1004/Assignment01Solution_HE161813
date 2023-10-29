using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	public class OrderDetailDAO
	{
		Prn231As1Context _context;
		public OrderDetailDAO(Prn231As1Context context)
		{
			_context = context;
		}
		public List<OrderDetail> GetOrderDetails()
		{
			return _context.OrderDetails.Include(p=>p.Order)
										.Include(p=>p.Product).ToList();
		}

		public List<OrderDetail> GetOrderDetailByOrderId(int orderId)
		{
			return _context.OrderDetails.Include(p=>p.Order)
										.Include(p=>p.Product).Where(p => p.OrderId == orderId).ToList();
		}
		public void CreateOrderDetail(OrderDetail p)
		{
			_context.OrderDetails.Add(p);
			_context.SaveChanges();
		}

		public void DeleteOrderDetail(int id)
		{
			var p = _context.OrderDetails.FirstOrDefault(x => x.ProductId == id);
			if (p != null)
			{
				_context.OrderDetails.Remove(p);
				_context.SaveChanges();
			}
		}
	}
}
