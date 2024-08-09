using Entities;
using Services.DTO;

namespace Services.Extensions
{
    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToResponse(this SellOrder sellOrder)
        {
            if (sellOrder == null) throw new ArgumentNullException(nameof(sellOrder));

            var response = new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Quantity * sellOrder.Price // Calculate TradeAmount
            };

            return response;
        }
    }
}
