using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.AdminTab.Job
{
    public class JobCategories
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement JobDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[2]"));
        IWebElement JobCategoriesFromDropDownMenu => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[4]"));
        IWebElement JobCategoryEdit => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-pencil-fill'])[1]"));
        IWebElement NameJobCategoryEdit => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement SaveButton => driver.FindElement(By.XPath("(//button[@type='submit'])"));
        IWebElement TrashIcon => driver.FindElement(By.XPath("//div[contains(text(),'QA Tester')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));

        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));


        // Constructors
        public JobCategories(IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public JobCategories ChoseJobCategoriesFromDropDownMenu() 
        {
            ExtentReporting.Instance.LogInfo("Chose Job Categories from drop down menu");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            AdminTabButton.Click();
            JobDropDownList.Click();
            JobCategoriesFromDropDownMenu.Click();
            JobCategoryEdit.Click();
            Actions actions = new Actions(driver);
            actions.Click(NameJobCategoryEdit)
                   .KeyDown(Keys.Control)
                   .SendKeys("a")
                   .KeyUp(Keys.Control)
                   .SendKeys(Keys.Delete)
                   .SendKeys("QA Tester")
                   .Perform();
            commonTools.HighlightElement(SaveButton, driver);
            SaveButton.Click();

            return this;
        }

        public JobCategories DeleteJobCategories() 
        {
            ExtentReporting.Instance.LogInfo("Delete Job Categories");

            TrashIcon.Click();
            commonTools.HighlightElement(YesDeleteButton, driver);
            YesDeleteButton.Click();
            return this;

            return this;
        }
    }
}
