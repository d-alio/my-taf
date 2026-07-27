using BusinessLayer.Components;
using CoreLayer.WebDriver;
using OpenQA.Selenium;

namespace BusinessLayer.Pages
{
    public class HomePage : BasePage
    {
        public SearchPanel SearchPanel { get; }
        public Header Header { get; }
        public HomePage(WebDriverWrapper webDriverWrapper) : base(webDriverWrapper)
        {
            SearchPanel = new SearchPanel(webDriverWrapper);
            Header = new Header(webDriverWrapper);
        }


    }
}

