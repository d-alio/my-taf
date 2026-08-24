using CoreLayer.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BusinessLayer.Components
{
    public class CareerSearchBlock
    {
        private readonly By _findRemoteOptionCheckbox = By.CssSelector("[class*='Filter_filterItem'] label[for*='checkbox-vacancy_type-Remote']");
        private readonly By _findCareerSearchButton = By.Name("submit_search_box_button");
        private readonly By _findCareerSearchInput = By.CssSelector("input[placeholder='Search by Role or Keyword']");
        private readonly By _findLocationInput = By.CssSelector("input[aria-label='Choose your country']");

        protected WebDriverWrapper WebDriverWrapper { get; }

        public CareerSearchBlock(WebDriverWrapper webDriverWrapper)
        {
            WebDriverWrapper = webDriverWrapper;
        }

        public CareerSearchBlock EnterCareerSearchTextUsingActions(string text)
        {
            var searchInput = WebDriverWrapper.FindElement(_findCareerSearchInput);
            WebDriverWrapper.ClickAndSendAction(searchInput, text);
            return this;
        }

        public void ClickFindCareerButton()
        {
            WebDriverWrapper.Click(_findCareerSearchButton);
        }

        //public void DeselectLocations()
        //{
        //  WebDriverWrapper.Click(_deselectLocationsIcon.Enabled);
        //}

        public void DeselectLocations()
        {
            var locationInput = WebDriverWrapper.FindElement(_findLocationInput);
            WebDriverWrapper.ClickAndSendBackspace(locationInput);
        }

        public void SelectRemoteCareerOption()
        {
            WebDriverWrapper.FindElement(_findRemoteOptionCheckbox).Click();
            return;
        }
    }
}
