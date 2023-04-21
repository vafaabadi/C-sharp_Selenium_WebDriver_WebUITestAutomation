using AngleSharp.Dom;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;




namespace WebUITestAutomation
{
    
    [TestFixture]
    public class Base
    {

        IWebDriver driver;

        string BaseDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;

        public ExtentReports extent;
        public ExtentTest test;


        [OneTimeSetUp] 
        public void SetUp() 
        {

            //string workingDirectory = Environment.CurrentDirectory;
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //String reportPath = projectDirectory + "\\index.html";
            //String reportPath = "C:\\Users\\44741\\Desktop" + "\\TestReport.html";
            String reportPath = BaseDirectory_Path + "\\TestReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath); 
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User name", "Vafa Abadi");


        }

        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(NUnit.Framework.TestContext.CurrentContext.Test.Name);

        }


        [TearDown]
        public void AfterTest(ChromeDriver driver)
        {

            var status = NUnit.Framework.TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = NUnit.Framework.TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("hh_mm_ss") + ".png";

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test Failed", captureScreenshot(driver, fileName));
                test.Log(Status.Fail, "test failed with logtrace" + stacktrace);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {

            }
            extent.Flush();

        }

        public MediaEntityModelProvider captureScreenshot(ChromeDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();

        }


        

    }
}
