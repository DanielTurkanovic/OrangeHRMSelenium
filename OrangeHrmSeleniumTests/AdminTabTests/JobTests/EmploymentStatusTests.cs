using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.Job;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.AdminTabTests.JobTests
{
    public class EmploymentStatusTests : TestBase
    {
        [Test]
        public void EditEmploymentStatusTests()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Employment status from Job drop down menu");

            var specificForm = (EmploymentStatus)WebForm;
            specificForm
            .AddingEmploymentStatus()
            .DeleteEmploymentStatus();

            IWebElement EmployeeStatusTable = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = EmployeeStatusTable.FindElements(By.XPath(".//div[@role='row']"));

            bool testStatusFound = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath(".//div[@role='cell']"));

                foreach (var cell in cells)
                {
                    if (cell.Text.Contains("Test status"))
                    {
                        testStatusFound = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!testStatusFound)
                {
                    Assert.That(true, "The Test status value was successfully deleted from the table");
                }
            }
        }
    }
}
