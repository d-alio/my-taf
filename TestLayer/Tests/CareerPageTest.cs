using BusinessLayer.Components;
using BusinessLayer.Pages;
using CoreLayer;
using NUnit.Framework;
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
        private CareerSearchBlock _CareerSearchBlock;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _homePage = new HomePage(WebDriverWrapper);
            _searchResultPage = new SearchResultPage(WebDriverWrapper);
            _careerPage = new CareerPage(WebDriverWrapper);
            _CareerSearchBlock = new CareerSearchBlock(WebDriverWrapper);
        }

        //Please note that Key "Middle Automation Tester in Java" leads to failed testcase

        [TestCase("Tester in JS")]
        public void ValidateThatTheUserCanSearchForPositionBasedOnCriteria(string textToFind)
        {
            Logger.Information("Starting the test 'ValidateThatTheUserCanSearchForPositionBasedOnCriteria'.");
            _homePage.Cookies.AcceptCookies();

            //Step 1: Click on 'Careers' menu button.
            Logger.Information("Step 1 started: Click on 'Careers' menu button.");
            _homePage.HeaderMenu.ClickMenuLink("Careers");
            _careerPage.WaitForCareerUrl("/careers");
            Logger.Information("Passed: 'Careers' page is opened.");

            // Step 2: Click on 'Start your search' button
            Logger.Information("Step 2 started: Click on 'Start your search' button.");
            _careerPage.WaitForFindCareerButton(10);
            _careerPage.ClickFindCareerButton();
            Logger.Information("Passed: Click on 'Start your search' button.");

            //Step 3: Accept cookies because user is redirected to sub-domen careers.epam.com
            Logger.Information("Step 3 started: Accept cookies on careers.epam.com");
            _careerPage.WaitForCareerUrl("/careers.epam.com");
            Logger.Information("Passed: Careers.epam.com is opened");
            _homePage.Cookies.AcceptCookies();
            Logger.Information("Passed: Accept cookies on careers.epam.com");


            //Select appropriate options for search

            //Step 4: Unselect locations
            Logger.Information("Step 4 started: Unselect locations");
            _careerPage.CareerSearchBlock.DeselectLocations();
            _careerPage.WaitForCareerUrl("/jobs?_gl=");
            Logger.Information("Passed: Unselect locations");

            //Step 5: Select 'Remote' option
            Logger.Information("Step 5 started: Select 'Remote' option");
            _careerPage.CareerSearchBlock.SelectRemoteCareerOption();
            _careerPage.WaitForCareerUrl("vacancy_type=remote");
            Logger.Information("Passed: Select 'Remote' option");

            //Step 6: Enter search keys
            Logger.Information("Step 6 started: Enter search keys");
            _careerPage.CareerSearchBlock.EnterCareerSearchTextUsingActions(textToFind);
            Logger.Information("Passed: Enter search keys");

            //Step 7: Click on Search button
            Logger.Information("Step 7 started: Click on Search button");
            _careerPage.CareerSearchBlock.ClickFindCareerButton();
            Logger.Information("Passed: Click on Search button");


            //Analyze search results

            //Step 8: Click on last career card
            Logger.Information("Step 8 started: Click on Last career card");
            _careerPage.WaitForCareerUrl("&search=");
            _searchResultPage.SearchResults.ClickOnParticularCareerResultTitle(_searchResultPage.SearchResults.GetResultsTitles().Count - 1);
            Logger.Information("Passed: Click on Last career card");

            //Step 9: Verify that search key is present on the page
            _careerPage.WaitForCareerUrl("/vacancy");
            Logger.Information("Step 9 started: Page from the last search card is loaded");
            Logger.Information("Step 9: Page with job details is opened");

            var fullPageText = _CareerSearchBlock.CombineResultText();
            Logger.Information("Step 9: Text from title and description is combined");

            Assert.That(fullPageText.ToLower(), Does.Contain(textToFind.ToLower()), $"The combined page text did not contain the expected keyword: '{textToFind}'.");
            
            Logger.Information("Ending the test 'SearchOnIndexPageTest'.");
        }


        
        [TestCaseSource(nameof(SearchModelData))]
        public void ValidateThatTheUserCanSearchForPositionBasedOnCriteria_JSON(SearchModel searchModelItems)
        {
            Logger.Information("Starting the test 'ValidateThatTheUserCanSearchForPositionBasedOnCriteria'.");
            _homePage.Cookies.AcceptCookies();

            //Step 1: Click on 'Careers' menu button.
            Logger.Information("Step 1 started: Click on 'Careers' menu button.");
            _homePage.HeaderMenu.ClickMenuLink("Careers");
            _careerPage.WaitForCareerUrl("/careers");
            Logger.Information("Passed: 'Careers' page is opened.");

            // Step 2: Click on 'Start your search' button
            Logger.Information("Step 2 started: Click on 'Start your search' button.");
            _careerPage.WaitForFindCareerButton(10);
            _careerPage.ClickFindCareerButton();
            Logger.Information("Passed: Click on 'Start your search' button.");

            //Step 3: Accept cookies because user is redirected to sub-domen careers.epam.com
            Logger.Information("Step 3 started: Accept cookies on careers.epam.com");
            _careerPage.WaitForCareerUrl("/careers.epam.com");
            Logger.Information("Passed: Careers.epam.com is opened");
            _homePage.Cookies.AcceptCookies();
            Logger.Information("Passed: Accept cookies on careers.epam.com");


            //Select appropriate options for search

            //Step 4: Unselect locations
            Logger.Information("Step 4 started: Unselect locations");
            _careerPage.CareerSearchBlock.DeselectLocations();
            _careerPage.WaitForCareerUrl("/jobs?_gl=");
            Logger.Information("Passed: Unselect locations");

            //Step 5: Select 'Remote' option
            Logger.Information("Step 5 started: Select 'Remote' option");
            _careerPage.CareerSearchBlock.SelectRemoteCareerOption();
            _careerPage.WaitForCareerUrl("vacancy_type=remote");
            Logger.Information("Passed: Select 'Remote' option");

            //Step 6: Enter search keys
            Logger.Information("Step 6 started: Enter search keys");
            _careerPage.CareerSearchBlock.EnterCareerSearchTextUsingActions(searchModelItems.JobKey);
            Logger.Information("Passed: Enter search keys");

            //Step 7: Click on Search button
            Logger.Information("Step 7 started: Click on Search button");
            _careerPage.CareerSearchBlock.ClickFindCareerButton();
            Logger.Information("Passed: Click on Search button");


            //Analyze search results

            //Step 8: Click on last career card
            Logger.Information("Step 8 started: Click on Last career card");
            _careerPage.WaitForCareerUrl("&search=");
            _searchResultPage.SearchResults.ClickOnParticularCareerResultTitle(_searchResultPage.SearchResults.GetResultsTitles().Count - 1);
            Logger.Information("Passed: Click on Last career card");

            //Step 9: Verify that search key is present on the page
            _careerPage.WaitForCareerUrl("/vacancy");
            Logger.Information("Step 9 started: Page from the last search card is loaded");
            Logger.Information("Step 9: Page with job details is opened");

            var fullPageText = _CareerSearchBlock.CombineResultText();
            Logger.Information("Step 9: Text from title and description is combined");

            Assert.That(fullPageText.ToLower(), Does.Contain(searchModelItems.JobKey.ToLower()), $"The combined page text did not contain the expected keyword: '{searchModelItems.JobKey}'.");

            Logger.Information($"Ending the test 'SearchOnIndexPageTest' with key '{searchModelItems.JobKey}'.");
        }

    }
}