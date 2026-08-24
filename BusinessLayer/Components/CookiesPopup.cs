using CoreLayer.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Components
{
    public class CookiesPopup
    {
        private readonly By _searchCookiesPopup = By.CssSelector("[role='dialog'] .ot-sdk-row");

        private readonly By _findAcceptButton = By.CssSelector("div[id='onetrust-button-group'] button[id='onetrust-accept-btn-handler']");

        protected WebDriverWrapper WebDriverWrapper { get; }

        public CookiesPopup(WebDriverWrapper webDriverWrapper)
        {
            WebDriverWrapper = webDriverWrapper;
        }

        public void AcceptCookies()
        {
            WebDriverWrapper.Click(_findAcceptButton);
            //WebDriverWrapper.
        }

    }
}
