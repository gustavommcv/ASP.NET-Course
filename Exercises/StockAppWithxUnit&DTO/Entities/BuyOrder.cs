using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class BuyOrder
    {
        public BuyOrder() 
        {
            this.BuyOrderID = Guid.NewGuid();
        }

        public Guid BuyOrderID { get; set; }

        // [Mandatory]
        [Required]
        public string? StockSymbol { get; set; }

        // [Mandatory]
        [Required]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        // [Value should be between 1 and 100000]
        [Range(1, 100000)]
        public uint Quantity { get; set; }

        // [Value should be between 1 and 10000]
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
