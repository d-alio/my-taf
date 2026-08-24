using BusinessLayer.Pages;
using CoreLayer;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SearchModel = TestLayer.Data.SearchModel;
//Test comment

namespace TestLayer.Tests
{
    [TestFixture]
    public class HomeTests : BaseTest
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

        [TestCase("Automation", "search?q=Automation")]
        public void SearchOnIndexPageTest(string textToFind, string searchUrl)
        {
            Logger.Information("Starting the test 'SearchOnIndexPageTest_Updated'.");
            _homePage.Cookies.AcceptCookies();

            var searchPanel = _homePage.Header.ClickSearchIcon();

            searchPanel.EnterSearchTextUsingActions(textToFind)
                .ClickFindButton();

            var currentUrl = WebDriverWrapper.GetUrl();
            Assert.That(currentUrl, Does.Contain(searchUrl));


            var results = _searchResultPage.SearchResults.GetResults();

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


        [TestCase("Middle Automation Tester in Java")]
        public void ValidateThatTheUserCanSearchForPositionBasedOnCriteria(string textToFind)
        {
            Logger.Information("Starting the test 'ValidateThatTheUserCanSearchForPositionBasedOnCriteria'.");
            _homePage.Cookies.AcceptCookies();

            _homePage.HeaderMenu.ClickMenuLink("Careers");

            // click on 'Start your search' button
            _careerPage.FindYourCareerButton.ClickFindCareerButton();

            //accept cookies because user is redirected to sub-domen careers.epam.com
            _homePage.Cookies.AcceptCookies();

            //Select appropriate options for search
            _careerPage.CareerSearch.DeselectLocations();
            _careerPage.CareerSearch.SelectRemoteCareerOption();
            _careerPage.CareerSearch.EnterCareerSearchTextUsingActions(textToFind);
            _careerPage.CareerSearch.ClickFindCareerButton();


            //Analyze search results
            //Click on last career card
            _searchResultPage.SearchResults.ClickOnParticularCareerResultTitle(_searchResultPage.SearchResults.GetResultsTitles().Count - 1);

            var allElements = WebDriverWrapper.GetAllElements();
            bool allContainText = true;
            foreach (IWebElement element in allElements)
            {
                if (!element.Text.Contains(textToFind, StringComparison.OrdinalIgnoreCase))
                {
                    allContainText = false;
                    break;
                }
            }
            Assert.That(allContainText, Is.True, $"Not all elements contain '{textToFind}'");


            Logger.Information("Ending the test 'SearchOnIndexPageTest'.");
        }







        [TestCaseSource(nameof(SearchModelData))]
        public void SearchOnIndexPageTest_JSON(SearchModel searchModelItems)
        {
            Logger.Information("Starting the test 'SearchOnIndexPageTest_JSON'.");
            _homePage.Cookies.AcceptCookies();

            var searchPanel = _homePage.Header.ClickSearchIcon();

            searchPanel.EnterSearchTextUsingActions(searchModelItems.TextToSearch)
                .ClickFindButton();

            var currentUrl = WebDriverWrapper.GetUrl();
            Assert.That(currentUrl, Does.Contain(searchModelItems.SearchUrl));


            var results = _searchResultPage.SearchResults.GetResults();

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






        [TestCaseSource(nameof(SearchModelData))]
        public void SearchOnIndexPageTest_Updated_WithJsonData(SearchModel searchModelItem)
        {
            Logger.Information("Starting the test 'SearchOnIndexPageTest_WithJson'.");

            var searchPanel = _homePage.Header.ClickSearchIcon();

            searchPanel.EnterSearchTextUsingActions(searchModelItem.TextToSearch)
                .ClickFindButton();

            var currentUrl = WebDriverWrapper.GetUrl();
            Assert.That(currentUrl, Does.Contain(searchModelItem.SearchUrl));

            Logger.Information("Ending the test 'SearchOnIndexPageTest_Updated'.");
        }

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