using OpenQA.Selenium;
using OrangeHrmSelenium.PimTab.EmployeeList;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.PimTabTests.EmployeeListTests
{
    public class EmploeeListTests : TestBase
    {
        [Test]
        public void AddAndDeleteEmployeeTests()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Adding Employee");
            var specificForm = (EmployeeList)WebForm;
            specificForm
            .AddAndDeleteEmployee();

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
                    Assert.IsTrue(true, "The McDuck value was successfully deleted from the table");
                }
            }
        }
    }
}
