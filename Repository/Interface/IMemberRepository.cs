﻿using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
	public interface IMemberRepository
	{
        List<MemberDTO> GetMembers();
        void CreateMember(MemberDTO m);
        void UpdateMember(MemberDTO m);
        void DeleteMember(int id);
    }
}
