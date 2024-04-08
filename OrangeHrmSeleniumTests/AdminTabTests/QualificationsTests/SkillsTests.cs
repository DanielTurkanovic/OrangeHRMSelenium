using OpenQA.Selenium;
using OrangeHrmSelenium.AdminTab.Qualifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests.AdminTabTests.QualificationsTests
{
    public class SkillsTests : TestBase
    {
        [Test]
        public void AddAndDeleteSkillTest()
        {
            ExtentReporting.Instance.LogInfo("Start testing - Add and delete skills");

            var specificForm = (Skills)WebForm;
            specificForm
                .AddAndDeleteSkills()
                .DeleteSkills();

            IWebElement RecordFound = Driver.FindElement(By.XPath("//div[@role='table']"));

            IList<IWebElement> tableRow = RecordFound.FindElements(By.XPath("(//div[@role='rowgroup'])[2]"));

            bool Selenium = false;

            foreach (var row in tableRow)
            {
                IList<IWebElement> cells = row.FindElements(By.XPath("//div[@class='oxd-table-card']"));

                foreach (var cell in cells)
                {
                    if (cell.Text.Contains(" C# Selenium"))
                    {
                        Selenium = true;
                        Assert.Fail("The value has not been deleted");
                    }
                }

                if (!Selenium)
                {
                    Assert.IsTrue(true, "The C# Seleniumk value was successfully deleted from the table");
                }
            }
        }
    }
}
