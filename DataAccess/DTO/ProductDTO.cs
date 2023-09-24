﻿using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string? ProductName { get; set; }

        public double Weight { get; set; }

        public decimal UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }
        [JsonIgnore]
        public virtual CategoryDTO? Category { get; set; }
    }
}
