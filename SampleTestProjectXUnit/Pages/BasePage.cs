using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTestProjectXUnit.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver, int timeoutSeconds)
        {
            Driver = driver;
            // Создаем один экземпляр ожидания для всей страницы
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        // Пример универсального метода ожидания внутри BasePage
        protected IWebElement WaitForElement(By locator)
        {
            return Wait.Until(d => d.FindElement(locator));
        }
    }
}
