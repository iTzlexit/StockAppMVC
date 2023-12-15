using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Represents a buy order to purchase the stocks 
    /// </summary>
    public class BuyOrder
    {
        [Key] 
        public Guid BuyOrderID { get; set; }
        public string StockSymbol { get; set; } = string.Empty;
        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string StockName { get; set;} = string.Empty;
        /// <summary>
        /// Date and time of order, when it is placed by the user
        /// </summary>
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 10000, ErrorMessage = "You can buy maximum of 100000 shares in single order. Minimum is 1.")]

        /// <summary>
        /// The number of stocks (shares) to buy
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        /// The price of each stock (share)
        /// </summary>
        [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1.")]
        public double Price { get; set; }
    }
}
