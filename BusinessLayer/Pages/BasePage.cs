using CoreLayer.WebDriver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Pages
{
    public abstract class BasePage
    {
        protected WebDriverWrapper WebDriverWrapper { get; }

        protected BasePage(WebDriverWrapper webDriverWrapper) => WebDriverWrapper = webDriverWrapper;
    }
}
