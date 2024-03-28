using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.AdminTab.Job
{
    public class EmploymentStatus
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement JobDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[2]"));
        IWebElement EmploymentStatusFromDropDownMenu => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[3]"));
        IWebElement AddButton => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-plus oxd-button-icon']"));
        IWebElement EmployeeName => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement SaveButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']"));
        IWebElement TrashIcon => driver.FindElement(By.XPath("//div[contains(text(),'Test status')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));

        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-trash oxd-button-icon']"));

        // Constructors
        public EmploymentStatus(IWebDriver driver)
        { 
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public EmploymentStatus AddingEmploymentStatus()
        {
            ExtentReporting.Instance.LogInfo($"Adding Employment Status");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            AdminTabButton.Click();
            JobDropDownList.Click();
            EmploymentStatusFromDropDownMenu.Click();
            AddButton.Click();
            EmployeeName.SendKeys("Test status");
            commonTools.HighlightElement(SaveButton, driver);
            SaveButton.Click();
            Task.Delay(3000);

            return this;
        }

        public EmploymentStatus DeleteEmploymentStatus()
        {
            ExtentReporting.Instance.LogInfo($"Delete Employment Status");

            TrashIcon.Click();
            commonTools.HighlightElement(YesDeleteButton, driver);
            YesDeleteButton.Click();
            Task.Delay(3000);

            return this;
        }
    }
}
