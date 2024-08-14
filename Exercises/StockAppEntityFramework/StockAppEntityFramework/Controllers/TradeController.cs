using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using Service_Contracts;
using Services.DTO;
using Services.Utilities;
using StockAppEntityFramework.Models;
using StockAppWithConfiguration;

namespace StockAppEntityFramework.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _options;
        private readonly IFinnService _finnhubService;
        private readonly IConfiguration _configuration;
        private readonly IStocksService _stocksService;

        public TradeController(IOptions<TradingOptions> options, IFinnService finnhubService, IConfiguration configuration, IStocksService stocksService)
        {
            _finnhubService = finnhubService;
            _options = options.Value;
            _configuration = configuration;
            _stocksService = stocksService;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            ViewData["token"] = _configuration["finnhubapi:token"];
            ViewBag.DefaultQuantity = _configuration.GetSection("TradingOptions")["DefaultOrderQuantity"];

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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            try
            {
                ModelValidator.Validate(buyOrderRequest);

                BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

                return RedirectToAction("orders", "trade");
            }
            catch (ArgumentException ex)
            {
                return Content($"Validation failed: \n{ex.Message}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            try
            {
                Console.WriteLine(sellOrderRequest.ToString());

                ModelValidator.Validate(sellOrderRequest);

                var sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

                return RedirectToAction("orders", "trade");
            }
            catch (ArgumentException ex)
            {
                return Content($"Validation failed: \n{ex.Message}");
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            var buyOrders = await _stocksService.GetBuyOrders();
            var sellOrders = await _stocksService.GetSellOrders();

            Orders orders = new Orders();
            orders.BuyOrders = buyOrders;
            orders.SellOrders = sellOrders;

            return View(orders);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            var buyOrders = await _stocksService.GetBuyOrders();
            var sellOrders = await _stocksService.GetSellOrders();

            var orders = new Orders();
            orders.BuyOrders = buyOrders;
            orders.SellOrders = sellOrders;

            // Return view as pdf
            return new ViewAsPdf("OrdersPDF", orders)
            {
                FileName = "OrdersReport.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };

        }
    }
}
