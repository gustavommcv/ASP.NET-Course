using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class SellOrder
    {
        public Guid SellOrderID { get; set; }

        // [Mandatory]
        [Required]
        public string? StockSymbol { get; set; }

        // [Mandatory]
        [Required]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        // [Range(1, 100000)]
        [Range(1, 100000)]
        public uint Quantity { get; set; }

        // [Range(1, 10000)]
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
