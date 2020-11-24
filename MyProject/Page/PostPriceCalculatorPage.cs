using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MyProject.Page
{
    public class PostPriceCalculatorPage : BasePage
    {
        private const string PageAddress = "https://post.lt/kainu-skaiciuokle";
        private const string Country = "Lietuva - Lithuania";
        private const string SendingWayTextTerminalToTerminal = "Siunta bus pristatyta iš vieno siuntų savitarnos terminalo į kitą.";
        private const string Price = "4.29";
        private IWebElement SelectCountryButton => Driver.FindElement(By.CssSelector(".select2-selection__arrow"));
        
        private IWebElement CountryOption => Driver.FindElement(By.XPath("//li[contains(@id,'-LT')]"));
        private IWebElement CountryBox => Driver.FindElement(By.CssSelector("//span[@title='Lietuva - Lithuania']"));

        private IWebElement FromLPExpressButton => Driver.FindElement(By.CssSelector("#tab-1-1 > div > div.col-md-9 > form > div:nth-child(4) > ul > li:nth-child(3) > label"));
        private IWebElement ToLPExpressButton => Driver.FindElement(By.CssSelector("#tab-1-1 > div > div.col-md-9 > form > div.row.mh-entry.destination > ul > li:nth-child(3) > label"));
        private IWebElement PackageSize => Driver.FindElement(By.CssSelector("#sizesList > li:nth-child(4) > label"));
        private IWebElement CalculatedPrice => Driver.FindElement(By.CssSelector("#lpe_price"));
        private IWebElement SendingWayInfo => Driver.FindElement(By.Id("lpePrcText1"));

        public PostPriceCalculatorPage(IWebDriver webdriver) : base(webdriver){}

        public PostPriceCalculatorPage NavigateToDefaultPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
            return this;
        }

        public PostPriceCalculatorPage AcceptAllCookies()
        {
            Cookie myCookie = new Cookie
                ("CookieConsent",
                "{stamp:%27RtK/rGsILdzWHuxmVX4E08mPDvtXvRVRLV3ZHyS39uYM/Wa6EAlMBg==%27%2Cnecessary:true%2Cpreferences:true%2Cstatistics:true%2Cmarketing:true%2Cver:1%2Cutc:1606239153702%2Cregion:%27lt%27}",
                "post.lt",
                "/",
                DateTime.Now.AddDays(2)
                );

            Driver.Manage().Cookies.AddCookie(myCookie);
            Driver.Navigate().Refresh();
            Driver.Manage().Cookies.DeleteAllCookies();
            return this;
        }

       
        public PostPriceCalculatorPage SelectCountry()
        {
            SelectCountryButton.Click();
            CountryOption.Click();
            return this;
        }

        public PostPriceCalculatorPage CheckCountry()
        {
            Assert.AreEqual(Country, CountryBox.GetAttribute("title"));
            return this;
        }

        public PostPriceCalculatorPage PickPackageSendingWay()
        {
            FromLPExpressButton.Click();
            ToLPExpressButton.Click();
            return this;
        }

        public PostPriceCalculatorPage CheckSendingWay()
        {
            Assert.AreEqual(SendingWayTextTerminalToTerminal, SendingWayInfo.Text, "text is not the same");
            return this;
        }

        public PostPriceCalculatorPage PickPackageSize()
        {
            PackageSize.Click();
            return this;
        }


        public PostPriceCalculatorPage CheckCalculatedPrice()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => CalculatedPrice.Displayed);
            Assert.AreEqual(Price, CalculatedPrice.Text.Substring(0, 4), "Price is not the same");
            return this;
        }
    }
}
