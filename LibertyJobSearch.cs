using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WindowsInput;
using WindowsInput.Native;

namespace WebUITestAutomation
{
    [TestClass]
    public class LibertyJobSearch
    {

        [TestMethod]
        public void SearchQAEngineer()
        {

            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");


            //starting browser
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.libertyspecialtymarkets.com/gb-en");






        }












    }
}
