using CoreLayer;
using CoreLayer.WebDriver;
using NUnit.Framework;
using static CoreLayer.WebDriver.WebDriverFactory;

namespace TestLayer.Tests
{
    public abstract class BaseTest
    {
        protected WebDriverWrapper WebDriverWrapper { get; private set; }

        protected Logger Logger { get; private set; }

        [SetUp]
        public virtual void SetUp()
        {
            var browserType = (BrowserType)Enum.Parse(typeof(BrowserType), Configuration.BrowserType);

            WebDriverWrapper = new WebDriverWrapper(browserType);
            WebDriverWrapper.StartBrowser();
            WebDriverWrapper.NavigateTo(Configuration.AppUrl);

            Logger ??= new Logger();
        }

        [TearDown]
        public virtual void TearDown() => WebDriverWrapper.Close();
    }
}
