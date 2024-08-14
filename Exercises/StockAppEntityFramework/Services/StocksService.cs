using Entities.Data;
using Service_Contracts;
using Services.DTO;
using Services.Extensions;
using Services.Utilities;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly OrderContext _context;

        public StocksService(OrderContext context)
        {
            _context = context;
        }

        // CreateBuyOrder: Inserts a new buy order into the database table called 'BuyOrders'.
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));

            ModelValidator.Validate(buyOrderRequest);

            var buyOrder = buyOrderRequest.ToBuyOrderType();

            _context.BuyOrders.Add(buyOrder);
            _context.SaveChanges();

            var buyOrderResponse = buyOrder.ToResponse();

            return Task.FromResult(buyOrderResponse);
        }

        // CreateSellOrder: Inserts a new sell order into the database table called 'SellOrders'.
        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));

            ModelValidator.Validate(sellOrderRequest);

            var sellOrder = sellOrderRequest.ToSellOrderType();

            var sellOrderResponse = sellOrder.ToResponse();
            _context.SellOrders.Add(sellOrder);
            _context.SaveChanges();

            return Task.FromResult(sellOrderResponse);
        }

        // GetBuyOrders: Returns the existing list of buy orders retrieved from database table called 'BuyOrders'.
        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            var responseList = _context.BuyOrders.Select(o => o.ToResponse()).ToList();

            return Task.FromResult(responseList);
        }

        // GetSellOrders: Returns the existing list of sell orders retrieved from database table called 'SellOrders'.
        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            var responseList = _context.SellOrders.Select(s => s.ToResponse()).ToList();

            return Task.FromResult(responseList);
        }
    }
}
