using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Reflection;

namespace Utilities.Extent
{
    public class ExtentReporting
    {
        private static ExtentReporting? instance = null;
        private static readonly object myLock = new object();
        private ExtentReports extentReports;
        private ExtentTest extentTest;

        private ExtentReporting() { }

        public static ExtentReporting Instance
        {
            get
            {
                lock (myLock)
                {
                    if (instance == null)
                    {
                        instance = new ExtentReporting();
                    }
                    return instance;
                }
            }
        }

        ///<sumarry>
        /// Create ExtentReporting and attach ExtentHtmlReporter
        ///</sumarry>
        ///<returns></returns>

        private ExtentReports StartReporting()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (extentReports == null)
            {
                Directory.CreateDirectory(basePath);

                extentReports = new ExtentReports();
                var htmlReporter = new ExtentSparkReporter(Path.Combine(basePath, "report.html"));

                extentReports.AttachReporter(htmlReporter);
            }

            return extentReports;
        }

        public void CreateTest(string testName)
        {
            extentTest = StartReporting().CreateTest(testName);
        }

        public void EndReporting()
        {
            StartReporting().Flush();
        }

        public void LogInfo(string info)
        {
            extentTest.Info(info);
        }

        public void LogPass(string info)
        {
            extentTest.Pass(info);
        }

        public void LogFail(string info)
        {
            extentTest.Fail(info);
        }

        public void LogScreenshot(string info, string image)
        {
            extentTest.Info(info, MediaEntityBuilder.CreateScreenCaptureFromBase64String(image).Build());
        }
    }
}
