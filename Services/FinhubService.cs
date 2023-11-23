using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using System.Net.Http;
using ServiceContracts;
using System.Text.Json;




namespace Services
{
    public class FinhubService: IFinhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration) // install 2 packages Microsoft.Extensions.Configuration.Abstractions & Microsoft.Extensions.Http
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Dictionary<string, object>? GetCompanyProfile(string stockSymbol) 
        {
            //create the http client

            HttpClient httpClient = _httpClientFactory.CreateClient();

            //create the http request

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}") //URI includes the secret token
            };

            //send the request
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage );

            //read the response body
            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            //convert response body (from JSON into Dictionary)
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            //return response dictionary back to the caller
            return responseDictionary;


        }

        public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol)
        {
            //create http client
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //create http request
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}") //URI includes the secret token
            };

            //send request
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            //read response body
            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            //convert response body (from JSON into Dictionary)
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            //return response dictionary back to the caller
            return responseDictionary;
        }



        /*
        User Secrets:
        dotnet user-secrets init --project StockApp
        dotnet user-secrets set "FinnhubToken" "clf3pj1r01qoveppj5rgclf3pj1r01qoveppj5s0" --project StockApp
        */


    }


}
