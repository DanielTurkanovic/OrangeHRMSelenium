using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace Utilities.Common
{
    public class CommonTools
    {
        IWebDriver driver;
        public CommonTools(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void HighlightElement(IWebElement element, IWebDriver driver)
        {
            // Keeping the element's original style
            string originalStyle = element.GetAttribute("style");

            // Create a JavaScriptExecutor object that enables the execution of Javascript code
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // Change the style of the element so that it has a 3px thick red dashed border
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 3px solid red; border-style: dashed;");

            // Waiting 2 seconds to see the effect
            Thread.Sleep(2000);

            // Return the element to its original style
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, originalStyle);
        }

        // Method which is used to scroll the page
        public void ScrollWindow(int value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, " + value + ");");
        }

        //Method which is used to take text from alert window
        public string WaitForAlertText(IWebDriver driver, TimeSpan timeout)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout);

                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
                string alertText = alert.Text;
                alert.Accept();

                return alertText;
            }
            catch (WebDriverTimeoutException)
            {
                throw new TimeoutException("Alert is not present within the specified time.");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while waiting for the alert.", ex);
            }
        }
    }
}
