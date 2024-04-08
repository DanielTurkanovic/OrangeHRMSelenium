using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.Job;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.AdminTabTests.JobTests
{
    public class WorkShiftsTests : TestBase
    {
        [Test]
        public void AddWorkShiftsTest()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Add Work Shift");

            var specificForm = (WorkShifts)WebForm;
            specificForm
                .AddWorkShift()
                .DelateWorkShifts();

            IWebElement WorkShifts = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = WorkShifts.FindElements(By.XPath("(//div[@role='rowgroup'])[2]"));

            bool LazyQaEngineer = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath("//div[@class='oxd-table-card']"));

                foreach (var cell in cells)
                {
                    if (cell.Text.Contains("LazyQaEngineer"))
                    {
                        LazyQaEngineer = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!LazyQaEngineer)
                {
                    Assert.IsTrue(true, "The Lazy Qa Engineers value was successfully deleted from the table");
                }
            }
        }
    }
}
