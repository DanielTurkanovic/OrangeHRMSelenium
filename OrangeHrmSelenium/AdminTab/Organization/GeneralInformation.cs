using Bogus;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.AdminTab.Organization
{
    public class GeneralInformation
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement OrganizationDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[3]"));
        IWebElement GeneralInformationFromDropDownList => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[1]"));
        IWebElement EditButton => driver.FindElement(By.XPath("//span[@class='oxd-switch-input oxd-switch-input--active --label-left']"));
        IWebElement OrganizationName => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement RegistrationNumber => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[3]"));
        IWebElement TaxId => driver.FindElement(By.XPath("//label[contains(text(),'Tax ID')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::input[contains(@class,'oxd-input oxd-input--active')]"));
        IWebElement Phone => driver.FindElement(By.XPath("//label[contains(text(),'Phone')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::input[contains(@class,'oxd-input oxd-input--active')]"));
        IWebElement Fax => driver.FindElement(By.XPath("//label[contains(text(),'Fax')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement Email => driver.FindElement(By.XPath("//label[contains(text(),'Email')]/ancestor::div[@class='oxd-grid-item oxd-grid-item--gutters']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement AdressStreet1 => driver.FindElement(By.XPath("//label[contains(text(),'Address Street 1')]/ancestor::div[@class='oxd-grid-item oxd-grid-item--gutters']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement AdressStreet2 => driver.FindElement(By.XPath("//label[contains(text(),'Address Street 2')]/ancestor::div[@class='oxd-grid-item oxd-grid-item--gutters']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement City => driver.FindElement(By.XPath("//label[contains(text(),'City')]/ancestor::div[@class='oxd-grid-item oxd-grid-item--gutters']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement StateProvince => driver.FindElement(By.XPath("//label[contains(text(),'State/Province')]/ancestor::div[@class='oxd-grid-item oxd-grid-item--gutters']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement ZipPostalCode => driver.FindElement(By.XPath("//label[contains(text(),'Zip/Postal Code')]/ancestor::div[@class='oxd-grid-item oxd-grid-item--gutters']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement Country => driver.FindElement(By.XPath("//div[@class='oxd-select-text--after']"));
        IWebElement ChooseCountry => driver.FindElement(By.XPath("//span[contains(text(), 'Canada')]"));
        IWebElement Notes => driver.FindElement(By.XPath("//label[contains(text(),'Notes')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::textarea[@class='oxd-textarea oxd-textarea--active oxd-textarea--resize-vertical']"));
        IWebElement Save => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']"));

        // Constructor
        public GeneralInformation(IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public GeneralInformation EditGeneralInformation()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            ExtentReporting.Instance.LogInfo($"Editing General Information");

            AdminTabButton.Click();
            OrganizationDropDownList.Click();
            GeneralInformationFromDropDownList.Click();
            commonTools.HighlightElement(EditButton, driver);
            EditButton.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Thread.Sleep(1000);


            var faker = new Faker();
            var registrationNumber = faker.Random.AlphaNumeric(5);
            var taxId = faker.Random.Int(1,11);
            var fax = faker.Phone.PhoneNumber();
            var email = faker.Internet.Email();
            var adress = faker.Address.ToString();
            var province = faker.Address.CitySuffix();
            var street = faker.Address.StreetName();
            var state = faker.Address.State();
            var zip = faker.Address.ZipCode();
            var notes = faker.Lorem.Text();

            js.ExecuteScript("arguments[0].value = 'Lazy tim';", OrganizationName);
            js.ExecuteScript($"arguments[0].value = '{registrationNumber}';", RegistrationNumber);
            js.ExecuteScript($"arguments[0].value = '{taxId}';", TaxId);
            js.ExecuteScript($"arguments[0].value = '{fax}';", Phone);
            js.ExecuteScript($"arguments[0].value = '{fax}'", Fax);
            js.ExecuteScript($"arguments[0].value = '{email}'", Email);
            js.ExecuteScript($"arguments[0].value = '{adress}'", AdressStreet1);
            js.ExecuteScript($"arguments[0].value = '{province}'", AdressStreet2);
            js.ExecuteScript($"arguments[0].value = '{street}'", City);
            js.ExecuteScript($"arguments[0].value = '{state}'", StateProvince);
            js.ExecuteScript($"arguments[0].value = '{zip}'", ZipPostalCode);
            js.ExecuteScript($"arguments[0].value = '{notes}'", Notes);

            commonTools.ScrollWindow(05);

            Country.Click();
            ChooseCountry.Click();
            commonTools.HighlightElement(Save, driver);

            Save.Click();

            Thread.Sleep(2000);

            return this;
        }
    }
}
