using OpenQA.Selenium;

namespace Utilities.Common
{
    public class Browser
    {
        private IWebDriver driver;
        public Browser(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Capture a screenshot using Selenium IWebDriver
        /// </summary>
        /// <returns></returns>

        public string GetScreenshot()
        {
            var file = ((ITakesScreenshot)driver).GetScreenshot();
            var img = file.AsBase64EncodedString;

            return img;
        }

        public string SaveScreenshot()
        {
            var fileName = Guid.NewGuid().ToString() + ".png";
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "screenshots");
            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, fileName);

            var file = ((ITakesScreenshot)driver).GetScreenshot();
            file.SaveAsFile(filePath);

            return filePath;
        }
    }
}
