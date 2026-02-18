using OpenQA.Selenium;
using SampleTestProjectXUnit.Pages;

public class HomePage : BasePage
{
    private readonly string _url;

    public HomePage(IWebDriver driver, int timeout, string baseUrl) : base(driver, timeout)
    {
        _url = baseUrl; // Получаем URL из теста, который взял его из конфига
    }

    public void Open()
    {
        Driver.Navigate().GoToUrl(_url);
    }
}