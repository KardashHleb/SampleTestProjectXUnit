using Microsoft.Extensions.Configuration;
using Xunit;
using OpenQA.Selenium;
using SampleTestProjectXUnit.Fixtures;

namespace SampleTestProjectXUnit.UITests
{
    // 1. Добавляем интерфейс IClassFixture, чтобы xUnit передал нам драйвер
    public class PageAvailabilityTests : IClassFixture<WebDriverFixture>
    {
        private readonly IConfiguration _config;
        private readonly IWebDriver _driver;
        private readonly string _url;

        public PageAvailabilityTests(WebDriverFixture fixture)
        {
            _driver = fixture.Driver;

            // 1. Конфигурацию читаем один раз при создании класса
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _url = config["BaseUrl"];
        }

        [Fact]
        public void Test_Url_Is_Correct()
        {
            

            // 4. КОМАНДА: Перейти на сайт
            _driver.Navigate().GoToUrl(_url);

            // 5. ПРОВЕРКА: Проверяем, что заголовок страницы содержит нужное слово
            // (Это и есть проверка того, что мы зашли куда нужно)
            Assert.Contains("Automation Exercise", _driver.Title);
        }

        [Fact]
        public void Test_Header_Logo_Is_Visible()
        {
           
            _driver.Navigate().GoToUrl(_url);
          

            // Проверяем наличие хедера
            var logo = _driver.FindElement(By.Id("header"));
            Assert.True(logo.Displayed, "Логотип в хедере должен быть видимым");
        }
    }
}