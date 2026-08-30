using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoreLayer.WebDriver
{
    public partial class WebDriverWrapper
    {
        public void Click(By by)
        {
            WaitForElementToBePresent(_driver, by, _timeout)?.Click();
        }

        public void EnterText(By by, string text)
        {
            var element = WaitForElementToBePresent(_driver, by, _timeout);
            element.Clear();
            element.SendKeys(text);
        }
        public IWebElement FindChildByName(By byParent, string childName)
        {
            var elementParent = WaitForElementToBePresent(_driver, byParent, _timeout);
            return elementParent.FindElement(By.Name(childName));
        }
        public void ClickAndSendAction(IWebElement element, string textToSend)
        {
            var clickAndSendKeysActions = new Actions(_driver);
            clickAndSendKeysActions.Click(element)
                .Pause(TimeSpan.FromSeconds(1))
                .SendKeys(textToSend)
                .Perform();
        }

        public void ClickAndSendBackspace(IWebElement element)
        {
            var actions = new Actions(_driver);
            actions.Click(element)
                   .Pause(TimeSpan.FromSeconds(1))
                   .SendKeys(Keys.Backspace)
                   .Perform();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _driver.FindElements(by);
        }

        public IWebElement FindElement(By by)
        {
            return _driver.FindElement(by);
        }

        public IList<IWebElement> GetAllElements()
        {
            return _driver.FindElements(By.CssSelector("*"));
        }

        public IWebElement WaitForElementToBePresent(IWebDriver Driver, By by, TimeSpan _timeout)
        {
            var wait = new WebDriverWait(Driver, _timeout);
            return wait.Until(drv =>
            {
                try
                {
                    var element = drv.FindElement(by);
                    if (element != null && element.Displayed)
                        return element;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("WaitForElementToBePresent method: 'NoSuchElementException' is found.");
                }
                return null;
            });
        }

        //RE-WRITE this
        //public void WaitForElementToBeInvisible(IWebDriver Driver, By by, TimeSpan _timeout)
        //{
        //    var wait = new WebDriverWait(Driver, _timeout);
        //    wait.Until(drv =>
        //    {
        //        try
        //        {
        //            var element = drv.FindElement(by);
        //            if (element == null && !element.Displayed) ;
        //        }
        //        catch (NoSuchElementException)
        //        {
        //            Console.WriteLine("WaitForElementToBeInvisible method: 'Element' is displayed.");
        //        }
        //    });
        //}

        public void WaitForElementToBeInvisible(IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);

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
    }
}

