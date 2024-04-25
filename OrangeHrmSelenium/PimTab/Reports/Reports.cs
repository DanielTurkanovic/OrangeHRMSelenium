using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.PimTab.Reports
{
    public class Reports
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement PimTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/pim/viewPimModule')]"));
        IWebElement ReportsButton => driver.FindElement(By.XPath("//a[contains(text(), 'Reports')]"));
        IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));
        IWebElement ReportName => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement SelectionCriteria => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-caret-down-fill oxd-select-text--arrow'])[1]"));
        IWebElement EmployeName => driver.FindElement(By.XPath("//span[contains(text(),'Employee Name')]/ancestor::div[@class='oxd-select-option']"));
        IWebElement Include => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-caret-down-fill oxd-select-text--arrow'])[2]"));
        IWebElement CurrentEmployeesOnly => driver.FindElement(By.XPath("//span[contains(text(),'Current Employees Only')]/ancestor::div[@class='oxd-select-option --selected']"));
        IWebElement SelectDisplayFieldGroup => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-caret-down-fill oxd-select-text--arrow'])[3]"));

        IWebElement Personal => driver.FindElement(By.XPath("//span[contains(text(),'Personal')]/ancestor::div[@class='oxd-select-option']"));
        IWebElement SelectDisplayField => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-caret-down-fill oxd-select-text--arrow'])[4]"));
        IWebElement EmployeeLastName => driver.FindElement(By.XPath("//span[contains(text(),'Employee Last Name')]/ancestor::div[@class='oxd-select-option']"));
        IWebElement PlusButton => driver.FindElement(By.XPath("(//button[@class='oxd-icon-button orangehrm-report-icon'])[2]"));
        IWebElement IncludeHeader => driver.FindElement(By.XPath("//span[@class='oxd-switch-input oxd-switch-input--active --label-right']"));
        IWebElement Save => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']"));
        IWebElement ReportNameSearch => driver.FindElement(By.XPath("//input[@placeholder='Type for hints...']"));
        IWebElement SearchButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']"));
        IWebElement TrashIckon => driver.FindElement(By.XPath("//div[contains(text(),'McDuck')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));
        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));


        // Constructor
        public Reports(IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public Reports AddReports()
        {
            ExtentReporting.Instance.LogInfo("Adding Reports");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(PimTabButton));

            PimTabButton.Click();
            ReportsButton.Click();
            AddButton.Click();
            ReportName.SendKeys("McDuck");
            SelectionCriteria.Click();
            EmployeName.Click();
            Include.Click();
            CurrentEmployeesOnly.Click();
            SelectDisplayFieldGroup.Click();
            Personal.Click();
            SelectDisplayField.Click();
            EmployeeLastName.Click();
            PlusButton.Click();
            IncludeHeader.Click();
            commonTools.HighlightElement(Save, driver);
            Save.Click();

            return this;
        }

        public Reports SearchForReportName()
        {
            ExtentReporting.Instance.LogInfo($"Search Report Name ");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(ReportsButton));

            ReportsButton.Click();
            ReportNameSearch.SendKeys("Mc");
            Task.Delay(3000).Wait();
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.ArrowDown).Perform();
            actions.SendKeys(Keys.Enter).Perform();
            Task.Delay(2000).Wait();
            commonTools.HighlightElement(SearchButton, driver);
            SearchButton.Click();

            return this;
        }
        public Reports DeleteReports()
            {
                ExtentReporting.Instance.LogInfo("Delete Reports Name");

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.ElementToBeClickable(ReportsButton));

                TrashIckon.Click();
                commonTools.HighlightElement(YesDeleteButton, driver);
                YesDeleteButton.Click();
                Task.Delay(1000).Wait();

                return this;
            }
        }
    }

