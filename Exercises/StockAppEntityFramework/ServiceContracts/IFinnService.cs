namespace Service_Contracts
{
    public interface IFinnService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
