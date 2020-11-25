using MyProject.Drivers;
using MyProject.Page;
using MyProject.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MyProject.Test
{
    public class BaseTest
    {

        public static IWebDriver driver;
        public static PostCodeAndAddressFinderPage _postCodeAndAddressFinderPage;
        public static PostPackageTrackingPage _postPackageTrackingPage;
        public static PostPriceCalculatorPage _postPriceCalculatorPage;
        

        [OneTimeSetUp]
        public static void SetUp()
        {
            driver = CustomDriver.GetIncognitoChrome();
            _postCodeAndAddressFinderPage = new PostCodeAndAddressFinderPage(driver);
            _postPackageTrackingPage = new PostPackageTrackingPage(driver);
            _postPriceCalculatorPage = new PostPriceCalculatorPage(driver);
            
        }

        [TearDown]
        public static void TakeScreenshot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                MyScreenshot.MakeScreenshot(driver);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            driver.Quit();
        }
    }
}
