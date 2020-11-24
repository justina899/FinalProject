using NUnit.Framework;


namespace MyProject.Test
{
    public class PostCodeAndAddressFinderTest : BaseTest
    {

        [Test]
        public void TestFindPostCode()
        {
            _postCodeAndAddressFinderPage.NavigateToDefaultPage()
                .AcceptAllCookies()
                .FindPostCodeButtonClick()
                .FillTown("Vilnius")
                .FillStreet("J. Jasinskio")
                .FillHouseNumber("15")
                .SearchPostCodeButtonClick();
        }

        [Test]
        public void TestFindAddress()
        {
            _postCodeAndAddressFinderPage.NavigateToDefaultPage()
                .AcceptAllCookies()
                .FindAddressButtonClick()
                .FillPostCode("01111")
                .SearchAddressButtonClick()
                .CheckSearchAddressResult();
        }
    }
}
