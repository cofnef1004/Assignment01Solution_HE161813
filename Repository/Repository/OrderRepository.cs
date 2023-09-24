using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.DTO;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
	public class OrderRepository : IOrderRepository
	{
		Prn231As1Context _prn231As1Context;
		IMapper mapper;
		OrderDAO orderDAO;
		public OrderRepository(Prn231As1Context context, IMapper mapper)
		{
			_prn231As1Context = context;
			this.mapper = mapper;
		}

		public void CreateOrder(OrderDTO orderDTO)
		{
			orderDAO = new OrderDAO(_prn231As1Context);
			Order p = mapper.Map<Order>(orderDTO);
			orderDAO.CreateOrder(p);
		}

		public List<OrderDTO> GetOrders()
		{
			orderDAO = new OrderDAO(_prn231As1Context);
			List<OrderDTO> orderDTOs = mapper.Map<List<OrderDTO>>(orderDAO.GetOrders());
			return orderDTOs;
		}


		public OrderDTO GetOrderById(int id)
		{
			orderDAO = new OrderDAO(_prn231As1Context);
			OrderDTO orderDTO = mapper.Map<OrderDTO>(orderDAO.GetOrderById(id));
			return orderDTO;
		}
		public void UpdateOrder(OrderDTO orderDTO)
		{
			orderDAO = new OrderDAO(_prn231As1Context);
			Order p = mapper.Map<Order>(orderDTO);
			orderDAO.UpdateOrder(p);
		}
		public void DeleteOrder(int id)
		{
			orderDAO = new OrderDAO(_prn231As1Context);
			orderDAO.DeleteOrder(id);
		}

	}
}
