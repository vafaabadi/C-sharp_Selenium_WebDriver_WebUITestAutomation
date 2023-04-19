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
using NUnit.Framework;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace WebUITestAutomation
{
    [TestClass]
    public class GoogleMaps
    {

        string BaseDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;


        //String WinDriver = "C:\\Program Files\\Windows Application Driver\\WinAppDriver.exe";




        [TestMethod]
        public void GoogleMaps_TravelTime()
        {
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
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.google.com/maps/");
            Thread.Sleep(2000);
            driver.FindElementByXPath("//*[@id=\"yDmH0d\"]/c-wiz/div/div/div/div[2]/div[1]/div[3]/div[1]/div[1]/form[1]/div/div/button/span").Click();
            Thread.Sleep(2000);
            driver.FindElementByXPath("//*[@id=\"searchboxinput\"]").SendKeys("21 Garston Rd, Frome BA11 1RT");
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            //click on Direction icon
            driver.FindElementByXPath("//*[@id=\"QA0Szd\"]/div/div/div[1]/div[2]/div/div[1]/div/div/div[4]/div[1]/button/span/img").Click();
            //reserve destination to starting point
            driver.FindElementByXPath("//*[@id=\"omnibox-directions\"]/div/div[3]/div[2]/button/div").Click();
            Thread.Sleep(2000);
            //type in destination
            driver.FindElementByXPath("//*[@id=\"sb_ifc52\"]/input").SendKeys("31 Springhead, Tunbridge Wells TN2 3NY");
            InputSimulator sim1 = new InputSimulator();
            sim1.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(5000);
            // method of transport: drive
            driver.FindElementByXPath("//*[@id=\"omnibox-directions\"]/div/div[2]/div/div/div/div[2]/button/img").Click();
            Thread.Sleep(2000);
            string driveDuration = driver.FindElementByXPath("//*[@id=\"section-directions-trip-0\"]/div[1]/div[1]/div[1]/div[1]/span[1]").Text;
            string[] count = driveDuration.Split('h');
            int hour = int.Parse(count[0]);
            Thread.Sleep(1000);
            if (hour >= 3)
            {
                string failureMessage = "it takes longer than 2 hours to drive!. Please consider other means of transport";
                NUnit.Framework.Assert.Fail(failureMessage);
            }
            //expand details of the journey
            driver.FindElementByXPath("//*[@id=\"section-directions-trip-details-msg-0\"]").Click();
            Thread.Sleep(2000);
            int i = 0;
            IList<IWebElement> Directions = driver.FindElementsByXPath("/html/body/div/div[9]/div[9]/div/div/div[1]/div[2]/div/div[1]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/span/div/h2");
            foreach(var Direction in Directions)
            {
                string Direct = Direction.Text;
                string[] directCount = Direct.Split(' ');
                if (directCount.Contains("A3") || directCount.Contains("A5"))
                {
                    string failureMessage = "Avoid the fastest route, becuase it takes you through M25!";
                    NUnit.Framework.Assert.Fail(failureMessage);
                }
                Thread.Sleep(2000);

            }

            Console.WriteLine("Please drive carefully. Safe Journey!");

            driver.Quit(); 



            //string firstDirection = driver.FindElementByXPath("//*[@id=\"directions-mode-group-title_0_0\"]").Text;
            //string[] first = firstDirection.Split(' ');
            //if (first.Contains("M25"))
            //{
            //    string failureMessage = "Avoid the fastest drive becuase it takes you through M25!";
            //    NUnit.Framework.Assert.Fail(failureMessage);
            //}


            //int xyz = int.Parse(count[0]);
            //Thread.Sleep(2000);







        }


    }
}