using Microsoft.Extensions.Configuration;
using RestSharp;
using SampleTestProjectXUnit.Fixtures; // Чтобы видеть ApiEndpoints
using System.Net;
using Xunit;
using Newtonsoft.Json.Linq;

namespace SampleTestProjectXUnit.ApiTests
{
    public class ProductApiTests
    {
        private readonly IConfiguration _config;

        public ProductApiTests()
        {
            // Инициализируем конфиг
            _config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Fact]
        public void Test_GetAllProducts_CleanCode()
        {
            // 1. Берем базовый URL из конфига
            var baseUrl = _config["ApiUrl"];

            // 2. Настраиваем клиента и запрос
            var client = new RestClient(baseUrl);
            var request = new RestRequest(ApiEndpoints.ProductsList, Method.Get);

            // 3. Выполняем
            var response = client.Execute(request);

            // --- ЧТО ПОПРАВИТЬ В ПРОВЕРКАХ ---

            // Проверяем статус код
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Парсим тело (т.к. API возвращает объект с массивом внутри)
            var json = JObject.Parse(response.Content);

            // Проверяем, что в JSON есть поле "products" и это не пустой список
            Assert.True(json.ContainsKey("products"), "В ответе нет поля 'products'");
            var products = (JArray)json["products"];
            Assert.NotEmpty(products);
        }

        [Fact]
        public void Test_PostAllProducts_Returns405()
        {
            var baseUrl = _config["ApiUrl"];
            var client = new RestClient(baseUrl);

            // Используем Method.Post вместо Method.Get
            var request = new RestRequest(ApiEndpoints.ProductsList, Method.Post);

            var response = client.Execute(request);

            // Проверяем статус код 405 (но будьте внимательны с этим API, см. примечание ниже)
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);

            var json = JObject.Parse(response.Content);
            Assert.Equal("This request method is not supported.", json["message"]?.ToString());
        }
    }
}