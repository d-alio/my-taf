using BusinessLayer.Components;
using CoreLayer.WebDriver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Pages
{
    public class ArticlePage : BasePage
    {
        public Header Header { get; }

        public ArticlePage(WebDriverWrapper webDriverWrapper) : base(webDriverWrapper)
        {
            Header = new Header(webDriverWrapper);
        }
    }
}
