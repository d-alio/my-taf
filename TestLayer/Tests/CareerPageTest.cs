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
    public class CareerPageTests : BaseTest
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


        [TestCase("Middle Automation Tester in Java")]
        public void ValidateThatTheUserCanSearchForPositionBasedOnCriteria(string textToFind)
        {
            Logger.Information("Starting the test 'ValidateThatTheUserCanSearchForPositionBasedOnCriteria'.");
            _homePage.Cookies.AcceptCookies();

            //Step 1: Click on 'Careers' menu button.
            Logger.Information("Step 1 started: Click on 'Careers' menu button.");
            _homePage.HeaderMenu.ClickMenuLink("Careers");
            Logger.Information("Passed: Click on 'Careers' menu button.");

            // Step 2: Click on 'Start your search' button
            Logger.Information("Step 2 started: Click on 'Start your search' button.");
            _careerPage.FindYourCareerButton.ClickFindCareerButton();
            Logger.Information("Passed: Click on 'Start your search' button.");

            //Step 3: Accept cookies because user is redirected to sub-domen careers.epam.com
            Logger.Information("Step 3 started: Accept cookies on careers.epam.com");
            _homePage.Cookies.AcceptCookies();
            Logger.Information("Passed: Accept cookies on careers.epam.com");


            //Select appropriate options for search

            //Step 4: Unselect locations
            Logger.Information("Step 4 started: Unselect locations");
            _careerPage.CareerSearch.DeselectLocations();
            Logger.Information("Passed: Unselect locations");

            //Step 5: Select 'Remote' option
            Logger.Information("Step 5 started: Select 'Remote' option");
            _careerPage.CareerSearch.SelectRemoteCareerOption();
            Logger.Information("Passed: Select 'Remote' option");

            //Step 6: Enter search keys
            Logger.Information("Step 6 started: Enter search keys");
            _careerPage.CareerSearch.EnterCareerSearchTextUsingActions(textToFind);
            Logger.Information("Passed: Enter search keys");

            //Step 7: Click on Search button
            Logger.Information("Step 7 started: Click on Search button");
            _careerPage.CareerSearch.ClickFindCareerButton();
            Logger.Information("Passed: Click on Search button");


            //Analyze search results

            //Step 8: Click on last career card
            Logger.Information("Step 8 started: Click on Last career card");
            _searchResultPage.SearchResults.ClickOnParticularCareerResultTitle(_searchResultPage.SearchResults.GetResultsTitles().Count - 1);
            Logger.Information("Passed: Click on Last career card");

            //Step 9: Verify that search key is present on the page
            Logger.Information("Step 9 started: Page from the last search card is loaded");
            var allElements = WebDriverWrapper.GetAllElements();
            Logger.Information($"Step 9: All elements are count. Number of elements on page is {allElements.Count()}");

            bool allContainText = false;
            for (int i = 0; i < allElements.Count(); i++)
            {
                if (allElements[i].Text.Contains(textToFind, StringComparison.OrdinalIgnoreCase))
                {
                    allContainText = true;
                    break;
                }
            }
            Assert.That(allContainText, Is.True, $"Not all elements contain '{textToFind}'");


            Logger.Information("Ending the test 'SearchOnIndexPageTest'.");
        }

    }
}