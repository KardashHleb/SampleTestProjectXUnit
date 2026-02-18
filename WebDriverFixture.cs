using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SampleTestProjectXUnit.Fixtures
{
    // Реализуем IDisposable, чтобы браузер закрывался сам после тестов
    public class WebDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public WebDriverFixture()
        {
            // 1. Настройка опций (опционально, но полезно)
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized"); // Открывать на весь экран
            options.AddArgument("--disable-notifications"); // Отключить пуши

            // 2. Инициализация самого драйвера
            // ChromeDriver сам найдет путь к браузеру, если пакет установлен
            Driver = new ChromeDriver(options);
        }

        public void Dispose()
        {
            // Метод Dispose вызывается xUnit автоматически в конце
            if (Driver != null)
            {
                Driver.Quit(); // Закрывает все окна и процесс драйвера
                Driver.Dispose();
            }
        }
    }
}