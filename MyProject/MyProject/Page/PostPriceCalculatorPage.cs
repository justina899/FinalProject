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
        private const string Price = "4.29 €";
        private SelectElement SelectCountryButton => new SelectElement(Driver.FindElement(By.CssSelector(".select2-selection__arrow")));
        //private IWebElement SelectCountryButton => Driver.FindElement(By.CssSelector(".select2-container--below .select2-selection__arrow"));
        private IWebElement Input => Driver.FindElement(By.Id("select2-type-od-container"));
        private IWebElement Destination => Driver.FindElement(By.CssSelector(".destination .col-xs-12:nth-child(3) > .mh-item"));

        private IWebElement FromLPExpressButton => Driver.FindElement(By.CssSelector("#tab-1-1 > div > div.col-md-9 > form > div:nth-child(4) > ul > li:nth-child(3) > label"));
        private IWebElement ToLPExpressButton => Driver.FindElement(By.CssSelector("#tab-1-1 > div > div.col-md-9 > form > div.row.mh-entry.destination > ul > li:nth-child(3) > label"));
        private IWebElement PackageSize => Driver.FindElement(By.CssSelector("#sizesList > li:nth-child(4) > label"));
        private IWebElement CalculatedPrice => Driver.FindElement(By.Id("lpe_price"));
        private IWebElement SendingWayInfo => Driver.FindElement(By.Id("lpePrcText1"));
        private IWebElement PopUp => Driver.FindElement(By.Id("CybotCookiebotDialogBodyButtonAccept"));

        public PostPriceCalculatorPage(IWebDriver webdriver) : base(webdriver)
        {
            Driver.Url = PageAddress;
        }

        public PostPriceCalculatorPage CloseBrowserPopUp()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementExists(By.Id("CybotCookiebotDialogBodyButtonAccept")));
            PopUp.Click();
            return this;
        }

        public PostPriceCalculatorPage FocusOnFrame()
        {
            Driver.SwitchTo().Frame(1);
            return this;
        }

        public PostPriceCalculatorPage SelectCountry()
        {
            SelectCountryButton.SelectByText(Country);
            //Input.Click();
            //Destination.Click();
            return this;
        }

        public PostPriceCalculatorPage CheckCountry()
        {
            Assert.AreEqual(Country, Input.GetAttribute("title"));
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
            Assert.AreEqual(Price, CalculatedPrice.Text, "Price is not the same");
            return this;
        }
    }
}
