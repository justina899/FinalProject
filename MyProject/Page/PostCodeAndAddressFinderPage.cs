using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace MyProject.Page
{
    public class PostCodeAndAddressFinderPage : BasePage 
    {
        private const string PageAddress = "https://post.lt/pasto-kodu-ir-adresu-paieska";
        private const string PostCode = "01111";
        private const string Address = "J. Jasinskio g. 15, 01111 Vilnius";
        private IWebElement PopUp => Driver.FindElement(By.Id("CybotCookiebotDialogBodyButtonAccept"));
        private IWebElement PostCodeFindButton => Driver.FindElement(By.CssSelector("#app-body > div.app-container > div.dialog-off-canvas-main-canvas > div > div.container-big.pad-size-1 > div > ul > li.active > a > span"));
        private IWebElement TownButton => Driver.FindElement(By.CssSelector(".select2-selection__placeholder"));
        private IWebElement TownInput => Driver.FindElement(By.CssSelector(".select2-search__field"));
        private IWebElement Town => Driver.FindElement(By.Id("select2-locality-container"));
        private IWebElement StreetButton => Driver.FindElement(By.CssSelector(".select2-selection__arrow"));
        private IWebElement StreetInput => Driver.FindElement(By.CssSelector(".select2-search__field"));
        private IWebElement Street => Driver.FindElement(By.Id("select2-street-container"));
        private IWebElement HouseNumber => Driver.FindElement(By.Id("house"));
        private IWebElement SearchPostCodeButton => Driver.FindElement(By.Id("searchPostCode"));
        private IWebElement PostCodeSearchingActualResult => Driver.FindElement(By.CssSelector("#tblContent > tr > td:nth-child(1)"));
        private IWebElement AddressFindButton => Driver.FindElement(By.CssSelector("#app-body > div.app-container > div.dialog-off-canvas-main-canvas > div > div.container-big.pad-size-1 > div > ul > li:nth-child(2) > a > span"));
        private IWebElement PostCodeInput => Driver.FindElement(By.Id("code"));
        private IWebElement SearchAddressButton => Driver.FindElement(By.Id("searchCode"));
        private IReadOnlyCollection<IWebElement> AddressSearchingActualResult => Driver.FindElements(By.CssSelector("#tblContent > tr:nth-child(1) > td:nth-child(3)"));
        private IWebElement Table => Driver.FindElement(By.Id("tblContent"));


        public PostCodeAndAddressFinderPage(IWebDriver webdriver) : base(webdriver){}

        public PostCodeAndAddressFinderPage NavigateToDefaultPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
            return this;
        }

        public PostCodeAndAddressFinderPage AcceptAllCookies()
        {
            Cookie myCookie = new Cookie
                ("CookieConsent",
                "{stamp:%27XrF9Tme2rtOF2vFNQMnUOjNRR+pQIFnBMFXcWPxkEsS6DbH7U1MDQw==%27%2Cnecessary:true%2Cpreferences:true%2Cstatistics:true%2Cmarketing:true%2Cver:1%2Cutc:1606319172027%2Cregion:%27lt%27}",
                "post.lt",
                "/",
                DateTime.Now.AddDays(2)
                );

            Driver.Manage().Cookies.AddCookie(myCookie);
            Driver.Navigate().Refresh();
            Driver.Manage().Cookies.DeleteAllCookies();
            return this;
        }
                
        public PostCodeAndAddressFinderPage CloseBrowserPopUp()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementExists(By.Id("CybotCookiebotDialogBodyButtonAccept")));
            PopUp.Click();
            return this;
        }

        public PostCodeAndAddressFinderPage FindPostCodeButtonClick()
        {
            PostCodeFindButton.Click();
            return this;
        }

        public PostCodeAndAddressFinderPage FillTown(string town)
        {
            TownButton.Click();        
            TownInput.SendKeys(town);
            Town.Click();
            return this;
        }

        public PostCodeAndAddressFinderPage FillStreet(string street)
        {
            StreetButton.Click();        
            StreetInput.SendKeys(street);
            Street.Click();
            return this;
        }

        public PostCodeAndAddressFinderPage FillHouseNumber(string houseNumber)
        {
            HouseNumber.SendKeys(houseNumber);
            return this;
        }

        public PostCodeAndAddressFinderPage SearchPostCodeButtonClick()
        {
            SearchPostCodeButton.Click();
            return this;
        }

        public PostCodeAndAddressFinderPage CheckSearchPostCodeResult()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => PostCodeSearchingActualResult.Displayed);
            Assert.AreEqual(PostCode, PostCodeSearchingActualResult.Text, "wrong post code");
            return this;
        }

        public PostCodeAndAddressFinderPage FindAddressButtonClick()
        {
            AddressFindButton.Click();
            return this;
        }

        public PostCodeAndAddressFinderPage FillPostCode(string postCode)
        {
            PostCodeInput.SendKeys(postCode);
            return this;
        }

        public PostCodeAndAddressFinderPage SearchAddressButtonClick()
        {
            SearchAddressButton.Click();
            return this;
        }

        private string SearchingRightAddress()
        {
            string address = "";
            foreach (IWebElement element in AddressSearchingActualResult)
            {
                if (element.Text.Contains(Address))
                {
                    address = element.Text;
                }
            }
            return address;
        }

        public PostCodeAndAddressFinderPage CheckSearchAddressResult()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => Table.Displayed);
            string address = SearchingRightAddress();
            Assert.IsTrue(address.Contains(Address), "wrong address");
            return this;
        }
    }
}
