using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace OrangeHrmSelenium.AdminTab.Job
{
    public class PayGrades
    {
        IWebDriver driver;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement JobDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[2]"));
        IWebElement PayGradesFromDropDownMenu => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[2]"));
        IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));
        IWebElement InputName => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement SaveButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']"));
        IWebElement CurrenciesAddButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));
        IWebElement CurrencySelectDropDownMenu => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-caret-down-fill oxd-select-text--arrow']"));
        IWebElement CurrencySelect => driver.FindElement(By.XPath("//div[@role='option']/span[contains(text(),'EUR - Euro')]"));
        IWebElement InputMinimumSalary => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[3]"));
        //IWebElement DeleteButton => driver.FindElement(By.XPath("(//button[@class='oxd-icon-button oxd-table-cell-action-space'])[11]"));
        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));
       

        // Constructor
        public PayGrades(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Methods
        public PayGrades AddPayGrades()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            AdminTabButton.Click();
            JobDropDownList.Click();
            PayGradesFromDropDownMenu.Click();
            AddButton.Click();
            InputName.SendKeys("QA Engineer");
            SaveButton.Click();
            CurrenciesAddButton.Click();
            CurrencySelectDropDownMenu.Click();
            CurrencySelect.Click();
            InputMinimumSalary.SendKeys("50000");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IWebElement InputMaximumSalary = new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementIsVisible(By.XPath("(//div[@class='oxd-grid-item oxd-grid-item--gutters'])[4]")));

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            bool isVisible = InputMaximumSalary.Displayed;
            bool isEnabled = InputMaximumSalary.Enabled;

            Actions actions = new Actions(driver);

            if (isVisible && isEnabled)
            {
                InputMaximumSalary.Click();
                actions.MoveToElement(InputMaximumSalary).Click().SendKeys("155888").Perform();
            }
            else
            {
                Console.WriteLine("Field is not visible.");
            }

            IWebElement ButtonSave = driver.FindElement(By.CssSelector("button.oxd-button.oxd-button--medium.oxd-button--secondary.orangehrm-left-space:nth-of-type(2)"));

            actions.MoveToElement(ButtonSave).Click().Perform();

            JobDropDownList.Click();
            Task.Delay(3000);
            PayGradesFromDropDownMenu.Click();

            return this;
        }

        public PayGrades DeletePayGrades()
        {
            IWebElement TrashIcon = driver.FindElement(By.XPath("//div[contains(text(),'QA Engineer')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));
            TrashIcon.Click();
            YesDeleteButton.Click();
            return this;

            return this;
        }
    }
}
