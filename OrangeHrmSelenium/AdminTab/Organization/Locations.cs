using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Security.Cryptography;
using System;
using Bogus;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using OpenQA.Selenium.DevTools.V120.HeapProfiler;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.AdminTab.Organization
{
    public class Locations
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement OrganizationDropDownList => driver.FindElement(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[3]"));
        IWebElement LocationsFromDropDownList => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-link'])[2]"));
        IWebElement AddButton => driver.FindElement(By.XPath("//i[@class='oxd-icon bi-plus oxd-button-icon']"));
        IWebElement Name => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
        IWebElement City => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[3]"));
        IWebElement State => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[4]"));
        IWebElement ZipCode => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[5]"));
        IWebElement Country => driver.FindElement(By.XPath("//div[@class='oxd-select-text-input']"));
        IWebElement CountryDropDownMenu => driver.FindElement(By.XPath("//span[contains(text(), 'Canada')]"));

        IWebElement Phone => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[6]"));
        IWebElement parentElementAddress => driver.FindElement(By.ClassName("oxd-input-group__label-wrapper"));
        IWebElement Address => parentElementAddress.FindElement(By.XPath("//textarea[@placeholder='Type here ...']"));
        IWebElement Notes => driver.FindElement(By.XPath("(//textarea[@placeholder='Type here ...'])[2]"));
        IWebElement Save => driver.FindElement(By.XPath("//button[@type='submit']"));
        IWebElement TrashIcon => driver.FindElement(By.XPath("//div[contains(text(),'Donald Duck')]/ancestor::div[@role='row']/descendant::i[contains(@class,'oxd-icon bi-trash')]"));

        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));
        // Constructor
        public Locations(IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public Locations AddLocations()
        {
            ExtentReporting.Instance.LogInfo("Adding location");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(AdminTabButton));

            AdminTabButton.Click();
            OrganizationDropDownList.Click();
            LocationsFromDropDownList.Click();
            AddButton.Click();

            var faker = new Faker();
            string cityName = faker.Address.City();
            string stateName = faker.Address.State();
            string zipCode = faker.Address.ZipCode();
            string address = faker.Address.FullAddress();

            Name.SendKeys("Donald Duck");

            Actions actions = new Actions(driver);
            actions.MoveToElement(City).Click().SendKeys(cityName).Build().Perform();


            City.SendKeys(cityName);
            State.SendKeys(stateName);
            ZipCode.SendKeys(zipCode);
            Phone.SendKeys("154 654");

            Country.Click();
            wait.Until(driver => CountryDropDownMenu.Displayed && CountryDropDownMenu.Enabled);
            actions.MoveToElement(CountryDropDownMenu).Click().Perform();

            actions.MoveToElement(Address).Click().SendKeys(address).Perform();

            wait.Until(driver => Notes.Displayed && Notes.Enabled);
            actions.MoveToElement(Notes).Click().SendKeys("Automation test").Perform();

            commonTools.HighlightElement(Save, driver);
            Save.Click();
            Task.Delay(3000);

            return this;
        }

        public Locations DeleteLocation()
        {
            ExtentReporting.Instance.LogInfo("Delete location");

            TrashIcon.Click();
            commonTools.HighlightElement(YesDeleteButton, driver);
            YesDeleteButton.Click();
            Task.Delay(3000);

            return this;
        }
    }
}
