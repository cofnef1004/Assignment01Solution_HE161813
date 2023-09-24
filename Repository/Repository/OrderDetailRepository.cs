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
	public class OrderDetailRepository : IOrderDetailRepository
	{
		Prn231As1Context _prn231As1Context;
		IMapper mapper;
		OrderDetailDAO orderDetailDAO;

		public OrderDetailRepository(Prn231As1Context context, IMapper mapper)
		{
			_prn231As1Context = context;
			this.mapper = mapper;
		}
		public void CreateOrderId(OrderDetailDTO orderDetailDTO)
		{
			throw new NotImplementedException();
		}

		public List<OrderDetailDTO> GetOrderDetailDTOs()
		{
			orderDetailDAO = new OrderDetailDAO(_prn231As1Context);
			List<OrderDetailDTO> orderDetailDTOs = mapper.Map<List<OrderDetailDTO>>(orderDetailDAO.GetOrderDetails());
			return orderDetailDTOs;
		}

		public List<OrderDetailDTO> GetOrderDetailDTOsByOrderId(int orderId)
		{
			orderDetailDAO = new OrderDetailDAO(_prn231As1Context);
			List<OrderDetailDTO> orderDetailDTOs = mapper.Map<List<OrderDetailDTO>>(orderDetailDAO.GetOrderDetailByOrderId(orderId));
			return orderDetailDTOs;
		}
	}
}
