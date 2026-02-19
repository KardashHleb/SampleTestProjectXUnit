using Microsoft.Extensions.Configuration;
using RestSharp;
using System.IO;

namespace SampleTestProjectXUnit.ApiTests
{
    public abstract class BaseApiTest
    {
        protected readonly IConfiguration Config;
        protected readonly RestClient Client;

        protected BaseApiTest()
        {
            // 1. Настраиваем чтение конфига (один раз для всех)
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 2. Инициализируем клиент
            var baseUrl = Config["ApiUrl"];
            Client = new RestClient(baseUrl);
        }
    }
}