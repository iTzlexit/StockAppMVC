using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Services;
using StockApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace StockApp.Controllers
{
    [Route("[controller]")] // /trade will always start with trade 
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinhubService _finnhubService;
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Constructor for TradeController that executes when a new object is created for the class
        /// </summary>
        /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
        /// <param name="stocksService">Injecting StocksService</param>
        /// <param name="finnhubService">Injecting FinnhubService</param>
        /// <param name="configuration">Injecting IConfiguration</param>
        public TradeController(IOptions<TradingOptions> tradingOptions, IFinhubService finnhubService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _configuration = configuration;
        }


        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public IActionResult Index()
        {
            // Reset stock symbol if not exists
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";

            // Get company profile from API server
            Dictionary<string, object>? companyProfileDictionary = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            // Get stock price quotes from API server
            Dictionary<string, object>? stockQuoteDictionary = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            // Create model object
            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

            // Load data from finnHubService into model object
            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                if (stockQuoteDictionary.ContainsKey("c") && stockQuoteDictionary["c"] != null)
                {
                    if (stockQuoteDictionary["c"] is JsonElement cElement && cElement.ValueKind == JsonValueKind.Number)
                    {
                        stockTrade = new StockTrade()
                        {
                            StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]),
                            StockName = Convert.ToString(companyProfileDictionary["name"]),
                            Price = cElement.GetDouble()
                        };
                    }
                    else
                    {
                        // Handle the case where the value is not a number.
                    }
                }
                else
                {
                    // Handle the case where "c" key is missing or its value is null.
                }
            }

            // Send Finnhub token to view
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }


    }
}
