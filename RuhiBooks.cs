using System;
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
using OpenQA.Selenium.Interactions;

namespace UnitTestProject1
{
    [TestClass]
    public class RuhiBooks
    {

        String WinDriver = "C:\\Program Files\\Windows Application Driver\\WinAppDriver.exe";
        int i = 1;
        int j = 1;
        float pageCountCompleted;



        [TestMethod]
        public void FindBook12()
        {
            //to run WinAppDriver
            // AppDomain.CurrentDomain.BaseDirectory this is here: C:\Users\44741\source\repos\UnitTestProject1\bin\Debug . it is part of the solution folder
            Process.Start($"WinAppDriver.exe", AppDomain.CurrentDomain.BaseDirectory);
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
            driver.Navigate().GoToUrl("https://www.bahaibookstore.com/");
            //wait until DSAR Form ison is visible
            Thread.Sleep(2000);

            
            // list of products
            IList<IWebElement> ProductCategories = driver.FindElementsByXPath("/html/body/form/div[3]/div/div[4]/div[1]/div/div/div[1]/div/div[2]/ul/li/a");
            //click on Free Downloads
            foreach (var ProductCategorie in ProductCategories)
            {
                if (ProductCategorie.Text.Equals("Free Downloads"))
                {
                    // hover over Free Downloads
                    Actions HoverOver = new Actions(driver);
                    //Performing the mouse hover action on the target element.
                    HoverOver.MoveToElement(driver.FindElement(By.XPath("/html/body/form/div[3]/div/div[4]/div[1]/div/div/div[1]/div/div[2]/ul/li[" + i + "]/a"))).Perform();
                    break;
                }
                i++;
            }

            Thread.Sleep(2000);

            // list of Languages under Free Downloads
            IList<IWebElement> Languages = driver.FindElementsByXPath("/html/body/form/div[3]/div/div[4]/div[1]/div/div/div[1]/div/div[2]/ul/li[13]/ul/li/a");
            //click on English
            foreach (var Language in Languages)
            {
                if (Language.Text.Equals("English"))
                {
                    driver.FindElement(By.XPath("/html/body/form/div[3]/div/div[4]/div[1]/div/div/div[1]/div/div[2]/ul/li[13]/ul/li[" + j + "]/a")).Click();
                    break;
                }
                j++;
            }

            var English = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            English.Until(ExpectedConditions.ElementIsVisible(By.XPath("(/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[2]/div/h1)")));

            

            string total = driver.FindElementByXPath("/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[3]/div[1]/div/table/tbody/tr/td[1]/div[1]/span").Text;
            string[] count = total.Split(' ');
            //string[] totalValues = count[0].Split('1');
            float pageCount = int.Parse(count[5]) / 12;


            if (pageCount <= 1)
            {
                pageCountCompleted = 1;
            }
            else
            {
                pageCountCompleted = pageCount + 1;
            }

            bool isFind = false;
            //int l = 0;
            int d = 0;

            for (int k = 0; k < pageCountCompleted; k++)
            {
                int l = 0;
                IList<IWebElement> TitleOfBooks = driver.FindElements(By.XPath("/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[3]/div[2]/div[1]/div/table/tbody/tr/td/div/table/tbody/tr/td/div/a/div"));

                foreach (IWebElement TitleOfBook in TitleOfBooks)
                {

                    l++;
                    if (TitleOfBook.Text.Contains("Creating a New Mind (Free ePub)"))
                    {
                        string isPrice = driver.FindElementByXPath("/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[3]/div[2]/div[1]/div/table/tbody/tr["+l+"]/td/div/table/tbody/tr/td/div/a/div/../../span[8]").Text.Trim();
                        Assert.AreEqual(isPrice, "$0.00");
                        isFind = true;
                        //click on Apply for Portfolio Manager
                        driver.FindElementByXPath("/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[3]/div[2]/div[1]/div/table/tbody/tr["+l+"]/td/div/table/tbody/tr/td/div/a/div").Click();
                        break;
                    }
                }
                if (isFind)
                {
                    break;
                }
                // Go to next page
                d = k + 2;
                driver.FindElementByXPath("/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[3]/div[1]/div/table/tbody/tr/td[1]/div[2]/a["+d+"]").Click();
                Thread.Sleep(3000);


            }

            Assert.IsTrue(isFind, "Your desired free downloadable book is not listed. Sorry!");


            driver.Quit();



        }



    }
}
