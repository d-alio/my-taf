using CoreLayer.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Components
{
    public class SearchPanel
    {
        private readonly By _searchPanel = By.ClassName("header-search__panel");

        private readonly By _findButton = By.XPath(".//*[@class='search-results__input-holder']/following-sibling::button");
        protected WebDriverWrapper WebDriverWrapper { get; }

        public SearchPanel(WebDriverWrapper webDriverWrapper)
        {
            WebDriverWrapper = webDriverWrapper;
        }

        public SearchPanel EnterSearchTextUsingActions(string text)
        {
            var searchInput = WebDriverWrapper.FindChildByName(_searchPanel, "q");
            WebDriverWrapper.ClickAndSendAction(searchInput, text);
            return this;
        }

        public void ClickFindButton()
        {
            WebDriverWrapper.Click(_findButton);
        }
    }
}
