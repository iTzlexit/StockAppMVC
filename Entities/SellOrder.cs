﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SellOrder
    {
        [Key]
        public Guid SellOrderID { get; set; }
        public string StockSymbol{ get; set; } = string.Empty;
        [Required(ErrorMessage = "Stock name can't be null or empty ")]
        public string StockName { get; set; } = string.Empty;

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares in single order. Minimum is 1.")]
        public uint Quantity { get; set; }
        [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1")]
        public double Price { get; set; }
    }
}
