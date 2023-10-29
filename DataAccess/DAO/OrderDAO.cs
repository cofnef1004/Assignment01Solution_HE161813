using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	public class OrderDAO
	{
		Prn231As1Context _context;
		public OrderDAO(Prn231As1Context context)
		{
			_context = context;
		}
		public List<Order> GetOrders()
		{
			return _context.Orders.Include(p => p.Member).ToList();
		}

        public List<Order> GetOrdersByMemId(int memId)
        {
            return _context.Orders.Include(p => p.Member).Where(p=>p.MemberId == memId).ToList();
        }

        public Order GetOrderById(int orderId)
		{
			return _context.Orders.SingleOrDefault(p => p.OrderId == orderId);
		}
		public void CreateOrder(Order p)
		{
			_context.Orders.Add(p);
			_context.SaveChanges();
		}
		public void UpdateOrder(Order order)
		{
			var p = _context.Orders.FirstOrDefault(x => x.OrderId == order.OrderId);
			if (p != null)
			{
				p.OrderDate = order.OrderDate;
				p.RequireDate = order.RequireDate;
				p.ShippedDate = order.ShippedDate;
				p.Freight = order.Freight;
				p.OrderId = order.OrderId;
				_context.SaveChanges();
			}
		}
		public void DeleteOrder(int id)
		{
			var p = _context.Orders.FirstOrDefault(x => x.OrderId == id);
			if (p != null)
			{
				var orderDetails = _context.OrderDetails.Where(od => od.OrderId == id);
				_context.OrderDetails.RemoveRange(orderDetails);
				_context.Orders.Remove(p);
				_context.SaveChanges();
			}
		}
	}
}
