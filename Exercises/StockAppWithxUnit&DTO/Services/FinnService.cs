using Microsoft.Extensions.Configuration;
using Service_Contracts;
using System.Text.Json;

namespace StockAppWithConfiguration.Services
{
    public class FinnhubService : IFinnService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string? _token;

        public FinnhubService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _token = _configuration["finnhubapi:token"];
        }

        public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            var url = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_token}";
            return FetchDataAsync(url);
        }

        public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            var url = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["finnhubapi:token"]}";
            return FetchDataAsync(url);
        }

        private async Task<Dictionary<string, object>?> FetchDataAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

                if (responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from Finnhub server");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
                }

                return responseDictionary;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Error fetching data from Finnhub API", ex);
            }
        }
    }
}
