using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OrangeHrmSelenium.AdminTab.Job;
using SeleniumExtras.WaitHelpers;

namespace OrangeHrmSelenium.AdminTab.UserManagement
{
    public class UserManagement
    {
        IWebDriver driver;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement AddUserButton => driver.FindElement(By.CssSelector("button.oxd-button.oxd-button--medium.oxd-button--secondary"));
        IWebElement UserRoleDropDown => driver.FindElement(By.XPath("(//i[contains(@class, 'oxd-icon bi-caret-down-fill oxd-select-text--arrow')])[1]"));
        IWebElement AdminUserRole => driver.FindElement(By.XPath("(//div[@class='oxd-select-option'])[2]"));
        IWebElement StatusDropDown => driver.FindElement(By.XPath("(//div[@class='oxd-select-text-input'])[2]"));
        IWebElement EnabledStatus => driver.FindElement(By.XPath("(//div[@class='oxd-select-option'])[3]"));
        IWebElement EmployeeName => driver.FindElement(By.XPath("(//div[@class='oxd-autocomplete-text-input oxd-autocomplete-text-input--active'])"));
        IWebElement EmployeeNameFromDropDown => driver.FindElement(By.XPath("//span[contains(text(),'Odis  Adalwin')]"));

        IWebElement Username => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement Password => driver.FindElement(By.XPath("(//input[@type='password'])[1]"));
        IWebElement ConfirmPassword => driver.FindElement(By.XPath("(//input[@type='password'])[2]"));
        IWebElement SaveButton => driver.FindElement(By.XPath("(//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space'])"));
        IWebElement SearchButton => driver.FindElement(By.CssSelector("button.oxd-button.orangehrm-left-space"));
        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));


        // Constructor
        public UserManagement(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Methods
        public UserManagement ClickOnAdminTabButton()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            AdminTabButton.Click();

            return this;
        }


        public UserManagement ClickOnAddButton()
        {
            AddUserButton.Click();
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/admin/saveSystemUser");

            return this;
        }

        public UserManagement AddUser()
        {
            UserRoleDropDown.Click();
            AdminUserRole.Click();
            Actions actions = new Actions(driver);
            actions.MoveToElement(EmployeeName).Click().Build().Perform();
            actions.SendKeys("a").Build().Perform();
            EmployeeNameFromDropDown.Click();
            StatusDropDown.Click();
            EnabledStatus.Click();
           
            Username.SendKeys("bugs b");
            Thread.Sleep(3000);
            Password.SendKeys("Admin123");
            Thread.Sleep(1000);
            ConfirmPassword.SendKeys("Admin123");
            SaveButton.Click();
            

            return this;
        }

        public UserManagement SearchForAddedUser()
        {
            Thread.Sleep(3000);
            Username.SendKeys("bugs b");
            Thread.Sleep(3000);
            SearchButton.Click();

            return this;
        }

        public UserManagement DelateWorkShifts()
        {
            IWebElement trashIcon = driver.FindElement(By.XPath("//div[contains(text(),'bugs b')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));
            trashIcon.Click();
            YesDeleteButton.Click();

            return this;
        }
    }
}
