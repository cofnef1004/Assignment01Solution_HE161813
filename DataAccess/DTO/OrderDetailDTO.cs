using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class OrderDetailDTO
	{
		public int OrderId { get; set; }

		public int ProductId { get; set; }

		public decimal UnitPrice { get; set; }

		public int? Quantity { get; set; }

		public double Discount { get; set; }

		[JsonIgnore]
		public virtual Order? Order { get; set; }
		[JsonIgnore]
		public virtual Product? Product { get; set; }
	}
}
