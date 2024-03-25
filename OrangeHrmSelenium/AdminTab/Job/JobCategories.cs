using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace OrangeHrmSelenium.AdminTab.Job
{
    public class JobCategories
    {
        IWebDriver driver;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement JobDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[2]"));
        IWebElement JobCategoriesFromDropDownMenu => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[4]"));
        IWebElement JobCategoryEdit => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-pencil-fill'])[1]"));
        IWebElement NameJobCategoryEdit => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement SaveButton => driver.FindElement(By.XPath("(//button[@type='submit'])"));
        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));


        // Constructors
        public JobCategories(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Methods
        public JobCategories ChoseJobCategoriesFromDropDownMenu() 
        {
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
            SaveButton.Click();

            return this;
        }

        public JobCategories DeleteJobCategories() 
        {
            IWebElement TrashIcon = driver.FindElement(By.XPath("//div[contains(text(),'QA Tester')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));
            TrashIcon.Click();
            YesDeleteButton.Click();
            return this;

            return this;
        }
    }
}
