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
	public class MemberRepository : IMemberRepository
	{
        Prn231As1Context _prn231As1Context;
        IMapper mapper;
        MemberDAO memberDAO;
        public MemberRepository(Prn231As1Context context, IMapper mapper)
        {
            _prn231As1Context = context;
            this.mapper = mapper;
        }

        public MemberRepository()
        {
        }

        public List<MemberDTO> GetMembers()
        {
            memberDAO = new MemberDAO(_prn231As1Context);
            List<MemberDTO> members = mapper.Map<List<MemberDTO>>(memberDAO.GetMembers());
            return members;
        }
        public void CreateMember(MemberDTO m)
        {
            memberDAO = new MemberDAO(_prn231As1Context);
            Member member = mapper.Map<Member>(m);
            memberDAO.CreateMember(member);
        }
        public void UpdateMember(MemberDTO m)
        {
            memberDAO = new MemberDAO(_prn231As1Context);
            Member member = mapper.Map<Member>(m);
            memberDAO.UpdateMember(member);
        }
        public void DeleteMember(int id)
        {
            memberDAO = new MemberDAO(_prn231As1Context);
            memberDAO.DeleteMember(id);
        }

        public MemberDTO Login(string email, string password)
        {
            memberDAO = new MemberDAO(_prn231As1Context);
            Member member = memberDAO.Login(email, password);
            MemberDTO memberDTO = mapper.Map<MemberDTO>(member);
            return memberDTO;
        }

        public MemberDTO GetMemberByEmail(string email)
        {
            memberDAO = new MemberDAO(_prn231As1Context);
            Member member = memberDAO.GetMemberByEmail(email);
            MemberDTO memberDTO = mapper.Map<MemberDTO>(member);
            return memberDTO;
        }

		public MemberDTO GetMemberById(int id)
		{
			memberDAO = new MemberDAO(_prn231As1Context);
			Member member = memberDAO.GetMemberById(id);
			MemberDTO memberDTO = mapper.Map<MemberDTO>(member);
			return memberDTO;
		}
	}
}
