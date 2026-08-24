using BusinessLayer.Components;
using CoreLayer.WebDriver;
using OpenQA.Selenium;

namespace BusinessLayer.Pages
{
    public class CareerPage : BasePage
    {
        public SearchPanel SearchPanel { get; }
        public Header Header { get; }
        public CookiesPopup Cookies { get; }
        public HeaderMenu HeaderMenu { get; }
        public FindYourCareerButton FindYourCareerButton { get; }
        public CareerSearchBlock CareerSearch { get; } 


        public CareerPage(WebDriverWrapper webDriverWrapper) : base(webDriverWrapper)
        {
            SearchPanel = new SearchPanel(webDriverWrapper);
            Header = new Header(webDriverWrapper);
            Cookies = new CookiesPopup(webDriverWrapper);
            HeaderMenu = new HeaderMenu(webDriverWrapper);
            FindYourCareerButton = new FindYourCareerButton(webDriverWrapper);
            CareerSearch = new CareerSearchBlock(webDriverWrapper);
        }


    }
}

