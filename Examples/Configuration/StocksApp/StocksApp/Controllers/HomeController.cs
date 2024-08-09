using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Services;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnHubService _myService;
        private readonly IOptions<TradingOptions> _tradingOptions;

        public HomeController(FinnHubService myService, IOptions<TradingOptions> tradingOptions)
        {
            _myService = myService;
            _tradingOptions = tradingOptions;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null) 
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }

            var responseDictionary = await _myService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);

            var stock = new Stock()
            {
                StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
                HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowestPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString()),
            };

            return View(stock);
        }
    }
}
