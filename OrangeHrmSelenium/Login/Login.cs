using OpenQA.Selenium;
using Utilities.Extent;

namespace OrangeHrmSelenium.Login
{
    public class Login
    {
        IWebDriver driver;

        //Locators
        IWebElement UserNameInput => driver.FindElement(By.Name("username"));
        IWebElement PasswordInput => driver.FindElement(By.Name("password"));
        IWebElement LoginButton => driver.FindElement(By.XPath("//button[@type='submit']"));

        //Constructor
        public Login(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Methods
        public Login LoginToPage(string username, string password)
        {
            ExtentReporting.Instance.LogInfo("Fill out user name and password filed");

            UserNameInput.SendKeys(username);
            PasswordInput.SendKeys(password);
            LoginButton.Click();

            return this;
        }
    }
}
