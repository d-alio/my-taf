using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLayer.WebDriver
{
    public partial class WebDriverWrapper
    {
        public IWebElement WaitForElementToBePresent(By by, TimeSpan _timeout)
        {
            var wait = new WebDriverWait(_driver, _timeout);
            return wait.Until(drv =>
            {
                try
                {
                    var element = drv.FindElement(by);
                    if (element != null && element.Displayed)
                        return element;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }

        public void WaitForElementToBeInvisible(By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(_driver, timeout);

            try
            {
                wait.Until(drv =>
                {
                    try
                    {
                        var element = drv.FindElement(by);
                        return !element.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"WaitForElementToBeInvisible method: Element located by '{by}' is still displayed after {timeout.TotalSeconds} seconds.");
                throw;
            }
        }

        public void WaitUntilCondition(Func<bool> condition)
        {
            var wait = new WebDriverWait(_driver, _timeout);
            wait.Until(_ => condition());
        }

        public void WaitForUrl(string urlPart, bool contains)
        {
            WaitUntilCondition(() => GetUrl().Contains(urlPart) == contains);
        }

        public void WaitForElementToBecomeClickable(By by)
        {
            WaitUntilCondition(() =>
            {
                var element = _driver.FindElements(by).FirstOrDefault();
                return element != null && element.Displayed && element.Enabled;
            });
        }

        public void ImplicitWaiter (int implicitWaitTime = 3)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTime);
        }
    }
}
