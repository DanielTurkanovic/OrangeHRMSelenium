using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.Organization;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.AdminTabTests.OrganizationTests
{
    public class GeneralInformationTests : TestBase
    {
        [Test]
        public void EditingGeneralInformation()
        {
            ExtentReporting.Instance.LogInfo($"Start testing - Editing General Information");

            var specificForm = (GeneralInformation)WebForm;
            specificForm
                .EditGeneralInformation();
        }
    }
}
