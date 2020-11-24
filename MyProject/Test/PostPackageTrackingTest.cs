using NUnit.Framework;

namespace MyProject.Test
{
    public class PostPackageTrackingTest : BaseTest
    {

        [Test]
        public void TestPackageTracking()
        {
            _postPackageTrackingPage.NavigateToDefaultPage()
                .AcceptAllCookies()
                .TrackPackage("RE083109570LT")
                .CheckTracking();
        }
    }
}
