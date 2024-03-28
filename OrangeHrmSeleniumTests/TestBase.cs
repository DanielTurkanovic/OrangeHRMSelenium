using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OrangeHrmSelenium.AdminTab.Job  ;
using OrangeHrmSelenium.AdminTab.Organization;
using OrangeHrmSelenium.AdminTab.UserManagement;
using OrangeHrmSelenium.Login;
using OrangeHrmSelenium.Logout;
using Utilities.Common;
using Utilities.Extent;

namespace OrangeHrmSeleniumTests
{
    public class TestBase
    {
        public enum FormType
        {
            Login,
            Logout,
            UserManagement,
            JobCategories,
            EmploymentStatus,
            PayGrades,
            WorkShifts,
            JobTitles,
            Locations
        }
        protected IWebDriver Driver { get; private set; }
        protected Browser Browser { get; private set; }
        protected FormType CurrentFormType { get; private set; }
        protected object WebForm { get; private set; }
        private Login loginForm;
        private Logout logoutForm;

        [SetUp]
        public void Setup()
        {
            ExtentReporting.Instance.CreateTest(TestContext.CurrentContext.Test.MethodName);

            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            Browser = new Browser(Driver);
            loginForm = new Login(Driver);
            logoutForm = new Logout(Driver);

            LoginValidation("Admin", "admin123");
            SetFormTypeFromTestClass();

            SwitchToForm(CurrentFormType);

        }

        private void LoginValidation(string username, string password)
        {
            ExtentReporting.Instance.LogInfo("Starting test - User name validation");

            var loginForm = new Login(Driver); 
            loginForm.LoginToPage(username, password); 
        }


        private void PageLogout()
        {
            ExtentReporting.Instance.LogInfo("Starting test - Logout from the application");

            var logoutForm = new Logout(Driver); 
            logoutForm.LogoutFromPage(); 
        }

        private void SetFormTypeFromTestClass()
        {
            string testClassName = TestContext.CurrentContext.Test.ClassName;
            if (testClassName.Equals("OrangeHrmSeleniumTests.UserManagementTests.UserManagementTests"))
            {
                CurrentFormType = FormType.UserManagement;
            }
            else if (testClassName.Equals("OrangeHrmSeleniumTests.JobTests.JobCategoriesTest"))
            {
                CurrentFormType = FormType.JobCategories;
            }
            else if (testClassName.Equals("OrangeHrmSeleniumTests.JobTests.EmploymentStatusTests"))
            {
                CurrentFormType = FormType.EmploymentStatus;
            }
            else if (testClassName.Equals("OrangeHrmSeleniumTests.JobTests.PayGradesTests"))
            {
                CurrentFormType = FormType.PayGrades;
            }
            else if (testClassName.Equals("OrangeHrmSeleniumTests.JobTests.WorkShiftsTests"))
            {
                CurrentFormType = FormType.WorkShifts;
            }
            else if (testClassName.Equals("OrangeHrmSeleniumTests.JobTests.JobTitlesTests"))
            {
                CurrentFormType = FormType.JobTitles;
            }
            else if (testClassName.Equals("OrangeHrmSeleniumTests.Organization.LocationsTests"))
            {
                CurrentFormType = FormType.Locations;
            }
        }

        public void SwitchToForm(FormType formType)
        {
            CurrentFormType = formType;

            switch (CurrentFormType)
            {
                case FormType.Login:
                    WebForm = new Login(Driver);
                    break;
                case FormType.Logout:
                    WebForm = new Logout(Driver);
                    break;
                case FormType.UserManagement:
                    WebForm = new UserManagement(Driver);
                    break;
                case FormType.JobCategories:
                    WebForm = new JobCategories(Driver);
                    break;
                case FormType.EmploymentStatus:
                    WebForm = new EmploymentStatus(Driver);
                    break;
                case FormType.PayGrades:
                    WebForm = new PayGrades(Driver);
                    break;
                case FormType.WorkShifts:
                    WebForm = new WorkShifts(Driver);
                    break;
                case FormType.JobTitles:
                    WebForm = new JobTitles(Driver);
                    break;
                case FormType.Locations:
                    WebForm = new Locations(Driver);
                    break;
            }
        }

        [TearDown]
        public void TearDown()
        {
            PageLogout();
            EndTest();
            ExtentReporting.Instance.EndReporting();
            Driver.Quit();
        }

        private void EndTest()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            switch (testStatus)
            {
                case TestStatus.Failed:
                    ExtentReporting.Instance.LogInfo($"Test has failed {message}");
                    break;
                case TestStatus.Skipped:
                    ExtentReporting.Instance.LogFail($"Test skipped {message}");
                    break;
                default:
                    break;
            }

            //extent report
            ExtentReporting.Instance.LogScreenshot("Ending test", Browser.GetScreenshot());
            ExtentReporting.Instance.LogScreenshot("Save screenshot", Browser.SaveScreenshot());
        }
    }
}