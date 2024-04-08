using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.AdminTab.Qualifications
{
    public class Skills
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement QualificationDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[4]"));
        IWebElement SkillsFromDropDownList => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[1]"));
        IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));
        IWebElement Name => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement Description => driver.FindElement(By.XPath("//textarea[@class='oxd-textarea oxd-textarea--active oxd-textarea--resize-vertical']"));
        IWebElement SaveButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        IWebElement TrashIcon => driver.FindElement(By.XPath("//div[contains(text(),'C# Selenium')]/ancestor::div[@role='row']/descendant::i[contains(@class,'oxd-icon bi-trash')]"));
        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));

        // Constructor
        public Skills (IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public Skills AddAndDeleteSkills()
        {
            ExtentReporting.Instance.LogInfo("Adding Skills");

            var fluentWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException)); 

            fluentWait.Until(driver =>
            {
                try
                {
                    AdminTabButton.Click();
                    QualificationDropDownList.Click();
                    SkillsFromDropDownList.Click();
                    AddButton.Click();
                    Name.SendKeys("C# Selenium");
                    Description.SendKeys("C# Selenium Automation test");
                    commonTools.HighlightElement(SaveButton, driver);
                    SaveButton.Click();
                    Thread.Sleep(3000);

                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            return this;
        }

        public Skills DeleteSkills()
        {
            ExtentReporting.Instance.LogInfo("Deleting Skills");

            TrashIcon.Click();
            commonTools.HighlightElement(YesDeleteButton, driver);
            YesDeleteButton.Click();
            Thread.Sleep(3000);

            return this;
        }
    }
}
