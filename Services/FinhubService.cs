using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using System.Net.Http;




namespace Services
{
    public class FinhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration) // install 2 packages Microsoft.Extensions.Configuration.Abstractions & Microsoft.Extensions.Http
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


    }
}
