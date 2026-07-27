using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace CoreLayer.WebDriver;

public static class WebDriverFactory
{
    public static IWebDriver CreateWebDriver(BrowserType browserType)
    {
        switch (browserType)
        {
            case BrowserType.Chrome:
                {
                    var service = ChromeDriverService.CreateDefaultService();
                    ChromeOptions options = new();
                    options.AddArgument("disable-infobars");
                    options.AddArgument("--incognito");

                    return new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
                }
            case BrowserType.Edge:
                return new EdgeDriver();
            case BrowserType.Firefox:
                return new FirefoxDriver();
            default:
                throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
        }
    }
}

public enum BrowserType
{
    Chrome,
    Edge,
    Firefox
}