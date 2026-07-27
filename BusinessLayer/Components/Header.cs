using CoreLayer.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Components
{

    public class Header
    {
        private readonly By _searchIcon = By.ClassName("header-search__button");
        protected WebDriverWrapper WebDriverWrapper { get; }
        public Header(WebDriverWrapper webDriverWrapper)
        {
            WebDriverWrapper = webDriverWrapper;
        }
        public SearchPanel ClickSearchIcon()
        {
            WebDriverWrapper.Click(_searchIcon);
            return new SearchPanel(WebDriverWrapper);
        }
    }

}
