using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	public class MemberDAO
	{
        Prn231As1Context _context;
        public MemberDAO(Prn231As1Context context)
        {
            _context = context;
        }
        public List<Member> GetMembers()
        {
            return _context.Members.ToList();
        }

		public Member GetMemberById(int id)
		{
			return _context.Members.FirstOrDefault(p => p.MemberId.Equals(id));
		}

		public Member GetMemberByEmail(string email) 
        {
            return _context.Members.FirstOrDefault(p => p.Email.Equals(email));
        }
        public void CreateMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }
        public void UpdateMember(Member member)
        {
            var m = _context.Members.FirstOrDefault(x => x.MemberId == member.MemberId);
            if (m != null)
            {
                m.Email = member.Email;
                m.City = member.City;
                m.CompanyName = member.CompanyName;
                m.Country = member.Country;
                m.Password = member.Password;
                _context.SaveChanges();
            }
        }
		public void DeleteMember(int id)
		{
			var member = _context.Members.Include(m => m.Orders).ThenInclude(o => o.OrderDetails).FirstOrDefault(x => x.MemberId == id);
			if (member != null)
			{
				var orders = member.Orders;

				foreach (var order in orders)
				{
					var orderDetails = order.OrderDetails;
					_context.OrderDetails.RemoveRange(orderDetails);
				}

				_context.Orders.RemoveRange(orders);
				_context.Members.Remove(member);
				_context.SaveChanges();
			}
		}

		public Member Login(string email, string password)
        {
            Member member = _context.Members.FirstOrDefault(m => m.Email == email && m.Password == password);
            return member;
        }
    }
}
