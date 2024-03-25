using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeHrmSelenium.Logout
{
    public class Logout
    {
        IWebDriver driver;
        //Locators
        IWebElement UserNameDropDown => driver.FindElement(By.ClassName("oxd-userdropdown-img"));
        IWebElement LogoutFromDropDown => driver.FindElement(By.CssSelector("a[href='/web/index.php/auth/logout']"));
        //Constructor
        public Logout(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Methods
        public Logout LogoutFromPage()
        {
            UserNameDropDown.Click();
            LogoutFromDropDown.Click();

            return this;
        }
    }
}
