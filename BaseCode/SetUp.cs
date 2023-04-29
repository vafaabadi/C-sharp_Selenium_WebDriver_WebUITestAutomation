using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using NUnit.Framework;
using System;

namespace WebUITestAutomation
{



    [SetUpFixture]
    public class SetUp
    {

        string BaseDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {


            String reportPath = BaseDirectory_Path; 
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            Base.extent = new ExtentReports();
            Base.extent.AttachReporter(htmlReporter);
            Base.extent.AddSystemInfo("Host name", "Local host");
            Base.extent.AddSystemInfo("Environment", "QA");
            Base.extent.AddSystemInfo("User name", "Vafa Abadi");



        }


        [OneTimeTearDown]
        public void AfterAllTests()
        {
            Base.extent.Flush();

        }







    }
}
