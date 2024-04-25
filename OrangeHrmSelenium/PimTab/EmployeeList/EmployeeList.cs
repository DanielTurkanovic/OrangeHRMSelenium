using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSelenium.PimTab.EmployeeList
{
    public class EmployeeList
    {
        IWebDriver driver;
        CommonTools commonTools;

        // Locators 
        IWebElement PimTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/pim/viewPimModule')]"));
        IWebElement AddEmployeeButton => driver.FindElement(By.XPath("(//a[@class='oxd-topbar-body-nav-tab-item'])[2]"));
        IWebElement PictureUpload => driver.FindElement(By.XPath("//input[@type='file']"));
        IWebElement EmployeeFullName => driver.FindElement(By.XPath("//input[@name='firstName']"));
        IWebElement MiddleName => driver.FindElement(By.XPath("//input[@name='middleName']"));
        IWebElement LastName => driver.FindElement(By.XPath("//input[@name='lastName']"));
        //IWebElement EmployeeId1 => driver.FindElement(By.XPath("(//input[@class=\"oxd-input oxd-input--active\"])[2]"));
        IWebElement EmployeeId => driver.FindElement(By.XPath("//label[contains(text(),'Employee Id')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::input[contains(@class, 'oxd-input oxd-input--active')]"));
        IWebElement CreateLoginDetails => driver.FindElement(By.XPath("//span[@class='oxd-switch-input oxd-switch-input--active --label-right']"));
        IWebElement UserName => driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[3]"));
        IWebElement Password => driver.FindElement(By.XPath("(//input[@type='password'])[1]"));
        IWebElement ConfirmPassword => driver.FindElement(By.XPath("(//input[@type='password'])[2]"));
        IWebElement SaveButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        IWebElement OtherId => driver.FindElement(By.XPath("//label[contains(text(),'Other Id')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement DriversLicenseNumber => driver.FindElement(By.XPath("//label[contains(text(),'License Number')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement LicenseExpiryDate => driver.FindElement(By.XPath("//label[contains(text(),'License Expiry Date')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement ChooseExpiryDate => driver.FindElement(By.XPath("//div[@class='oxd-date-input-link --today']"));
        IWebElement Nationality => driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-caret-down-fill oxd-select-text--arrow'])[1]"));
        IWebElement ChooseNationality => driver.FindElement(By.XPath("//span[contains(text(),'American')]"));
        IWebElement MaritalStatus => driver.FindElement(By.XPath("(//div[@class='oxd-select-text-input'])[2]"));
        IWebElement ChooseMaritalStatus => driver.FindElement(By.XPath("//span[contains(text(),'Married')]"));
        IWebElement DateOfBirth => driver.FindElement(By.XPath("//label[contains(text(),'Date of Birth')]/ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']/descendant::input[@class='oxd-input oxd-input--active']"));
        IWebElement RadioButtonMale => driver.FindElement(By.XPath("(//span[@class='oxd-radio-input oxd-radio-input--active --label-right oxd-radio-input'])[1]"));
        IWebElement SaveButton2 => driver.FindElement(By.XPath("(//button[@type='submit'])[1]"));
        IWebElement AdminTabButton => driver.FindElement(By.XPath("//a[contains(@href, '/web/index.php/admin/viewAdminModule')]"));
        IWebElement TrashIcon => driver.FindElement(By.XPath("//div[contains(text(),'McDuck')]/ancestor::div[@role='row']/descendant::i[contains(@class,'bi-trash')]"));
        IWebElement YesDeleteButton => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin']"));

        // Constructor
        public EmployeeList (IWebDriver driver)
        {
            this.driver = driver;
            commonTools = new CommonTools(driver);
        }

        // Methods
        public EmployeeList AddAndDeleteEmployee()
        {
            ExtentReporting.Instance.LogInfo("Adding Employee");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(PimTabButton));

            PimTabButton.Click();
            AddEmployeeButton.Click();

            var imageFileName = "5506088.jpg";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageFileName);
            PictureUpload.SendKeys(imagePath);

            EmployeeFullName.SendKeys("Scrooge");
            MiddleName.SendKeys("J");
            LastName.SendKeys("McDuck");

            Thread.Sleep(4000);

            int broj;
            string tekst = EmployeeId.GetAttribute("value");
            if (int.TryParse(tekst, out broj))
            {
                broj++;

                wait.Until(ExpectedConditions.TextToBePresentInElementValue(EmployeeId, ""));

                Actions actions = new Actions(driver);
                actions.MoveToElement(EmployeeId)
               .Click()
               .SendKeys(Keys.Home)
               .KeyDown(Keys.Shift)
               .SendKeys(Keys.End)
               .KeyUp(Keys.Shift)
               .SendKeys(Keys.Delete)
               .Perform();

                actions.SendKeys(broj.ToString()).Perform();
            }

            CreateLoginDetails.Click();
            UserName.SendKeys("McDuck");
            Password.SendKeys("Admin123");
            ConfirmPassword.SendKeys("Admin123");
            commonTools.HighlightElement(SaveButton, driver);
            SaveButton.Click();
       
            Thread.Sleep(2000);

            OtherId.SendKeys("1");
            DriversLicenseNumber.SendKeys("2");
            LicenseExpiryDate.Click();
            ChooseExpiryDate.Click();
            Nationality.Click();
            ChooseNationality.Click();
            MaritalStatus.Click();
            ChooseMaritalStatus.Click();

            string datumScript = "arguments[0].value = '12-05-1947';"; 
            ((IJavaScriptExecutor)driver).ExecuteScript(datumScript, DateOfBirth);

            RadioButtonMale.Click();
            SaveButton2.Click();
            AdminTabButton.Click();

            commonTools.ScrollWindow(500);

            TrashIcon.Click();
            commonTools.HighlightElement(YesDeleteButton, driver);
            YesDeleteButton.Click();

            Thread.Sleep(3000);

            return this;
        }

    }
}
