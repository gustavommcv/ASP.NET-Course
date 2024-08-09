namespace Services.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

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

            var orderResponse = (SellOrderResponse)obj;

            if (orderResponse == null) return false;

            if (orderResponse.TradeAmount == TradeAmount
                && orderResponse.StockSymbol == StockSymbol
                && orderResponse.Quantity == Quantity
                && orderResponse.Price == Price
                && orderResponse.SellOrderID == SellOrderID
                && orderResponse.DateAndTimeOfOrder == DateAndTimeOfOrder
                && orderResponse.StockName == StockName)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"StockSymbol: {StockSymbol}, " +
                   $"StockName: {StockName}, " +
                   $"DateAndTimeOfOrder: {DateAndTimeOfOrder}, " +
                   $"Quantity: {Quantity}, " +
                   $"Price: {Price}, " +
                   $"TradeAmount: {TradeAmount}";
        }
    }
}
