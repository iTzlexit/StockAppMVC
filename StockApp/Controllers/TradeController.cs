using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockApp.Models;
using System.Diagnostics;

namespace StockApp.Controllers
{
    [Route("[controller]")] // /trade will always start with trade 
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinhubService _finhubService;
        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> tradingOptions, IFinhubService finhubService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _finhubService = finhubService;
            _configuration = configuration;
        }


        [Route("/")]
        [Route("[action]")] // route will be trade/index 
        [Route("~/[controller]")]
        public IActionResult Index()
        {
            //reset stock symbol if it isnt present 

            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";


            //get company profile from api server 

            Dictionary<string, object>? companyProfileDictionary = _finhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            //Get stock price quotes from Api Server 

            Dictionary<string, object>? stockQuoteDictionary = _finhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            //create model object
            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

            //load data from finnHubService into model object
            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade() { StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]), StockName = Convert.ToString(companyProfileDictionary["name"]), Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
            }

            //Send Finnhub token to view
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }
    }
}
