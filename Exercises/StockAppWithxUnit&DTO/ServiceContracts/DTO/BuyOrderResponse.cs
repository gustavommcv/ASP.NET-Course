namespace Services.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != this.GetType())
                return false;

            BuyOrderResponse orderResponse = (BuyOrderResponse)obj;

            if (orderResponse == null) return false;

            if (orderResponse.TradeAmount == TradeAmount
                && orderResponse.StockSymbol == StockSymbol
                && orderResponse.Quantity == Quantity
                && orderResponse.Price == Price
                && orderResponse.BuyOrderID == BuyOrderID
                && orderResponse.DateAndTimeOfOrder == DateAndTimeOfOrder
                && orderResponse.StockName == StockName)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
