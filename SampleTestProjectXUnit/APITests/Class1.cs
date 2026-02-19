using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using SampleTestProjectXUnit.Fixtures; // Чтобы видеть ApiEndpoints
using System.Buffers.Text;
using System.Net;
using Xunit;

namespace SampleTestProjectXUnit.ApiTests
{
   
    public class ProductApiTests : BaseApiTest
    {
    
      

        [Fact]
        public void Test_GetAllProducts_CleanCode()
        {
          

           
            var request = new RestRequest(ApiEndpoints.ProductsList, Method.Get);

            // 3. Выполняем
            var response = Client.Execute(request);

            // --- ЧТО ПОПРАВИТЬ В ПРОВЕРКАХ ---

            // Проверяем статус код
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Парсим тело (т.к. API возвращает объект с массивом внутри)
            var json = JObject.Parse(response.Content);

            // Проверяем, что в JSON есть поле "products" и это не пустой список
            Assert.True(json.ContainsKey("products"), "В ответе нет поля 'products'");
            var products = json["products"] as JArray;
            Assert.NotNull(products);
            Assert.NotEmpty(products);
        }

        [Fact]
        public void Test_PostAllProducts_Returns405()
        {
            
            var request = new RestRequest(ApiEndpoints.ProductsList, Method.Post);
            var response = Client.Execute(request);

            // 1. Проверяем, что сам запрос дошел успешно (сервер ответил 200)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // 2. Парсим JSON и проверяем бизнес-логику ошибки внутри
            var json = JObject.Parse(response.Content);

            // Обратите внимание: в этом API код ответа внутри JSON называется "responseCode"
            Assert.Equal(405, (int)json["responseCode"]);
            Assert.Equal("This request method is not supported.", json["message"]?.ToString());
        }
    }
}