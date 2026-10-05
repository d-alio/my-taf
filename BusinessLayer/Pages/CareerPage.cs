using BusinessLayer.Components;
using CoreLayer.WebDriver;
using OpenQA.Selenium;

namespace BusinessLayer.Pages
{
    public class CareerPage : BasePage
    {
        private readonly By _findStartYourSearchButton = By.CssSelector("a[href='https://careers.epam.com/en/jobs/?utm_medium=internal&utm_campaign=ta&utm_source=www.epam.com&utm_term=start-your-search-here&utm_content=job-search']");
        public SearchPanel SearchPanel { get; }
        public Header Header { get; }
        public CookiesPopup Cookies { get; }
        public HeaderMenu HeaderMenu { get; }
        //public FindYourCareerButton FindYourCareerButton { get; }
        public CareerSearchBlock CareerSearchBlock { get; } 


        public CareerPage(WebDriverWrapper webDriverWrapper) : base(webDriverWrapper)
        {
            SearchPanel = new SearchPanel(webDriverWrapper);
            Header = new Header(webDriverWrapper);
            Cookies = new CookiesPopup(webDriverWrapper);
            HeaderMenu = new HeaderMenu(webDriverWrapper);
            
            CareerSearchBlock = new CareerSearchBlock(webDriverWrapper);
        }


        public void ClickFindCareerButton()
        {
            WebDriverWrapper.Click(_findStartYourSearchButton);
        }
        public void WaitForFindCareerButton(int waitForSeconds)
        {
            WebDriverWrapper.WaitForElementToBePresent(_findStartYourSearchButton, TimeSpan.FromSeconds(waitForSeconds));
        }


        public void WaitForCareerUrl(string careerUrlPart)
        {
            WebDriverWrapper.WaitForUrl(careerUrlPart, true);
        }
    }
}

