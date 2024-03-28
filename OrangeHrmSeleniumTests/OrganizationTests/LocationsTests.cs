using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.Organization;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.Organization
{
    public class LocationsTests : TestBase
    {
        [Test]
        public void AddAndDeleteLocationsTest()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Add and delete locations");

            var specificForm = (Locations)WebForm;
            specificForm
            .AddLocations()
            .DeleteLocation();

            IWebElement RecordFound = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = RecordFound.FindElements(By.XPath("(//div[@role='rowgroup'])[2]"));

            bool DonaldDuck = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath("//div[@class='oxd-table-card']"));

                foreach (var cell in cells)
                {
                    if (cell.Text.Contains("donald d"))
                    {
                        DonaldDuck = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!DonaldDuck)
                {
                    Assert.IsTrue(true, "The Donald Duck value was successfully deleted from the table");
                }
            }
        }
    }
}
