using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace StockApp.Controllers
{
    [Route("[controller]")] // /trade will always start with trade 
    public class TradeController : Controller
    {

     
        public TradeController()
        {
            
        }

        public IActionResult Index(IOptions<TradingOptions> tradingOptions, )
        {
            return View();
        }
    }
}
