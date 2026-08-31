using CoreLayer;
using CoreLayer.WebDriver;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V149.Runtime;
using System.Security.Cryptography.X509Certificates;
using TestLayer.Data;
using static CoreLayer.WebDriver.WebDriverFactory;
using SearchModel = TestLayer.Data.SearchModel;

namespace TestLayer.Tests
{
    public abstract class BaseTest
    {
        protected WebDriverWrapper WebDriverWrapper { get; private set; }

        protected Logger Logger { get; private set; }

        public static string noSearchResultExpectedMessage = "Sorry, but your search returned no results. Please try another query.";


        [SetUp]
        public virtual void SetUp()
        {
            var browserType = (BrowserType)Enum.Parse(typeof(BrowserType), Configuration.BrowserType);

            WebDriverWrapper = new WebDriverWrapper(browserType);
            WebDriverWrapper.StartBrowser();
            WebDriverWrapper.NavigateTo(Configuration.AppUrl);

            Logger ??= new Logger();
        }
        public static List<SearchModel> SearchModelData
        {
            get
            {
                var jsonData = File.ReadAllText(Configuration.TestDataPath);
                var searchModelItems = JsonConvert.DeserializeObject<List<SearchModel>>(jsonData);
                return searchModelItems;
            }
        }

        //method to generate random string for searches
        public string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789 ";
            var random = new Random();

            char[] stringChars = new char[length];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        [TearDown]
        public virtual void TearDown() => WebDriverWrapper.Close();
    }
}
