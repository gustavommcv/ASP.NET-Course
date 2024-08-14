using Entities;
using Entities.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Services.DTO
{
    public class BuyOrderRequest
    {
        // [Mandatory]
        [Required]
        public string? StockSymbol { get; set; }

        // [Mandatory]
        [Required]
        public string? StockName { get; set; }

        // [Should not be older than Jan 01, 2000]
        [DateRange("2000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }

        // [Value should be between 1 and 100000]
        [Range(1, 100000)]
        public uint Quantity { get; set; }

        // [Value should be between 1 and 10000]
        [Range(1, 10000)]
        public double Price { get; set; }

        public BuyOrder ToBuyOrderType()
        {
            var buyOrder = new BuyOrder() 
            {
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Price = Price,
                Quantity = Quantity,
                StockName = StockName,
                StockSymbol = StockSymbol
            };

            return buyOrder;
        }

        public override string ToString()
        {
            return $"StockSymbol: {StockSymbol}, " +
                   $"StockName: {StockName}, " +
                   $"DateAndTimeOfOrder: {DateAndTimeOfOrder:yyyy-MM-dd HH:mm:ss}, " +
                   $"Quantity: {Quantity}, " +
                   $"Price: {Price:F2}";
        }
    }
}
