using BusinessLayer.Components;
using CoreLayer.WebDriver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Pages
{
    public class SearchResultPage : BasePage
    {
        public Header Header { get; }
        public SearchResultsSection SearchResults { get; }

        public SearchResultPage(WebDriverWrapper webDriverWrapper) : base(webDriverWrapper)
        {
            Header = new Header(webDriverWrapper);
            SearchResults = new SearchResultsSection(webDriverWrapper);
        }
    }
}
