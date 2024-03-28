using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.AdminTab.Job
{
    public class JobTitles
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement JobDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[2]"));
        IWebElement JobTitlesFromDropDownList => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[1]"));
        IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));
        IWebElement JobTitle => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement JobDescription => driver.FindElement(By.XPath("(//textarea[@class='oxd-textarea oxd-textarea--active oxd-textarea--resize-vertical'])[1]"));
        IWebElement BrowseButton => driver.FindElement(By.XPath("//input[@type='file']"));
        IWebElement Note => driver.FindElement(By.XPath("//textarea[@placeholder='Add note']"));
        IWebElement SaveButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        IWebElement TrashIcon => driver.FindElement(By.XPath("//div[contains(text(),'Automation test')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));

        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));

        // Constructor
        public JobTitles (IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public JobTitles AddJobTitles()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            ExtentReporting.Instance.LogInfo("Adding Job Title");

            AdminTabButton.Click();
            JobDropDownList.Click();
            JobTitlesFromDropDownList.Click();
            AddButton.Click();
            JobTitle.SendKeys("Automation test");
            JobDescription.SendKeys("Automation test");

            var imageFileName = "5506088.jpg";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageFileName);
            BrowseButton.SendKeys(imagePath);

            commonTools.ScrollWindow(500);
            Note.SendKeys("Automation test");
            commonTools.HighlightElement(SaveButton, driver);
            SaveButton.Click();
            Task.Delay(3000);

            return this;
        }

        public JobTitles DelateJobTitles()
        {
            ExtentReporting.Instance.LogInfo("Delate Job Title");

            TrashIcon.Click();
            commonTools.HighlightElement(YesDeleteButton, driver);  
            YesDeleteButton.Click();
            Task.Delay(3000);

            return this;
        }
    }
}
