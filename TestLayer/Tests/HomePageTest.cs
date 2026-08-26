using BusinessLayer.Pages;
using CoreLayer;
using Newtonsoft.Json;
using NUnit.Framework;
using SearchModel = TestLayer.Data.SearchModel;
//Test comment

namespace TestLayer.Tests
{
    [TestFixture]
    public class HomePageTests : BaseTest
    {
        private HomePage _homePage;
        private SearchResultPage _searchResultPage;
        private CareerPage _careerPage;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _homePage = new HomePage(WebDriverWrapper);
            _searchResultPage = new SearchResultPage(WebDriverWrapper);
            _careerPage = new CareerPage(WebDriverWrapper);
        }

        [TestCase("dotnet", "search?q=dotnet")]
        public void SearchOnIndexPageTest(string textToFind, string searchUrl)
        {
            Logger.Information("Starting the test 'SearchOnIndexPageTest'.");

            //Step 1: Accept cookies pop up
            Logger.Information("Step 1 started: Accept cookies");
            _homePage.Cookies.AcceptCookies();
            Logger.Information("Passed: Cookies accepted");

            //Step 2: Click on Search icon
            Logger.Information("Step 2 started: Click on Search icon");
            var searchPanel = _homePage.Header.ClickSearchIcon();
            Logger.Information("Passed: Click on Search icon");

            //Step 3: Enter keys received as test parameter and click on Find button
            Logger.Information($"Step 3 started: Enter keys '{textToFind}' received as test parameter and click on Find button");
            searchPanel.EnterSearchTextUsingActions(textToFind)
                .ClickFindButton();
            Logger.Information($"Passed: Keys '{textToFind}' received as test parameter and Find button clicked");

            //Step 4: Check that URL with results contains the entered keys parameter
            Logger.Information($"Step 4 started: Check that URL with results contains the entered keys parameter '{textToFind}'");
            var currentUrl = WebDriverWrapper.GetUrl();
            Assert.That(currentUrl, Does.Contain(searchUrl));
            Logger.Information($"Passed: URL with results contains the entered keys parameter '{textToFind}'");

            //Get all the results elements
            var results = _searchResultPage.SearchResults.GetResults();
            Logger.Information($"Search results have been received. {results.Count()} page card(s) is/are displayed on the page.");

            //Step 5: Check that results contain the entered keys parameter
            bool allContainText = true;
            foreach (var result in results)
            {
                if (!result.Text.Contains(textToFind, StringComparison.OrdinalIgnoreCase))
                {
                    allContainText = false;
                    break;
                }
            }
            Assert.That(allContainText, Is.True, $"Not all search results contain '{textToFind}'");

            Logger.Information("Ending the test 'SearchOnIndexPageTest'.");
        }


        
        [TestCaseSource(nameof(SearchModelData))]
        public void SearchOnIndexPageTest_JSON(SearchModel searchModelItems)
        {
            Logger.Information("Starting the test 'SearchOnIndexPageTest_JSON'.");

            //Step 1: Accept cookies pop up
            Logger.Information("Step 1 started: Accept cookies");
            _homePage.Cookies.AcceptCookies();
            Logger.Information("Passed: Cookies accepted");

            //Step 2: Click on Search icon
            Logger.Information("Step 2 started: Click on Search icon");
            var searchPanel = _homePage.Header.ClickSearchIcon();
            Logger.Information("Passed: Click on Search icon");

            //Step 3: Enter keys received as test parameter and click on Find button
            Logger.Information($"Step 3 started: Enter keys from the file '{searchModelItems.TextToSearch}' as test parameter and click on Find button");
            searchPanel.EnterSearchTextUsingActions(searchModelItems.TextToSearch)
                .ClickFindButton();
            Logger.Information($"Passed: Keys '{searchModelItems.TextToSearch}' received as test parameter and Find button clicked");

            //Step 4: Check that URL with results contains the entered keys parameter
            Logger.Information($"Step 4 started: Check that URL with results contains the entered keys parameter '{searchModelItems.TextToSearch}'");
            var currentUrl = WebDriverWrapper.GetUrl();
            Assert.That(currentUrl, Does.Contain(searchModelItems.SearchUrl));
            Logger.Information($"Passed: URL with results contains the entered keys parameter '{searchModelItems.TextToSearch}'");

            //Get all the results elements
            var results = _searchResultPage.SearchResults.GetResults();
            Logger.Information($"Search results have been received. {results.Count()} page card(s) is/are displayed on the page.");

            //Step 5: Check that results contain the entered keys parameter
            bool allContainText = true;
            foreach (var result in results)
            {
                if (!result.Text.Contains(searchModelItems.TextToSearch, StringComparison.OrdinalIgnoreCase))
                {
                    allContainText = false;
                    break;
                }
            }
            Assert.That(allContainText, Is.True, $"Not all search results contain '{searchModelItems.TextToSearch}'");


            Logger.Information("Ending the test 'SearchOnIndexPageTest_JSON'.");
        }


        //[TestCaseSource(nameof(SearchModelData))]
        //public void SearchOnIndexPageTest_Updated_WithJsonData(SearchModel searchModelItem)
        //{
        //    Logger.Information("Starting the test 'SearchOnIndexPageTest_Updated_WithJsonData'.");

        //    var searchPanel = _homePage.Header.ClickSearchIcon();

        //    searchPanel.EnterSearchTextUsingActions(searchModelItem.TextToSearch)
        //        .ClickFindButton();

        //    var currentUrl = WebDriverWrapper.GetUrl();
        //    Assert.That(currentUrl, Does.Contain(searchModelItem.SearchUrl));

        //    Logger.Information("Ending the test 'SearchOnIndexPageTest_Updated_WithJsonData'.");
        //}

        private static List<SearchModel> SearchModelData
        {
            get
            {
                var jsonData = File.ReadAllText(Configuration.TestDataPath);
                var searchModelItems = JsonConvert.DeserializeObject<List<SearchModel>>(jsonData);
                return searchModelItems;
            }
        }
    }
}