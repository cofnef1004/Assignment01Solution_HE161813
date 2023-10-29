using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Member, MemberDTO>().ReverseMap();
			CreateMap<Order, OrderDTO>().ReverseMap();
			CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
		}
    }
}
