using CoreLayer.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Components
{
    public class HeaderMenu
    {
        private readonly By _findMenuLinks = By.CssSelector("ul.top-navigation__row a[class*=\'top-navigation__item-link\']");
        protected WebDriverWrapper WebDriverWrapper { get; }

        public HeaderMenu(WebDriverWrapper webDriverWrapper)
        {
            WebDriverWrapper = webDriverWrapper;
            var menuLinks = webDriverWrapper.FindElements(_findMenuLinks);
        }

        public void ClickMenuLink(string linkText)
        {
            var menuLinks = WebDriverWrapper.FindElements(_findMenuLinks);
            foreach (var link in menuLinks)
            {
                if (link.Text.Trim().Equals(linkText, StringComparison.OrdinalIgnoreCase))
                {
                    link.Click();
                    return;
                }
            }
            throw new Exception($"Menu link with text '{linkText}' not found.");
        }
    }
}
