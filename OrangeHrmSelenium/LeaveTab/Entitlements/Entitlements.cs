using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.LeaveTab.Entitlements
{
    public class Entitlements
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement LeaveTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/leave/viewLeaveModule')]"));
        IWebElement EntitlamentsButton => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[1]"));
        IWebElement AddEntitlements => driver.FindElement(By.XPath("//a[contains(text(), 'Add Entitlements')]"));
        IWebElement EmployeeName => driver.FindElement(By.XPath("//input[@placeholder='Type for hints...']"));
        IWebElement LeaveType => driver.FindElement(By.XPath("//div[contains(text(), 'Select')]"));
        IWebElement SelectLeaveType => driver.FindElement(By.XPath("//span[contains(text(), 'CAN - Vacation')]"));
        IWebElement Entitlement => driver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active']"));
        IWebElement SaveButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        IWebElement ConfirmButton => driver.FindElement(By.XPath("//button[contains(@class, 'medium oxd-button--secondary') and @type='button']"));
        IWebElement TrashIcon => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-trash']"));
        IWebElement YesDelete => driver.FindElement(By.XPath("//button[contains(@class, 'oxd-button--label-danger ') and @type ='button']"));

        // Constructor
        public Entitlements(IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public Entitlements AddingEntitlements()
        {
            ExtentReporting.Instance.LogInfo("Adding Entitlements");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(LeaveTabButton));

            LeaveTabButton.Click();
            EntitlamentsButton.Click();
            AddEntitlements.Click();
            EmployeeName.SendKeys("a");

            Task.Delay(2000).Wait();
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.ArrowDown).Perform();
            actions.SendKeys(Keys.Enter).Perform();

            LeaveType.Click();
            SelectLeaveType.Click();
            Entitlement.Click();
            Entitlement.SendKeys("30");
            commonTools.HighlightElement(SaveButton, driver);
            SaveButton.Click();
            commonTools.HighlightElement(ConfirmButton, driver);
            ConfirmButton.Click();

            return this;
        }

        public Entitlements DeleteEntitlements()
        {
            ExtentReporting.Instance.LogInfo("Delete Entitlements");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(TrashIcon));

            TrashIcon.Click();
            commonTools.HighlightElement(YesDelete, driver);
            YesDelete.Click();

            return this;
        }
    }
}
