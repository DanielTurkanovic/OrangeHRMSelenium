using OpenQA.Selenium;
using OrangeHrmSelenium.LeaveTab.Entitlements;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.LeaveTabTests.EntitlementsTests
{
    public class EntitlementsTests : TestBase
    {
        [Test]
        public void AddEntitlements()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Adding Entitlements");
            var specificForm = (Entitlements)WebForm;
            specificForm
                .AddingEntitlements()
                .DeleteEntitlements();

            IWebElement NoRecordsFound = Driver.FindElement(By.XPath("(//span[@class='oxd-text oxd-text--span'])[2]"));

            if (NoRecordsFound.Displayed) 
            {
                Assert.That(true, "Record is deleted");
            }
            else
            {
                Assert.Fail("Record is not deleted");
            }
        }
    }
}
