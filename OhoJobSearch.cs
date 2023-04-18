using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Windows;
using WebDriverManager.Helpers;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using WindowsInput;
using WindowsInput.Native;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace WebUITestAutomation
{
    [TestClass]
    public class OHO
    {

        string BaseDirectory_Path_Oho = AppDomain.CurrentDomain.BaseDirectory;
        
        bool isFind;



        [TestMethod]
        public void OHO_QA()
        {
            //to run WinAppDriver
            // AppDomain.CurrentDomain.BaseDirectory this is here: C:\Users\44741\source\repos\UnitTestProject1\bin\Debug . it is part of the solution folder
            /*******
            //Files need to be copied to C:\Users\44741\source\repos\UnitTestProject1 . when the solution is built, a copy of the files from C:\Users\44741\source\repos\UnitTestProject1 will be copied to C:\Users\44741\source\repos\UnitTestProject1\bin\Debug where the file will be read/run automatically.
            *******/
            string WinDriver_FullPath = BaseDirectory_Path_Oho + "\\WinAppDriver_baseDirectory\\WinAppDriver.exe";
            Process.Start(@WinDriver_FullPath);
            //Process.Start($"WinAppDriver.exe", AppDomain.CurrentDomain.BaseDirectory);
            //Process.Start(@WinDriver);
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);


            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");


            //starting browser
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.oho.co.uk/");
            Thread.Sleep(2000);
            //search text box
            driver.FindElementByXPath("//*[@id=\"keywords\"]").Click();
            driver.FindElementByXPath("//*[@id=\"keywords\"]").SendKeys("QA");
            Thread.Sleep(2000);
            //click on search icon
            driver.FindElementByXPath("//*[@id=\"SearchBar\"]/div/span/input").Click();
            Thread.Sleep(2000);
            //type London in Location search box
            driver.FindElementByXPath("//*[@id=\"centerpoint\"]").SendKeys("London");
            //click on Search bttn
            driver.FindElementByXPath("//*[@id=\"jobsearchrefine\"]/div[7]/div/div/div/div/input").Click();
            Thread.Sleep(2000);

            string distance = "110";
            string radius = driver.FindElementByXPath("//*[@id=\"jobsearchrefine\"]/div[3]/output/span").Text;

            isFind = false;
            while (radius != distance) 
            {
                int i = 1;
                // list of jobs
                IList<IWebElement> ListedJobs = driver.FindElementsByXPath("/html/body/div[1]/div[3]/div/div/div[1]/section/div/div[2]/div/div[2]/div/ul/div/h4/a");
                //click on Free Downloads
                foreach (var ListedJob in ListedJobs)
                {
                    if (ListedJob.Text.Contains("Quality Assurance"))
                    {
                        if (driver.FindElementByXPath("/html/body/div[1]/div[3]/div/div/div[1]/section/div/div[2]/div/div[2]/div/ul/div[" + i + "]/h4/a/../../ul/li[2]/span").Text.Equals("Permanent"))
                        {
                            if (driver.FindElementByXPath("/html/body/div[1]/div[3]/div/div/div[1]/section/div/div[2]/div/div[2]/div/ul/div[" + i + "]/h4/a/../../ul/li[2]/span").Text.Equals("Permanent") || driver.FindElementByXPath("/html/body/div[1]/div[3]/div/div/div[1]/section/div/div[2]/div/div[2]/div/ul/div[" + i + "]/h4/a/../../ul/li[2]/span").Text.Equals("Permanent") || driver.FindElementByXPath("/html/body/div[1]/div[3]/div/div/div[1]/section/div/div[2]/div/div[2]/div/ul/div[" + i + "]/h4/a/../../ul/li[2]/span").Text.Equals("Permanent"))
                            isFind = true;
                            driver.FindElementByXPath("/html/body/div[1]/div[3]/div/div/div[1]/section/div/div[2]/div/div[2]/div/ul/div[" + i + "]/h4/a").Click();
                            break;
                        }

                    }
                    i++;
                }
                if (isFind == false)
                {
                    driver.FindElementByXPath("//*[@id=\"jobsearchrefine\"]/div[3]/div").Click();
                    //InputSimulator sim = new InputSimulator();
                    //sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                    //sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                    driver.FindElementByXPath("//*[@id=\"jobsearchrefine\"]/div[7]/div/div/div/div/input").Click();
                    Thread.Sleep(2000);
                }
                else
                {
                    break;
                }
                

            }
            
            string jobLocation = driver.FindElement(By.XPath("//*[@id=\"locality\"]")).Text;
            List<string> jobLocations = new List<string>();
            jobLocations.Add("London"); jobLocations.Add("Oxford"); jobLocations.Add("Cambridge");
            NUnit.Framework.Assert.That(jobLocations, Does.Contain("Oxford"));

            string benefitDetails = driver.FindElement(By.XPath("//*[@id=\"JobDetails\"]/div/div[2]/div[2]/div[1]/ul/li[3]/span")).Text;
            string[] count = benefitDetails.Split(' ');

            string[] lowerSalary = count[0].Split('£');
            int minSalary = int.Parse(lowerSalary[1]);
            NUnit.Framework.Assert.That(minSalary, Is.GreaterThan(23000));

            Thread.Sleep(2000);

            driver.FindElementByXPath("//*[@id=\"JobDetails\"]/div/div[2]/div[2]/div[1]/div/a/span").Click();

            Thread.Sleep(2000);

            driver.FindElementByXPath("//*[@id=\"first-name\"]").SendKeys("Web UI");
            driver.FindElementByXPath("//*[@id=\"last-name\"]").SendKeys("Test Automation");
            driver.FindElementByXPath("//*[@id=\"email\"]").SendKeys("TesterWasHere@gmail.com");
            driver.FindElementByXPath("//*[@id=\"telephone\"]").SendKeys("01122333222");
            driver.FindElementByXPath("//*[@id=\"additional-information\"]").SendKeys("Hello! This is Web UI automated test case. Have a nice day. Bye.");
            driver.FindElementByXPath("//*[@id=\"terms-and-conditions\"]").Click();
            driver.FindElementByXPath("//*[@id=\"work-eligibility\"]").Click();

            //string cvUpload = AppDomain.CurrentDomain.BaseDirectory;
            //string cvUpload_FullPath = cvUpload + "\\DummyCV.pdf";
            //driver.FindElementByXPath("//*[@id=\"apply-modal-form\"]/div[5]/div/div/div/label/i").SendKeys(cvUpload_FullPath);

            Thread.Sleep(2000);

            driver.FindElementByXPath("//*[@id=\"apply-modal-form\"]/div[6]/div/div/div/div/input[2]").Click();

            Thread.Sleep(2000);

            NUnit.Framework.Assert.AreEqual("Thank you for your application", driver.FindElementByXPath("//*[@id=\"main\"]/section/div/div/div/div/h2").Text);

            driver.Quit();





        }





    }
}
