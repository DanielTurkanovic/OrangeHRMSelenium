using AventStack.ExtentReports.Model;
using OpenQA.Selenium;
using OrangeHrmSelenium.PimTab.Reports;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.PimTabTests.ReportsTests
{
    public class ReportTests : TestBase
    {
        [Test]
        public void AddReport()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Adding Report"); 
            var specificForm = (Reports)WebForm;
            specificForm
            .AddReports()
            .SearchForReportName()
            .DeleteReports();

            IWebElement WorkShifts = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = WorkShifts.FindElements(By.XPath("(//div[@role='rowgroup'])[2]"));

            bool McDuck = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath("//div[@class='oxd-table-card']"));

                foreach (var cell in cells)
                {
                    if (cell.Text.Contains("McDuck"))
                    {
                        McDuck = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!McDuck) 
                {
                    Assert.That(true, "The McDuck value was successfully deleted from the table");
                }
            }
        }
    }
}
