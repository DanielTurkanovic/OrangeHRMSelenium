using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.Job;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.AdminTabTests.JobTests
{
    public class PayGradesTests : TestBase
    {
        [Test]
        public void AddAndDeletePayGradeTest()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Add and delete Pay Grades");

            var specificForm = (PayGrades)WebForm;
            specificForm
                .AddPayGrades()
                .DeletePayGrades();

            IWebElement PayGradesTable = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = PayGradesTable.FindElements(By.XPath("(//div[@role='rowgroup'])[2]"));

            bool QAEngineerFound = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath("//div[@class='oxd-table-card']"));

                foreach (var cell in cells)
                {
                    if (cell.Text.Contains("QA Engineer"))
                    {
                        QAEngineerFound = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!QAEngineerFound)
                {
                    Assert.That(true, "The Test status value was successfully deleted from the table");
                }
            }
        }
    }
}
