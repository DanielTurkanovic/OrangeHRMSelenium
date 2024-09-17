using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.UserManagement;
using SeleniumExtras.WaitHelpers;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.AdminTabTests.UserManagementTests
{
    public class UserManagementTests : TestBase
    {
        [Test]
        public void AddUser()
        {
            ExtentReporting.Instance.LogInfo("Starting test - Adding user");

            var specificForm = (UserManagement)WebForm;
            var message =
            specificForm
                .ClickOnAdminTabButton()
                .ClickOnAddButton()
                .AddUser()
                .SearchForAddedUser()
                .DelateAddedUser();

            IWebElement WorkShifts = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = WorkShifts.FindElements(By.XPath("(//div[@role='rowgroup'])[2]"));

            bool BugsB = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath("//div[@class='oxd-table-card']"));

                foreach (var cell in cells)
                {
                    if (cell.Text.Contains("bugs b"))
                    {
                        BugsB = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!BugsB)
                {
                    Assert.That(true, "The bugs b value was successfully deleted from the table");
                }
            }
        }
    }
}
