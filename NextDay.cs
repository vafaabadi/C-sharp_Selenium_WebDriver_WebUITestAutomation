//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
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
    
    
    
    
    [TestFixture]
    public class NextDay : Base
    {



        float pageCountCompleted_Liberty;
        //string WinDriver = "C:\\Program Files\\Windows Application Driver\\WinAppDriver.exe";
        string BaseDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;
        String WinDriver = "C:\\Program Files\\Windows Application Driver\\WinAppDriver.exe";


        //IWebDriver driver;
        FirstPublicMap Public;
        CurrentOpportunitiesMap Opportunities;
        SearchMap SearchP;
        PersonalInfoMap PersonalInfo;


        [Test]
        public void PendingAppForPortfolioManagerPosition()
        {
            

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
                pageCountCompleted_Liberty = 1;
            }
            else
            {
                pageCountCompleted_Liberty = pageCount + 1;
            }


            bool isFind = false;
            int l = 0;

            for (int k = 0; k < pageCountCompleted_Liberty; k++)
            {

                IList<IWebElement> positions = driver.FindElements(By.XPath("/html/body/div/main/div/div/section/div[3]/div[2]/article/div/div/h3/a"));

                foreach (IWebElement position in positions)
                {

                    l++;
                    if (position.Text.Equals("Portfolio Manager"))
                    {
                        string isLocation = driver.FindElementByXPath("/html/body/div/main/div/div/section/div[3]/div[2]/article[" + l + "] /div/div/div/span[1]").Text.Trim();
                        Assert.AreEqual(isLocation, "Milan, Italy");
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

            Thread.Sleep(2000);

            // Upload CV to CV box without opening windows popup
            // AppDomain.CurrentDomain.BaseDirectory this is here: C:\Users\44741\source\repos\UnitTestProject1\bin\Debug . it is part of the solution folder
            string CV_ID_Path = AppDomain.CurrentDomain.BaseDirectory;
            string CV_ID_FullPath = CV_ID_Path+"\\ID.jpg";
            driver.FindElementByXPath("(//input[@type='file'])[1]").SendKeys(CV_ID_FullPath);
            /*
            PersonalInfo.CVchoosefile.Click();
            Thread.Sleep(3000);
            session.FindElementByXPath("//*[@Name=\"ID\"]").Click();
            Thread.Sleep(3000);
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            //   <input type="file" id=.....
            //   string File_with_FullPath="C:\Some_Folder\MyFile.txt";
            //   driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(File_with_FullPath);
            */


            Thread.Sleep(3000);

            PersonalInfo.TermsConditionsBox.Click();

            PersonalInfo.NextBttn.Click();

            Thread.Sleep(3000);

            // wait for the confirm submission load
            var SubmitPage = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            SubmitPage.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[1]/div[1]/div")));

            Thread.Sleep(2000);

            ////submit bttn
            //driver.FindElementByXPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[2]/fieldset/div/button").Click();

            //Thread.Sleep(3000);

            //string isPortfolioManager = driver.FindElementByXPath("/html/body/div/main/div/div/section/div/div/div[1]/article/div[2]/h4/a").Text.Trim();
            //Assert.AreEqual(isPortfolioManager, "Portfolio Manager");

            //string isSpain = driver.FindElementByXPath("/html/body/div/main/div/div/section/div/div/div[1]/article/div[2]/p").Text.Trim();
            //Assert.AreEqual(isSpain, "Spain");

            //Console.WriteLine("Application succesfully submitted. Good luck!");

            driver.Quit();






            //string total = driver.FindElementByXPath("//*[contains(text(),'1-6')]").Text;
            //string[] count = total.Split(' ');
            ////string[] totalValues = count[2].Split('9');
            //float pageCount = int.Parse(count[2]) / 6;
            //float pageCountCompleted = pageCount + 1;

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


        [Test]
        public void SubmitAppForPortfolioManagerPosition()
        {
            

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
                pageCountCompleted_Liberty = 1;
            }
            else
            {
                pageCountCompleted_Liberty = pageCount + 1;
            }


            bool isFind = false;            
            int l = 0;

            for (int k = 0; k < pageCountCompleted_Liberty; k++)
            {

                IList<IWebElement> positions = driver.FindElements(By.XPath("/html/body/div/main/div/div/section/div[3]/div[2]/article/div/div/h3/a"));

                foreach (IWebElement position in positions)
                {

                    l++;
                    if (position.Text.Equals("Portfolio Manager"))
                    {
                        string isLocation = driver.FindElementByXPath("/html/body/div/main/div/div/section/div[3]/div[2]/article[" + l + "] /div/div/div/span[1]").Text.Trim();
                        Assert.AreEqual(isLocation, "Milan. Italy");
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


            string CV_ID_Path = AppDomain.CurrentDomain.BaseDirectory;
            string CV_ID_FullPath = CV_ID_Path + "\\ID.jpg";
            driver.FindElementByXPath("(//input[@type='file'])[1]").SendKeys(CV_ID_FullPath);

            /*
            PersonalInfo.CVchoosefile.Click();
            Thread.Sleep(3000);
            session.FindElementByXPath("//*[@Name=\"ID\"]").Click();
            Thread.Sleep(3000);
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            */


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


        [Test]
        public void FindBook12()
        {
            float pageCountCompleted;


            driver.Navigate().GoToUrl("https://www.bahaibookstore.com/");
            //wait until DSAR Form ison is visible
            Thread.Sleep(2000);


            // list of products
            IList<IWebElement> ProductCategories = driver.FindElementsByXPath("/html/body/form/div[3]/div/div[4]/div[1]/div/div/div[1]/div/div[2]/ul/li/a");
            //click on Free Downloads
            int i = 1;
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
            int j = 1;
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
                        string isPrice = driver.FindElementByXPath("/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[3]/div[2]/div[1]/div/table/tbody/tr[" + l + "]/td/div/table/tbody/tr/td/div/a/div/../../span[8]").Text.Trim();
                        Assert.AreEqual(isPrice, "$0.00");
                        isFind = true;
                        //click on Apply for Portfolio Manager
                        driver.FindElementByXPath("/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[3]/div[2]/div[1]/div/table/tbody/tr[" + l + "]/td/div/table/tbody/tr/td/div/a/div").Click();
                        break;
                    }
                }
                if (isFind)
                {
                    break;
                }
                // Go to next page
                d = k + 2;
                driver.FindElementByXPath("/html/body/form/div[3]/div/div[3]/div[2]/div/div[1]/div/div[3]/div[1]/div/table/tbody/tr/td[1]/div[2]/a[" + d + "]").Click();
                Thread.Sleep(3000);


            }

            Assert.IsTrue(isFind, "Your desired free downloadable book is not listed. Sorry!");


            driver.Quit();



        }





        [Test]
        public void SampleSpareCodes()
        {


            //to run WinAppDriver
            Process.Start(@WinDriver);
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);

            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            //starting browser
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.kayak.co.uk/flights");


            driver.FindElementByXPath("").Click();
            Thread.Sleep(3000);

            session.FindElementByXPath("").Click();

            Thread.Sleep(2000);

            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            Thread.Sleep(2000);

            driver.SwitchTo().Alert().Accept();

            driver.SwitchTo().Frame(0);


            //----------------------------------

            string totalCases = driver.FindElement(By.XPath("//legend[contains(text(),'Cases')]")).Text;
            string[] count = totalCases.Split(' ');
            string[] totalValues = count[1].Split('(');
            float pageCount = int.Parse(totalValues[1]) / 10;
            float pageCountCompleted = pageCount + 1;
            string caseID = "1110011";
            List<string> DMR = new List<string>();
            DMR.Add("C01A"); DMR.Add("C01B"); DMR.Add("C02A"); DMR.Add("C02B");
            DMR.Add("C02C"); DMR.Add("C02D"); DMR.Add("C02E"); DMR.Add("C02F");
            DMR.Add("C09B");

            bool isFind = false;
            int caseNumber = 0;

            for (int i = 1; i <= pageCountCompleted; i++)
            {
                IList<IWebElement> allCases = driver.FindElements(By.XPath("//strong//a"));
                for (int j = 0; j < allCases.Count; j++)
                {
                    if (allCases[j].Text.Equals(caseID))
                    {
                        isFind = true;
                        Console.WriteLine("case " + caseID + " found");
                        caseNumber = j + 1;
                        break;
                    }
                }
                if (isFind)
                    break;
                else
                {
                    driver.FindElement(By.XPath("//a[text()='" + (i + 1) + "']")).Click();
                }
            }

            Assert.IsTrue(isFind, "Unable to find the CaseID. Please check your CaseID:" + caseID);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//div//strong//a[text()='" + caseID + "']/../../..//span[@title='Reason for filing']//following-sibling::span"));
            for (int i = 0; i < DMR.Count; i++)
            {
                bool isMatch = false;
                for (int j = 0; j < elements.Count; j++)
                {
                    if (elements[j].Text.Contains(DMR[i]))
                    {
                        isMatch = true;
                        break;
                    }
                }
                if (isMatch == false)
                {
                    Assert.Fail("DMR value is not matched to the corresponsing case number. Please check the case number is:" + caseID);
                }
            }

            //----------------------------------

            //   Assert.IsTrue(findpage.GetDBName().Contains("Internal Fraud Database"), "Unable to change the database. please check.");   


            //------------------------------------


            string totalFrauds = driver.FindElement(By.XPath("//legend[contains(text(),'Frauds')]")).Text;
            //   string[] count = totalFrauds.Split(' ');
            //   string[] totalValues = count[1].Split('(');
            //   float pageCount = int.Parse(totalValues[1]) / 10;
            //    float pageCountCompleted = pageCount + 1;
            string fraudID = "1001";

            //  bool isFind = false;

            for (int i = 1; i <= pageCountCompleted; i++)
            {
                IList<IWebElement> allCases = driver.FindElements(By.XPath("//b//a"));
                for (int j = 0; j < allCases.Count; j++)
                {
                    if (allCases[j].Text.Equals(fraudID))
                    {
                        isFind = true;
                        Console.WriteLine("fraud " + fraudID + " found");

                        break;
                    }
                }
                if (isFind)
                    break;
                else
                {
                    driver.FindElement(By.XPath("//a[text()='" + (i + 1) + "']")).Click();
                    Thread.Sleep(5000);
                }
            }

            Assert.IsTrue(isFind, "Unable to find the fraudID. Please check your FraudID:" + fraudID);


            //-------------------------------------

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddAdditionalCapability("pageLoadStrategy", "none");
            driver = new ChromeDriver($"{AppDomain.CurrentDomain.BaseDirectory}", chromeOptions);
            driver = new ChromeDriver($"{AppDomain.CurrentDomain.BaseDirectory}");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.google.co.uk");

            //---------------------------------------

            Assert.AreEqual("Mr Search Search - 01 Jan 2000", driver.FindElement(By.XPath("")).Text);

            //----------------------------------------



        }






    }

}
