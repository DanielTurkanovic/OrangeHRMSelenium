using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.Job;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.JobTests
{
    public class JobTitlesTests : TestBase
    {
        [Test]
        public void AddJobTitle()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Add Job Titles");

            var specificForm = (JobTitles)WebForm;
            specificForm
                .AddJobTitles()
                .DelateJobTitles();

            IWebElement JobTitlesTable = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = JobTitlesTable.FindElements(By.XPath("(//div[@role='rowgroup'])[2]"));

            bool AutomationTestFound = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath("//div[@class='oxd-table-card']"));

                foreach (var cell in cells)
                {
                    if (cell.Text.Equals("Automation test"))
                    {
                        AutomationTestFound = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!AutomationTestFound)
                {
                    Assert.IsTrue(true, "The Test status value was successfully deleted from the table");
                }
            }
        }
    }
}
