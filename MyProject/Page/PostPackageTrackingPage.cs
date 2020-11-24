using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MyProject.Page
{
    public class PostPackageTrackingPage : BasePage
    {
        private const string PageAddress = "https://post.lt/siuntu-sekimas";
        private const string TrackingResult = "Siunta nerasta. Prašome patikrinti siuntos numerį.";
        private IWebElement PopUp => Driver.FindElement(By.Id("CybotCookiebotDialogBodyButtonAccept"));
        private IWebElement PackageNumberInput => Driver.FindElement(By.Id("parcelsInput"));
        private IWebElement FindPackageButton => Driver.FindElement(By.CssSelector("#app-body > div.app-container > div.dialog-off-canvas-main-canvas > div > div.container-big.pad-size-1 > div > div:nth-child(1) > div > div.panel.panel-default.panel-tracking > form > div > div.form-group.actions > button"));
        private IWebElement ActualTrackingResult => Driver.FindElement(By.CssSelector(".list-tick"));

        public PostPackageTrackingPage(IWebDriver webdriver) : base(webdriver){}

        public PostPackageTrackingPage NavigateToDefaultPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
            return this;
        }

        public PostPackageTrackingPage AcceptAllCookies()
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

        public PostPackageTrackingPage TrackPackage(string packageNumber)
        {
            PackageNumberInput.SendKeys(packageNumber);
            FindPackageButton.Click();
            return this;
        }

        public PostPackageTrackingPage CheckTracking()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => ActualTrackingResult.Displayed);
            Assert.IsTrue(ActualTrackingResult.Text.Contains(TrackingResult), "text is not the same");
            return this;
        }
    }
}
