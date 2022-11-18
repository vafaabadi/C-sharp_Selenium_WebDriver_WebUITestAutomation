using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;





namespace WebUITestAutomation
{



    [TestClass]
    public class Kayak_Direct_GoogleSearch
    {

        private int i = 0;
        private int j = 0;


        [TestMethod]
        public void KayakViaGoogleSearch()
        {

            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");


            //starting browser
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://google.com/");

            // Reject all
            driver.FindElementByCssSelector("#W0wltc > div").Click();
            // click on search bar
            driver.FindElementByCssSelector("body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.RNNXgb > div > div.a4bIc > input").Click();
            // type for suggestions
            driver.FindElementByCssSelector("body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.RNNXgb > div > div.a4bIc > input").SendKeys("book fli");

            // list of Google suggestions
            IList<IWebElement> GoogleSuggestions = driver.FindElementsByXPath("/html/body/div[1]/div[3]/form/div[1]/div[1]/div[2]/div[2]/div[2]/div[1]/div/ul/li/div/div/div[1]/span");
            //click on book flights online
            foreach (var GoogleSuggestion in GoogleSuggestions)
            {
                if (GoogleSuggestion.Text.Equals("book flights online"))
                {
                    driver.FindElementByXPath("(/html/body/div[1]/div[3]/form/div[1]/div[1]/div[2]/div[2]/div[2]/div[1]/div/ul/li/div/div/div[1]/span)[" + i + "]").Click();
                    break;
                }
                i++;
            }

            // list of Search Results
            IList<IWebElement> SearchResults = driver.FindElementsByXPath("//*[@id=\"rso\"]/div/div/div/div/div/a/div/cite");
            //click on Kayak website
            foreach (var SearchResult in SearchResults)
            {
                if (SearchResult.Text.Equals("https://www.kayak.co.uk › flights"))
                {
                    j++;
                    driver.FindElementByXPath("(//*[@id=\"rso\"]/div/div/div/div/div/a/div/cite)[" + j + "]").Click();
                    break;
                }
                j++;
            }


            Thread.Sleep(4000);
            // click on "Accept" for privacy message box
            driver.FindElementByXPath("/html/body/div[4]/div/div[3]/div/div/div[2]/div/div/div[1]/button").Click();
            Thread.Sleep(3000);
            // click on To? box to select destination
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/div/div[1]/div/div/section[2]/div/div/div/div/div/div[1]/div[2]/div/div[3]/div/div/input").Click();
            Thread.Sleep(2000);
            // type "Ist" in destination box
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/div/div[1]/div/div/section[2]/div/div/div/div/div/div[1]/div[2]/div/div[3]/div/div/input").SendKeys("Ist");
            Thread.Sleep(2000);
            // assert Istanbul listed
            string Istanbul = driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/ul/li[1]/div/div[2]/div/span[1]").Text;
            Assert.AreEqual(Istanbul, "Istanbul, Turkey");
            // Check box for Istanbul Turkey
            driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/ul/li[1]/div/div[3]/span/span/input").Click();
            // click on Date - flight out
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/div/div[1]/div/div/section[2]/div/div/div/div/div/div[1]/div[2]/div/div[4]/div/div/div/div[1]/span/span[2]").Click();
            Thread.Sleep(3000);
            // click on date 31 January 2023
            driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/div[2]/div/div[2]/div[2]/div[2]/div[37]").Click();
            Thread.Sleep(2000);
            // click on next month arrow for return
            driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/div[2]/div/div[3]/button").Click();
            Thread.Sleep(2000);
            // click on 24 February for return
            driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/div[2]/div/div[2]/div[2]/div[2]/div[26]").Click();
            Thread.Sleep(2000);
            // click on Search icon
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/div/div[1]/div/div/section[2]/div/div/div/div/div/div[1]/div[2]/div/div[5]").Click();
            Thread.Sleep(3000);

            // Search on flight is completed under test case KayakDirect().
            driver.Quit();


        }



        [TestMethod]
        public void KayakDirect()
        {

            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");


            //starting browser
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.kayak.co.uk/flights");


            Thread.Sleep(5000);
            // click on "Accept" for privacy message box
            driver.FindElementByXPath("/html/body/div[4]/div/div[3]/div/div/div[2]/div/div/div[1]/button").Click();
            Thread.Sleep(3000);
            // click on To? box to select destination
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/div/div[1]/div/div/section[2]/div/div/div/div/div/div[1]/div[2]/div/div[3]/div/div/input").Click();
            Thread.Sleep(2000);
            // type "Ist" in destination box
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/div/div[1]/div/div/section[2]/div/div/div/div/div/div[1]/div[2]/div/div[3]/div/div/input").SendKeys("Ist");
            Thread.Sleep(2000);
            // assert Istanbul listed
            string Istanbul = driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/ul/li[1]/div/div[2]/div/span[1]").Text;
            Assert.AreEqual(Istanbul, "Istanbul, Turkey");
            // Check box for Istanbul Turkey
            driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/ul/li[1]/div/div[3]/span/span/input").Click();
            // click on Date - flight out
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/div/div[1]/div/div/section[2]/div/div/div/div/div/div[1]/div[2]/div/div[4]/div/div/div/div[1]/span/span[2]").Click();
            Thread.Sleep(3000);
            // click on date 31 January 2023
            driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/div[2]/div/div[2]/div[2]/div[2]/div[37]").Click();
            Thread.Sleep(2000);
            // click on next month arrow for return
            driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/div[2]/div/div[3]/button").Click();
            Thread.Sleep(2000);
            // click on 24 February for return
            driver.FindElementByXPath("/html/body/div[12]/div/div[2]/div/div[2]/div/div[2]/div[2]/div[2]/div[26]").Click();
            Thread.Sleep(2000);
            // click on Search icon
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/div/div[1]/div/div/section[2]/div/div/div/div/div/div[1]/div[2]/div/div[5]").Click();
            Thread.Sleep(3000);

            //click to order based on "Cheapest"
            driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div/div[2]/div[2]/div/div[2]/div[1]/div[2]/div[2]/div[3]/div/div/div[2]/div[1]/a[1]/div[1]/div").Click();
            Thread.Sleep(3000);
           


            bool isFind = false;
            for (int k = 0; k < 30; k++) 
            {

                // captures names of airlines from the list of flights
                IList<IWebElement> ListOfLights = driver.FindElementsByXPath("/html/body/div[1]/div[1]/main/div/div[2]/div[2]/div/div[2]/div[1]/div[2]/div[5]/div[3]/div[1]/div/div/div/div/div/div/div[3]/div[1]");
                int l = 0;

                foreach (var ListOfLight in ListOfLights)
                {
                    l++;
                    if(ListOfLight.Text.Equals("Turkish Airlines"))
                    {
                        IList<IWebElement> Directs = driver.FindElementsByXPath("(/html/body/div[1]/div[1]/main/div/div[2]/div[2]/div/div[2]/div[1]/div[2]/div[5]/div[3]/div[1]/div/div/div/div/div/div/div[3]/div[1])["+l+"]/../../div[2]/div/ol/li/div/div/div[4]/div[1]");
                        Assert.AreEqual(Directs[0].Text, Directs[1].Text);
                        // is flight out "direct"
                        string isOutDirect = driver.FindElementByXPath("(/html/body/div[1]/div[1]/main/div/div[2]/div[2]/div/div[2]/div[1]/div[2]/div[5]/div[3]/div[1]/div/div/div/div/div/div/div[3]/div[1])["+l+"]/../../div[2]/div/ol/li/div/div/div[4]/div[1]").Text.Trim();
                        Assert.AreEqual(isOutDirect, "direct");
                        isFind = true;
                        string ChepeastFare = driver.FindElementByXPath("(/html/body/div/div/main/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/a/span/span)[" + l + "]").Text.Trim();
                        Console.WriteLine("Cheapest direct flight by Turkish Airlines from London to Istanbul for your specified date is: " + ChepeastFare);
                        Console.WriteLine(l);
                        break;

                    }

                }

                if (isFind)
                {
                    break;
                }
                else
                {
                    // click on "show more results"
                    driver.FindElementByXPath("/html/body/div[1]/div[1]/main/div/div[2]/div[2]/div/div[2]/div[1]/div[3]/div[1]/div/a").Click();
                    Thread.Sleep(5000);
                }


            }


            Assert.IsTrue(isFind, "Unable to find the direct Turkish Airline flight from London to Istanbul for the specified date. Sorry!");


        }















    }
}
