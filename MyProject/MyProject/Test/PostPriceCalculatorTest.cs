using MyProject.Page;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Test
{
    public class PostPriceCalculatorTest
    {
        private static PostPriceCalculatorPage _page;

        [OneTimeSetUp]
        public static void SetUp()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            _page = new PostPriceCalculatorPage(driver);
            
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            _page.CloseBrowser();
        }

        //C:\Users\J\source\repos\MyProject

        [Test]
        public void TestPickedCountry()
        {
            _page.CloseBrowserPopUp()
                //.FocusOnFrame()
                .SelectCountry()
                .CheckCountry();
        }

        [Test]
        public void TestPickedSendingWay()
        {
            _page.CloseBrowserPopUp()
                 //.SelectCountry()
                 //.CheckCountry();
                //.FocusOnFrame()
                .PickPackageSendingWay()
                 .CheckSendingWay();
        }

        [Test]
        public void TestSendingPrice()
        {
            _page.CloseBrowserPopUp()
                //.FocusOnFrame()
                .PickPackageSendingWay()
                .CheckSendingWay()
                .PickPackageSize()
                .CheckCalculatedPrice();
        }
    }
}
