namespace ServiceContracts
{
    public interface IFinhubService
    {
        Dictionary<string, object>? GetCompanyProfile(string stockSymbol);

        public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol); 
    }
}