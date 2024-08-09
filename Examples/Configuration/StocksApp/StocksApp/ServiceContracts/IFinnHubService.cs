namespace StocksApp.ServiceContracts
{
    public interface IFinnHubService
    {
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stocksymbol);
    }
}
