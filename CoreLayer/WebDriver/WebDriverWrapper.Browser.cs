using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreLayer.WebDriver.WebDriverFactory;

namespace CoreLayer.WebDriver
{//A partial class in C# allows you to split the definition of a class, struct, or interface across multiple files.
 //At compile time, all the parts are combined into a single class
 //This is especially useful for:
 //Large classes(for better organization)
 //Code generation tools(e.g., designer files in WinForms/WPF)
 //Collaborative development(multiple developers can work on the same class)
    public partial class WebDriverWrapper
    {
        private readonly TimeSpan _timeout;

        private readonly IWebDriver _driver;

        private const int WaitTimeInSeconds = 10;

        public WebDriverWrapper(BrowserType browserType)
        {
            _driver = WebDriverFactory.CreateWebDriver(browserType);
            _timeout = TimeSpan.FromSeconds(WaitTimeInSeconds);
        }

        public void StartBrowser(int implicitWaitTime = 10)
        {
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTime);

            //try add cookies
            //Cookie cookie1 = new Cookie("sa-user-id",);
            //_driver.Manage().Cookies.AddCookie;

        }


        public void Close()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void WindowMaximize()
        {
            _driver.Manage().Window.Maximize();
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string GetUrl()
        {
            return _driver.Url;
        }
    }
}
