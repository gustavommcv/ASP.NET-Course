using Entities;
using Services.DTO;

namespace Services.Extensions
{
    public static class BuyOrderExtensions
    {
        public static BuyOrderResponse ToResponse(this BuyOrder buyOrder)
        {
            if (buyOrder == null) throw new ArgumentNullException(nameof(buyOrder));

            var response = new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                TradeAmount = buyOrder.Quantity * buyOrder.Price // Calculate TradeAmount
            };

            return response;
        }
    }
}
