using Services.DTO;

namespace StockAppCRUD.Models
{
    public class Orders
    {
        public List<BuyOrderResponse>? BuyOrders { get; set; }
        public List<SellOrderResponse>? SellOrders { get; set; }
    }
}
