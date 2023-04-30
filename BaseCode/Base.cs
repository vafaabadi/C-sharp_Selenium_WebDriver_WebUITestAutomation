using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace WebUITestAutomation
{


    [TestFixture]
    public class Base
    {


        public WindowsDriver<WindowsElement> session;
        public ChromeDriver driver;
        
        string BaseDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;

        public NUnit.Framework.TestContext TestContext;
        public static ExtentReports extent;
        public ExtentTest test;



        [SetUp]
        public void StartBrowser()
        {

            test = extent.CreateTest(NUnit.Framework.TestContext.CurrentContext.Test.Name);

            //to run WinAppDriver
            // AppDomain.CurrentDomain.BaseDirectory this is here: C:\Users\44741\source\repos\UnitTestProject1\bin\Debug . it is part of the solution folder
            /*******
            //Files need to be copied to C:\Users\44741\source\repos\UnitTestProject1 . when the solution is built, a copy of the files from C:\Users\44741\source\repos\UnitTestProject1 will be copied to C:\Users\44741\source\repos\UnitTestProject1\bin\Debug where the file will be read/run automatically.
            *******/
            string WinDriver_FullPath = BaseDirectory_Path + "\\WinAppDriver_baseDirectory\\WinAppDriver.exe";
            Process.Start(@WinDriver_FullPath);
            //Process.Start($"WinAppDriver.exe", AppDomain.CurrentDomain.BaseDirectory);
            //Process.Start(@WinDriver);
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);


            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("incognito");
            //options.AddArgument("no-sandbox");


            //starting browser
            driver = new ChromeDriver(options);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);



        }






        [TearDown]
        public void AfterTest()
        {

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("hh_mm_ss") + ".png";

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test Failed", driver is null ? null : captureScreenshot(driver, fileName));
                //test.Fail("Test Failed").AddScreenCaptureFromPath(fileName);
                test.Log(Status.Fail, "test failed with logtrace" + stacktrace);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Pass("Test Passed");
            }
            

        }





        public MediaEntityModelProvider captureScreenshot(ChromeDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();

        }

       







    }
}
