using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.AdminTab.Job
{
    public class WorkShifts
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement JobDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[2]"));
        IWebElement WorkShiftsFromDropDownList => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[5]"));
        IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));
        IWebElement ShiftName => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement WorkingHoursFromAM => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-clock oxd-time-input--clock'])[1]"));
        IWebElement HourButtonUpAM => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-chevron-up oxd-icon-button__icon oxd-time-hour-input-up']"));
        IWebElement WorkingHoursFromPM => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-clock oxd-time-input--clock'])[2]"));
        IWebElement HourButtonDownPM => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-chevron-down oxd-icon-button__icon oxd-time-hour-input-down']"));
        IWebElement AssignedEmployees => driver.FindElement(By.XPath("//input[@placeholder='Type for hints...']"));
        IWebElement SaveButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']"));
        IWebElement TrashIcon => driver.FindElement(By.XPath("//div[contains(text(),'LazyQaEngineer')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));
        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']")); 

        // Constructor
        public WorkShifts(IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public WorkShifts AddWorkShift()
        {
            ExtentReporting.Instance.LogInfo($"Click Work Shifts from drop down manu and add Work Shift");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            AdminTabButton.Click();
            JobDropDownList.Click();
            WorkShiftsFromDropDownList.Click();
            AddButton.Click();
            ShiftName.SendKeys("LazyQaEngineer");
            WorkingHoursFromAM.Click();

            for (int i = 0; i < 3; i++)
            {
                HourButtonUpAM.Click();
            }

            WorkingHoursFromPM.Click();

            for (int i = 0; i < 3; i++)
            {
                HourButtonDownPM.Click();
            }

            IWebElement body = driver.FindElement(By.TagName("body"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(body, 0, 0).Click().Perform();

            AssignedEmployees.SendKeys("LazyQaEngineer");
            commonTools.HighlightElement(SaveButton, driver);
            SaveButton.Click();
            Task.Delay(3000);

            return this;
        }

        public WorkShifts DelateWorkShifts()
        {
            ExtentReporting.Instance.LogInfo($"Delete added Work Shift");

            TrashIcon.Click();
            commonTools.HighlightElement(YesDeleteButton, driver);
            YesDeleteButton.Click();
            Task.Delay(3000);

            return this;
        }

    }
}
