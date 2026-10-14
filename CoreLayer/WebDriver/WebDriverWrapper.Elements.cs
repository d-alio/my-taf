using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace CoreLayer.WebDriver
{
    public partial class WebDriverWrapper
    {
        public void Click(By by)
        {
            _waiters.WaitForElementToBePresent(by, _timeout)?.Click();
            //WaitForElementToBecomeClickable(by);
            //var element = _driver.FindElement(by);
            //element.Click();

        }

        public void EnterText(By by, string text)
        {
            var element = _waiters.WaitForElementToBePresent( by, _timeout);
            element.Clear();
            element.SendKeys(text);
        }
        public IWebElement FindChildByName(By byParent, string childName)
        {
            var elementParent = _waiters.WaitForElementToBePresent( byParent, _timeout);
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
    }
}

