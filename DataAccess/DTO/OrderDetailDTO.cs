using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

		[AllowNull]
		public virtual OrderDTO? Order { get; set; }
		[AllowNull]
		public virtual ProductDTO? Product { get; set; }
	}
}
