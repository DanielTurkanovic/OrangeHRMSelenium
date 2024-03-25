using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace OrangeHrmSelenium.AdminTab.Job
{
    public class EmploymentStatus
    {
        IWebDriver driver;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement JobDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[2]"));
        IWebElement EmploymentStatusFromDropDownMenu => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[3]"));
        IWebElement AddButton => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-plus oxd-button-icon']"));
        IWebElement EmployeeName => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement SaveButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']"));
        IWebElement DeleteButton => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-trash'])[5]"));
        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-trash oxd-button-icon']"));

        // Constructors
        public EmploymentStatus(IWebDriver driver)
        { 
            this.driver = driver;
        }

        // Methods
        public EmploymentStatus EditEmploymentStatus()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            AdminTabButton.Click();
            JobDropDownList.Click();
            EmploymentStatusFromDropDownMenu.Click();
            AddButton.Click();
            EmployeeName.SendKeys("Test status");
            SaveButton.Click();

            return this;
        }

        public EmploymentStatus DeleteEmploymentStatus()
        {
            IWebElement TrashIcon = driver.FindElement(By.XPath("//div[contains(text(),'Test status')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));
            TrashIcon.Click();
            YesDeleteButton.Click();

            return this;
        }
    }
}
