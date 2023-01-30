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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitTestProject1.LibertyJobSearch_Pages;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WindowsInput;
using WindowsInput.Native;



namespace WebUITestAutomation
{
    [TestClass]
    public class LibertyJobSearch
    {
        float pageCountCompleted;
        string WinDriver = "C:\\Program Files\\Windows Application Driver\\WinAppDriver.exe";


        IWebDriver driver;
        FirstPublicMap Public;
        CurrentOpportunitiesMap Opportunities;
        SearchMap SearchP;
        PersonalInfoMap PersonalInfo;




        [TestMethod]
        public void SubmitAppForPortfolioManagerPosition()
        {
            //Run WinAppDriver
            Process.Start(@WinDriver);
            DesiredCapabilities desktopCapabilities= new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);


            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");


            //starting browser
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl("https://www.libertyspecialtymarkets.com/gb-en");

            var Cookies = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            Cookies.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"onetrust-accept-btn-handler\"]")));

            driver.FindElementByXPath("//*[@id=\"onetrust-accept-btn-handler\"]").Click();

            var CareersLoad = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            CareersLoad.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__next\"]/div/div[1]/header/nav/div[4]/div/nav/ol/li[1]/div/a/span")));


            Public = new FirstPublicMap(driver);
            //click on careers
            Public.Careers.Click();

            Thread.Sleep(2000);

            // click on current opportunities
            IWebElement CurrOpt = driver.FindElement(By.XPath("//*[@id=\"pane-primary-navigator\"]/div[2]/p"));
            //Creating object of an Actions class
            Actions action = new Actions(driver);
            //Performing the mouse hover action on the target element.
            action.MoveToElement(CurrOpt).Perform();

            Thread.Sleep(2000);

            Opportunities = new CurrentOpportunitiesMap(driver);
            //click on open positions
            Opportunities.OpenPositions.Click();

            //  6 postions listed per page
            //  /html/body/div/main/div/div/section/div[3]/div[2]/article/div/div/h3/a

            Thread.Sleep(5000);

            driver.SwitchTo().Window(driver.WindowHandles.Last());

            SearchP = new SearchMap(driver);

            SearchP.KeywordsBox.Click();
            SearchP.KeywordsBox.SendKeys("Portfolio Manager");
            SearchP.SearchBttn.Click();

            Thread.Sleep(2000);

            string total = driver.FindElementByXPath("//*[contains(text(),'results')]").Text;
            string[] count = total.Split(' ');
            //string[] totalValues = count[0].Split('1');
            float pageCount = int.Parse(count[0]) / 6;


            if (pageCount <= 1)
            {
                pageCountCompleted = 1;
            }
            else
            {
                pageCountCompleted = pageCount + 1;
            }


            bool isFind = false;            
            int l = 0;

            for (int k = 0; k < pageCountCompleted; k++)
            {

                IList<IWebElement> positions = driver.FindElements(By.XPath("/html/body/div/main/div/div/section/div[3]/div[2]/article/div/div/h3/a"));

                foreach (IWebElement position in positions)
                {

                    l++;
                    if (position.Text.Equals("Portfolio Manager"))
                    {
                        string isLocation = driver.FindElementByXPath("/html/body/div/main/div/div/section/div[3]/div[2]/article[" + l + "] /div/div/div/span[1]").Text.Trim();
                        Assert.AreEqual(isLocation, "Spain");
                        isFind = true;
                        //click on Apply for Portfolio Manager
                        driver.FindElementByXPath("/html/body/div/main/div/div/section/div[3]/div[2]/article[" + l + "]/div/div/a").Click();
                        break;
                    }
                }
                if (isFind)
                {
                    break;
                }
                driver.FindElementByXPath("//*[@id=\"main\"]/div/div/section/div[3]/div[1]/div[3+" + k + "]/a[6+" + k + "]").Click();
                Thread.Sleep(3000);


            }

            Assert.IsTrue(isFind, "Your desired position at desired location is not listed at the moment. Sorry!");

            driver.SwitchTo().Window(driver.WindowHandles.Last());

            Thread.Sleep(3000);

            PersonalInfo = new PersonalInfoMap(driver);

            PersonalInfo.FirstNameBox.Click();
            PersonalInfo.FirstNameBox.SendKeys("test");

            PersonalInfo.LastNameBox.Click();
            PersonalInfo.LastNameBox.SendKeys("automation");

            PersonalInfo.PhoneNumberBox.Click();
            PersonalInfo.PhoneNumberBox.SendKeys("0123456789");

            PersonalInfo.EmailBox.Click();
            PersonalInfo.EmailBox.SendKeys("test.automation@gmail.com");

            PersonalInfo.CountryDropDown.Click();
            driver.FindElementByXPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[5]/select/option[15]").Click();   //Austria

            PersonalInfo.IndustryDropDown.Click();
            driver.FindElementByXPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[6]/select/option[24]").Click();  // private equity

            PersonalInfo.CVchoosefile.Click();

            Thread.Sleep(3000);

            session.FindElementByXPath("//*[@Name=\"ID\"]").Click();

            Thread.Sleep(3000);

            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);


            Thread.Sleep(3000);

            PersonalInfo.TermsConditionsBox.Click();

            PersonalInfo.NextBttn.Click();

            Thread.Sleep(3000);

            // wait for the confirm submission load
            var SubmitPage = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            SubmitPage.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[1]/div[1]/div")));

            //submit bttn
            driver.FindElementByXPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[2]/fieldset/div/button").Click();

            Thread.Sleep(3000);

            string isPortfolioManager = driver.FindElementByXPath("/html/body/div/main/div/div/section/div/div/div[1]/article/div[2]/h4/a").Text.Trim();
            Assert.AreEqual(isPortfolioManager, "Portfolio Manager");

            string isSpain = driver.FindElementByXPath("/html/body/div/main/div/div/section/div/div/div[1]/article/div[2]/p").Text.Trim();
            Assert.AreEqual(isSpain, "Spain");

            Console.WriteLine("Application succesfully submitted. Good luck!");

            driver.Quit();




            //string total = driver.FindElementByXPath("//*[contains(text(),'1-6')]").Text;
            //string[] count = total.Split(' ');
            ////string[] totalValues = count[2].Split('9');
            //float pageCount = int.Parse(count[2]) / 6;
            //float pageCountCompleted = pageCount + 1;

            //000
            //001
            //002
            //003


            //for (int k = 0; k < pageCountCompleted; k++)
            //{

            //    IList<IWebElement> positions = driver.FindElements(By.XPath("/html/body/div/main/div/div/section/div[3]/div[2]/article/div/div/h3/a"));



            //    foreach (IWebElement position in positions)
            //    {

            //        l++;
            //        if (position.Text.Equals("Portfolio Manager"))
            //        {
            //            isFind = true;
            //            //click on Apply for Portfolio Manager
            //            driver.FindElementByXPath("//*[@id=\"main\"]/div/div/section/div[3]/div[2]/article[l]/div/div[1]/h3/a/../../../div[2]/a").Click();
            //            break;
            //        }
            //    }
            //    if (isFind)
            //    {
            //        break;
            //    }
            //    driver.FindElementByXPath("//*[@id=\"main\"]/div/div/section/div[3]/div[1]/div[3+"+k+"]/a[6+"+k+"]").Click();
            //    Thread.Sleep(3000);


            //}
            //Console.WriteLine(l);
            //Console.WriteLine(isFind);

            //Assert.IsTrue(isFind, "Your desired position is not listed at the moment. Sorry!");



            // 1st   /html/body/div/main/div/div/section/div[3]/div[1]/div[3]/a[6]
            // 2nd   /html/body/div/main/div/div/section/div[3]/div[1]/div[4]/a[7]
            // 3rd   /html/body/div/main/div/div/section/div[3]/div[1]/div[4]/a[8]
            // 4th   /html/body/div/main/div/div/section/div[3]/div[1]/div[4]/a[9]
            // 5th   /html/body/div/main/div/div/section/div[3]/div[1]/div[4]/a[10]
            // 6th   /html/body/div/main/div/div/section/div[3]/div[1]/div[4]/a[10]
            // 7th   /html/body/div/main/div/div/section/div[3]/div[1]/div[4]/a[10]
            // 8th   /html/body/div/main/div/div/section/div[3]/div[1]/div[4]/a[9]
            // 9th   'Next' hyperlink disappears

            // 1     /html/body/div/main/div/div/section/div[3]/div[1]/div[3]/span
            // 2     /html/body/div/main/div/div/section/div[3]/div[1]/div[3]/a[1]
            // 3     /html/body/div/main/div/div/section/div[3]/div[1]/div[3]/a[2]
            // 4     /html/body/div/main/div/div/section/div[3]/div[1]/div[3]/a[3]
            // 5     /html/body/div/main/div/div/section/div[3]/div[1]/div[3]/a[4]
            // 6     /html/body/div/main/div/div/section/div[3]/div[1]/div[3]/a[5]










        }
    }

}
