using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class OrderDTO
	{
		public int OrderId { get; set; }

		public int MemberId { get; set; }

		public DateTime OrderDate { get; set; }

		public DateTime? RequireDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public DateTime Freight { get; set; }

		[JsonIgnore]
		public virtual MemberDTO? Member { get; set; }
	}
}
