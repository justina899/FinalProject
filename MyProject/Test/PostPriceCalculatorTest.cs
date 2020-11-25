using NUnit.Framework;


namespace MyProject.Test
{
    public class PostPriceCalculatorTest : BaseTest
    {

        [Test]
        public void TestSelectedCountry()
        {
            _postPriceCalculatorPage.NavigateToDefaultPage()
                .AcceptAllCookies()
                .SelectCountry()
                .CheckCountry();
        }

        [Test]
        public void TestPickedSendingWay()
        {
            _postPriceCalculatorPage.NavigateToDefaultPage()
                .AcceptAllCookies()
                .PickPackageSendingWay()
                 .CheckSendingWay();
        }

        [Test]
        public void TestSendingPrice()
        {
            _postPriceCalculatorPage.NavigateToDefaultPage()
                .AcceptAllCookies()
                .PickPackageSendingWay()
                .CheckSendingWay()
                .PickPackageSize()
                .CheckCalculatedPrice();
        }
    }
}
