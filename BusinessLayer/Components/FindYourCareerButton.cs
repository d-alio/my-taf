using CoreLayer.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Components
{
    public class FindYourCareerButton
    {
        public readonly By _findStartYourSearchButton = By.CssSelector("a[href='https://careers.epam.com/en/jobs/?utm_medium=internal&utm_campaign=ta&utm_source=www.epam.com&utm_term=start-your-search-here&utm_content=job-search']");

        protected WebDriverWrapper WebDriverWrapper { get; }

        public FindYourCareerButton (WebDriverWrapper webDriverWrapper)
        {
            WebDriverWrapper = webDriverWrapper;
        }

        public void ClickFindCareerButton()
        {
            WebDriverWrapper.Click(_findStartYourSearchButton);
        }

    }
}
