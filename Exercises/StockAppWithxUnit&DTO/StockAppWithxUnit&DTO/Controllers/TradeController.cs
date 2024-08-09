using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service_Contracts;
using StockAppWithConfiguration;
using StockAppWithxUnit_DTO.Models;

namespace StockAppWithxUnit_DTO.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _options;
        private readonly IFinnService _finnhubService;
        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> options, IFinnService finnhubService, IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _options = options.Value;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/trade/index")]
        public async Task<IActionResult> Index()
        {
            ViewData["token"] = _configuration["finnhubapi:token"];

            var symbol = _options.DefaultStockSymbol;

            if (symbol == null)
                return BadRequest();

            var companyProfile = await _finnhubService.GetCompanyProfile(symbol);
            var stockPriceQuote = await _finnhubService.GetStockPriceQuote(symbol);

            var stockSymbol = companyProfile?.FirstOrDefault(kv => kv.Key == "ticker").Value.ToString();
            var stockName = companyProfile?.FirstOrDefault(kv => kv.Key == "name").Value.ToString();
            var price = stockPriceQuote?.FirstOrDefault(kv => kv.Key == "c").Value.ToString();

            var stockTrade = new StockTrade()
            {
                StockSymbol = stockSymbol?.ToString(),
                Price = Convert.ToDouble(price),
                Quantity = 5,
                StockName = stockName?.ToString()
            };

            return View(stockTrade);
        }
    }
}
