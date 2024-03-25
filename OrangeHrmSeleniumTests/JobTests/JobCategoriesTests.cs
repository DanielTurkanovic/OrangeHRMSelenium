using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.Job;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.JobTests
{
    public class JobCategoriesTest : TestBase
    {
        [Test]
            public void EditJobCategoriesTests()
            {
            ExtentReporting.Instance.LogInfo("Start testing - Job Categories from Job drop down menu");

            var specificForm = (JobCategories)WebForm;
            specificForm
                .ChoseJobCategoriesFromDropDownMenu()
                .DeleteJobCategories();

            IWebElement jobCategoryTable = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = jobCategoryTable.FindElements(By.XPath(".//div[@role='row']"));

            bool qaTesterFound = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath(".//div[@role='cell']"));

                foreach (var cell in cells) 
                {
                    if (cell.Text.Contains("QA Tester"))
                    {
                        qaTesterFound = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!qaTesterFound)
                {
                    Assert.IsTrue(true, "The QA Tester value was successfully deleted from the table");
                }
            }
        }
    } 
}
