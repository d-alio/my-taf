using CoreLayer.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace BusinessLayer.Components
{
    public class SearchResultsSection
    {
        // private readonly By _resultsPaginationItems = By.CssSelector("[data-event-content='page_pagination']");
        // private readonly By _resultsCounter = By.CssSelector("h2[class='search-results__counter']");
        private readonly By _searchResults = By.ClassName("search-results__item");
        private readonly By _findCareerResultCardTitle = By.CssSelector("[data-event-content*='title']");
        private readonly By _emptySearchResultMessage = By.XPath("//div[contains(@class, 'search-results--empty-result') and contains(text(), 'no results')]");

        protected WebDriverWrapper WebDriverWrapper { get; }
        
        public SearchResultsSection (WebDriverWrapper webDriverWrapper)
        {
            WebDriverWrapper = webDriverWrapper;
            var resultItems = webDriverWrapper.FindElements(_searchResults);
        }

        public ReadOnlyCollection<IWebElement> GetResults()
        {
            return WebDriverWrapper.FindElements(_searchResults);
        }

        public ReadOnlyCollection<IWebElement> GetResultsTitles()
        {
            return WebDriverWrapper.FindElements(_findCareerResultCardTitle);
        }

        public void ClickOnParticularCareerResultTitle(int orderNumber)
        {
            var CareerResultsTitleList = GetResultsTitles();

            if ((orderNumber < 0) || (orderNumber >= CareerResultsTitleList.Count))
            {
                throw new ArgumentOutOfRangeException("No element with such index");
            }
            else
            {
                CareerResultsTitleList[orderNumber].Click();
            }
            return;
        }

        public IWebElement NoResultExceptionMessage()
        {
            return WebDriverWrapper.FindElement(_emptySearchResultMessage);
        }
    }
}
