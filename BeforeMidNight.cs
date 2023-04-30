using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Windows;
using WebDriverManager.Helpers;
using WebDriverManager.DriverConfigs.Impl;
using WindowsInput;
using WindowsInput.Native;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Security.Policy;
using NUnit.Framework.Interfaces;

namespace WebUITestAutomation
{



    [TestFixture]
    public class BeforeMidNight : Base
    {

        string BaseDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;

        //String WinDriver = "C:\\Program Files\\Windows Application Driver\\WinAppDriver.exe";




        [Test]
        public void GoogleMaps_TravelTime()
        {
            
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
            if (hour >= 4)
            {
                string failureMessage = "it takes longer than 2 hours to drive!. Please consider other means of transport";
                NUnit.Framework.Assert.Fail(failureMessage);
            }
            //expand details of the journey
            driver.FindElementByXPath("//*[@id=\"section-directions-trip-details-msg-0\"]").Click();
            Thread.Sleep(2000);
            int i = 0;
            IList<IWebElement> Directions = driver.FindElementsByXPath("/html/body/div/div[9]/div[9]/div/div/div[1]/div[2]/div/div[1]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/span/div/h2");
            foreach (var Direction in Directions)
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


        }

        string BaseDirectory_Path_Oho = AppDomain.CurrentDomain.BaseDirectory;





        [Test]
        public void OHO_QA()
        {
            
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
            bool isFind;
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


        [Test]
        public void KayakViaGoogleSearch()
        {
            

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

            int i = 0;
            foreach (var GoogleSuggestion in GoogleSuggestions)
            {
                if (GoogleSuggestion.Text.Equals("book flights online"))
                {
                    driver.FindElementByXPath("(/html/body/div[1]/div[3]/form/div[1]/div[1]/div[2]/div[2]/div[2]/div[1]/div/ul/li/div/div/div[1]/span)[" + i + "]").Click();
                    break;
                }
                i++;
            }

            int j = 0;
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


            foreach (var process in Process.GetProcessesByName("chrome.exe"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("WinAppDriver.exe"))
            {
                process.Kill();
            }






        }






        [Test]
        public void KayakDirect()
        {
            

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
                    if (ListOfLight.Text.Equals("Turkish Airlines"))
                    {
                        IList<IWebElement> Directs = driver.FindElementsByXPath("(/html/body/div[1]/div[1]/main/div/div[2]/div[2]/div/div[2]/div[1]/div[2]/div[5]/div[3]/div[1]/div/div/div/div/div/div/div[3]/div[1])[" + l + "]/../../div[2]/div/ol/li/div/div/div[4]/div[1]");
                        Assert.AreEqual(Directs[0].Text, Directs[1].Text);
                        // is flight out "direct"
                        string isOutDirect = driver.FindElementByXPath("(/html/body/div[1]/div[1]/main/div/div[2]/div[2]/div/div[2]/div[1]/div[2]/div[5]/div[3]/div[1]/div/div/div/div/div/div/div[3]/div[1])[" + l + "]/../../div[2]/div/ol/li/div/div/div[4]/div[1]").Text.Trim();
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


            driver.Quit();


            foreach (var process in Process.GetProcessesByName("chrome.exe"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("WinAppDriver.exe"))
            {
                process.Kill();
            }


        }


        [Test]
        public void DSARform_UpToHooyu()
        {
            
            driver.Navigate().GoToUrl("https://development.cifas.org.uk/");
            //wait until DSAR Form ison is visible
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#saveCookieSetting").Click();
            var ExplicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ExplicitWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#shortcuts > div.social > div.sociallink.search > div.contracted")));
            Thread.Sleep(1000);
            //DSAR icon
            driver.FindElementByCssSelector("#shortcuts > div.social > div.sociallink.identity_protection > div").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#shortcuts > div.social > div.sociallink.identity_protection.open > a").Click();
            //Title
            driver.FindElementByCssSelector("#strTitle").SendKeys("Mr");
            driver.FindElementByCssSelector("#strFirstName").SendKeys("TestAutomation");
            driver.FindElementByCssSelector("#strMiddleName").SendKeys("FirstTestCase");
            driver.FindElementByCssSelector("#strSurname").SendKeys("DSARform");
            driver.FindElementByCssSelector("#blnSimilarName").Click();
            driver.FindElementByCssSelector("#previousNames").Click();
            var PreviousFirstName = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            PreviousFirstName.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#prevNameWrapper > div.previousname-form.previous-name1 > div:nth-child(1) > label")));
            driver.FindElementByCssSelector("#strPrevFirstname1").SendKeys("PreviousFirstName");
            driver.FindElementByCssSelector("#strPrevMiddle1").SendKeys("PreviousMiddleName");
            driver.FindElementByCssSelector("#strPrevSurname1").SendKeys("PreviousSurName");
            driver.FindElementByCssSelector("#dob_day").SendKeys("11");
            driver.FindElementByCssSelector("#sar_form_0 > div.container > div:nth-child(7) > div > input[type=number]:nth-child(2)").SendKeys("11");
            driver.FindElementByCssSelector("#sar_form_0 > div.container > div:nth-child(7) > div > input[type=number]:nth-child(3)").SendKeys("1956");
            //Confirm
            driver.FindElementByCssSelector("#submitBtn").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#strPostcode1").SendKeys("AL1 4RF");
            driver.FindElementByCssSelector("#intYearsAtAddress1").Click();
            driver.FindElementByCssSelector("#addressLookup1Select-button > span.ui-selectmenu-text").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#ui-id-2").Click();
            driver.FindElementByCssSelector("#intYearsAtAddress1").SendKeys("4");
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry1").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry1 > option:nth-child(233)").Click();
            Thread.Sleep(1000);
            //Previous Address
            driver.FindElementByCssSelector("#previousAddresses").Click();
            var PreviousPostCode = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            PreviousPostCode.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#prevAddressWrapper > div.previousaddress-form.previous-address2 > div:nth-child(2) > label")));
            driver.FindElementByCssSelector("#strPostcode2").SendKeys("SE5 0YA");
            driver.FindElementByCssSelector("#intYearsAtAddress2").Click();
            driver.FindElementByCssSelector("#addressLookup2Select-button > span.ui-selectmenu-text").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#ui-id-8").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#intYearsAtAddress2").SendKeys("4");
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry2").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#idCountry2 > option:nth-child(233)").Click();
            Thread.Sleep(3000);
            driver.FindElementByCssSelector("#sar_form_1 > div.d-flex.justify-content-between > button").Click();
            var ContactDetailsPage = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ContactDetailsPage.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#page_full > div > div > h2")));
            driver.FindElementByCssSelector("#strHomeTel").SendKeys("0123456789");
            driver.FindElementByCssSelector("#strMobileTel").SendKeys("078543334231");
            driver.FindElementByCssSelector("#strEmailAddress").SendKeys("testautomation@selenium.com");
            driver.FindElementByCssSelector("#strEmailAddressRepeat").SendKeys("testautomation@selenium.com");
            driver.FindElementByCssSelector("#idBrought").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#sar_form_2 > div.d-flex.justify-content-between > button").Click();
            Thread.Sleep(1000);
            // Summary Page
            bool strFirstName = driver.FindElementByXPath("//*[@id='strFirstName']").Displayed;
            Assert.AreEqual(strFirstName, true);
            bool strPreviousFirstName = driver.FindElementByCssSelector("#strPrevFirstname4").Displayed;
            Assert.AreEqual(strPreviousFirstName, true);
            bool strPostCode = driver.FindElementByCssSelector("#strPostcode1").Displayed;
            Assert.AreEqual(strPostCode, true);
            bool strPreviousPostCode = driver.FindElementByCssSelector("#strPostcode1").Displayed;
            Assert.AreEqual(strPreviousPostCode, true);
            bool strHomeTelephone = driver.FindElementByCssSelector("#strHomeTel").Displayed;
            Assert.AreEqual(strHomeTelephone, true);
            // Consent
            driver.FindElementByCssSelector("#blnEquifaxOptOut").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#blnEquifaxOptOut > option:nth-child(2)").Click();
            Thread.Sleep(1000);
            // Submit
            driver.FindElementByCssSelector("#sar_form_3 > div.d-flex.justify-content-between > button").Click();
            Thread.Sleep(2000);
            // Confirm my identity
            var ConfirmMyIdentity = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ConfirmMyIdentity.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#hooyu > b")));
            //Reference Number
            bool isRefNo = driver.FindElementByXPath("//*[@id='page_full']/div/p[1]").Displayed;
            Assert.AreEqual(isRefNo, true);


            driver.Quit();


            foreach (var process in Process.GetProcessesByName("chrome.exe"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("WinAppDriver.exe"))
            {
                process.Kill();
            }




        }


        [Test]
        public void DSARform_UploadLater()
        {
            
            driver.Navigate().GoToUrl("https://development.cifas.org.uk/");
            //wait until DSAR Form ison is visible
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#saveCookieSetting").Click();
            var ExplicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ExplicitWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#shortcuts > div.social > div.sociallink.search > div.contracted")));
            Thread.Sleep(1000);
            //DSAR icon
            driver.FindElementByCssSelector("#shortcuts > div.social > div.sociallink.identity_protection > div").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#shortcuts > div.social > div.sociallink.identity_protection.open > a").Click();
            //Title
            driver.FindElementByCssSelector("#strTitle").SendKeys("Mr");
            driver.FindElementByCssSelector("#strFirstName").SendKeys("TestAutomation");
            driver.FindElementByCssSelector("#strMiddleName").SendKeys("FirstTestCase");
            driver.FindElementByCssSelector("#strSurname").SendKeys("DSARform");
            driver.FindElementByCssSelector("#blnSimilarName").Click();
            driver.FindElementByCssSelector("#previousNames").Click();
            var PreviousFirstName = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            PreviousFirstName.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#prevNameWrapper > div.previousname-form.previous-name1 > div:nth-child(1) > label")));
            driver.FindElementByCssSelector("#strPrevFirstname1").SendKeys("PreviousFirstName");
            driver.FindElementByCssSelector("#strPrevMiddle1").SendKeys("PreviousMiddleName");
            driver.FindElementByCssSelector("#strPrevSurname1").SendKeys("PreviousSurName");
            driver.FindElementByCssSelector("#dob_day").SendKeys("11");
            driver.FindElementByCssSelector("#sar_form_0 > div.container > div:nth-child(7) > div > input[type=number]:nth-child(2)").SendKeys("11");
            driver.FindElementByCssSelector("#sar_form_0 > div.container > div:nth-child(7) > div > input[type=number]:nth-child(3)").SendKeys("1956");
            //Confirm
            driver.FindElementByCssSelector("#submitBtn").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#strPostcode1").SendKeys("AL1 4RF");
            driver.FindElementByCssSelector("#intYearsAtAddress1").Click();
            driver.FindElementByCssSelector("#addressLookup1Select-button > span.ui-selectmenu-text").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#ui-id-2").Click();
            driver.FindElementByCssSelector("#intYearsAtAddress1").SendKeys("4");
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry1").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry1 > option:nth-child(233)").Click();
            Thread.Sleep(1000);
            //Previous Address
            driver.FindElementByCssSelector("#previousAddresses").Click();
            var PreviousPostCode = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            PreviousPostCode.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#prevAddressWrapper > div.previousaddress-form.previous-address2 > div:nth-child(2) > label")));
            driver.FindElementByCssSelector("#strPostcode2").SendKeys("SE5 0YA");
            driver.FindElementByCssSelector("#intYearsAtAddress2").Click();
            driver.FindElementByCssSelector("#addressLookup2Select-button > span.ui-selectmenu-text").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#ui-id-8").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#intYearsAtAddress2").SendKeys("4");
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry2").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#idCountry2 > option:nth-child(233)").Click();
            Thread.Sleep(3000);
            driver.FindElementByCssSelector("#sar_form_1 > div.d-flex.justify-content-between > button").Click();
            var ContactDetailsPage = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ContactDetailsPage.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#sar_form_2 > div.container > div:nth-child(4) > label")));
            driver.FindElementByCssSelector("#strHomeTel").SendKeys("0123456789");
            driver.FindElementByCssSelector("#strMobileTel").SendKeys("078543334231");
            driver.FindElementByCssSelector("#strEmailAddress").SendKeys("testautomation@selenium.com");
            driver.FindElementByCssSelector("#strEmailAddressRepeat").SendKeys("testautomation@selenium.com");
            driver.FindElementByCssSelector("#idBrought").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#sar_form_2 > div.d-flex.justify-content-between > button").Click();
            Thread.Sleep(1000);
            // Summary Page
            bool strFirstName = driver.FindElementByXPath("//*[@id='strFirstName']").Displayed;
            Assert.AreEqual(strFirstName, true);
            bool strPreviousFirstName = driver.FindElementByCssSelector("#strPrevFirstname4").Displayed;
            Assert.AreEqual(strPreviousFirstName, true);
            bool strPostCode = driver.FindElementByCssSelector("#strPostcode1").Displayed;
            Assert.AreEqual(strPostCode, true);
            bool strPreviousPostCode = driver.FindElementByCssSelector("#strPostcode1").Displayed;
            Assert.AreEqual(strPreviousPostCode, true);
            bool strHomeTelephone = driver.FindElementByCssSelector("#strHomeTel").Displayed;
            Assert.AreEqual(strHomeTelephone, true);
            // Consent
            driver.FindElementByCssSelector("#blnEquifaxOptOut").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#blnEquifaxOptOut > option:nth-child(2)").Click();
            Thread.Sleep(1000);
            // Submit
            driver.FindElementByCssSelector("#sar_form_3 > div.d-flex.justify-content-between > button").Click();
            Thread.Sleep(2000);
            // Confirm my identity
            var ConfirmMyIdentity = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ConfirmMyIdentity.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#hooyu > b")));
            //Reference Number
            bool isRefNo = driver.FindElementByXPath("//*[@id='page_full']/div/p[1]").Displayed;
            Assert.AreEqual(isRefNo, true);
            driver.FindElementByCssSelector("#cifas_upload").Click();
            Thread.Sleep(2000);
            // Upload documents later
            driver.FindElementByXPath("/html/body/div[8]/div[2]/div/form/div/a[2]").Click();
            var UploadLater = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            UploadLater.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[8]/div[2]/div/h2")));
            // assert email address
            string strRmailAddress = driver.FindElementByXPath("/html/body/div[8]/div[2]/div/form/fieldset/div/div[1]/div").Text.Trim();
            Assert.AreEqual(strRmailAddress, "testautomation@selenium.com");
            // Send link
            driver.FindElementByXPath("/html/body/div[8]/div[2]/div/form/fieldset/div/div[2]/div/input").Click();
            Thread.Sleep(2000);
            bool strRefNo = driver.FindElementByXPath("/html/body/div[8]/p[1]").Displayed;
            Assert.AreEqual(strRefNo, true);
            Thread.Sleep(1000);
            bool strExpiryTime = driver.FindElementByXPath("/html/body/div[8]/p[3]").Displayed;
            Assert.AreEqual(strExpiryTime, true);


            driver.Quit();


            foreach (var process in Process.GetProcessesByName("chrome.exe"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("WinAppDriver.exe"))
            {
                process.Kill();
            }



        }



        [Test]
        public void DSARform_ManualUpload()
        {
            
            driver.Navigate().GoToUrl("https://development.cifas.org.uk/");
            //wait until DSAR Form ison is visible
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#saveCookieSetting").Click();
            var ExplicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ExplicitWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#shortcuts > div.social > div.sociallink.search > div.contracted")));
            Thread.Sleep(3000);
            //DSAR icon
            driver.FindElementByCssSelector("#shortcuts > div.social > div.sociallink.identity_protection > div").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#shortcuts > div.social > div.sociallink.identity_protection.open > a").Click();
            //Title
            driver.FindElementByCssSelector("#strTitle").SendKeys("Mr");
            driver.FindElementByCssSelector("#strFirstName").SendKeys("TestAutomation");
            driver.FindElementByCssSelector("#strMiddleName").SendKeys("FirstTestCase");
            driver.FindElementByCssSelector("#strSurname").SendKeys("DSARform");
            driver.FindElementByCssSelector("#blnSimilarName").Click();
            driver.FindElementByCssSelector("#previousNames").Click();
            var PreviousFirstName = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            PreviousFirstName.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#prevNameWrapper > div.previousname-form.previous-name1 > div:nth-child(1) > label")));
            driver.FindElementByCssSelector("#strPrevFirstname1").SendKeys("PreviousFirstName");
            driver.FindElementByCssSelector("#strPrevMiddle1").SendKeys("PreviousMiddleName");
            driver.FindElementByCssSelector("#strPrevSurname1").SendKeys("PreviousSurName");
            driver.FindElementByCssSelector("#dob_day").SendKeys("11");
            driver.FindElementByCssSelector("#sar_form_0 > div.container > div:nth-child(7) > div > input[type=number]:nth-child(2)").SendKeys("11");
            driver.FindElementByCssSelector("#sar_form_0 > div.container > div:nth-child(7) > div > input[type=number]:nth-child(3)").SendKeys("1956");
            //Confirm
            driver.FindElementByCssSelector("#submitBtn").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#strPostcode1").SendKeys("AL1 4RF");
            driver.FindElementByCssSelector("#intYearsAtAddress1").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#addressLookup1Select-button > span.ui-selectmenu-text").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#ui-id-2").Click();
            driver.FindElementByCssSelector("#intYearsAtAddress1").SendKeys("4");
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry1").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry1 > option:nth-child(233)").Click();
            Thread.Sleep(1000);
            //Previous Address
            driver.FindElementByCssSelector("#previousAddresses").Click();
            var PreviousPostCode = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            PreviousPostCode.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#prevAddressWrapper > div.previousaddress-form.previous-address2 > div:nth-child(2) > label")));
            driver.FindElementByCssSelector("#strPostcode2").SendKeys("SE5 0YA");
            driver.FindElementByCssSelector("#intYearsAtAddress2").Click();
            driver.FindElementByCssSelector("#addressLookup2Select-button > span.ui-selectmenu-text").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#ui-id-8").Click();
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#intYearsAtAddress2").SendKeys("4");
            Thread.Sleep(2000);
            driver.FindElementByCssSelector("#idCountry2").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#idCountry2 > option:nth-child(233)").Click();
            Thread.Sleep(3000);
            driver.FindElementByCssSelector("#sar_form_1 > div.d-flex.justify-content-between > button").Click();
            var ContactDetailsPage = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ContactDetailsPage.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#sar_form_2 > div.container > div:nth-child(4) > label")));
            driver.FindElementByCssSelector("#strHomeTel").SendKeys("0123456789");
            driver.FindElementByCssSelector("#strMobileTel").SendKeys("078543334231");
            driver.FindElementByCssSelector("#strEmailAddress").SendKeys("testautomation@selenium.com");
            driver.FindElementByCssSelector("#strEmailAddressRepeat").SendKeys("testautomation@selenium.com");
            driver.FindElementByCssSelector("#idBrought").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#sar_form_2 > div.d-flex.justify-content-between > button").Click();
            Thread.Sleep(1000);
            // Summary Page
            bool strFirstName = driver.FindElementByXPath("//*[@id='strFirstName']").Displayed;
            Assert.AreEqual(strFirstName, true);
            bool strPreviousFirstName = driver.FindElementByCssSelector("#strPrevFirstname4").Displayed;
            Assert.AreEqual(strPreviousFirstName, true);
            bool strPostCode = driver.FindElementByCssSelector("#strPostcode1").Displayed;
            Assert.AreEqual(strPostCode, true);
            bool strPreviousPostCode = driver.FindElementByCssSelector("#strPostcode1").Displayed;
            Assert.AreEqual(strPreviousPostCode, true);
            bool strHomeTelephone = driver.FindElementByCssSelector("#strHomeTel").Displayed;
            Assert.AreEqual(strHomeTelephone, true);
            // Consent
            driver.FindElementByCssSelector("#blnEquifaxOptOut").Click();
            Thread.Sleep(1000);
            driver.FindElementByCssSelector("#blnEquifaxOptOut > option:nth-child(2)").Click();
            Thread.Sleep(1000);
            // Submit
            driver.FindElementByCssSelector("#sar_form_3 > div.d-flex.justify-content-between > button").Click();
            Thread.Sleep(2000);
            // Confirm my identity
            var ConfirmMyIdentity = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ConfirmMyIdentity.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#hooyu > b")));
            //Reference Number
            bool isRefNo = driver.FindElementByXPath("//*[@id='page_full']/div/p[1]").Displayed;
            Assert.AreEqual(isRefNo, true);
            driver.FindElementByCssSelector("#cifas_upload").Click();
            Thread.Sleep(2000);
            driver.FindElementByXPath("//*[@id='identity_options']/a[1]/b").Click();
            Thread.Sleep(2000);
            //Upload Identification Documents
            driver.FindElementByXPath("//*[@id='list_1-button']/span[2]").Click();
            Thread.Sleep(1000);
            // UK driving licence
            driver.FindElementByXPath("//*[@id='ui-id-3']").Click();
            Thread.Sleep(2000);

            // upload front of card
            string FrontOfCard = AppDomain.CurrentDomain.BaseDirectory;
            string FrontOfCard_FullPath = FrontOfCard + "\\FrontOfCard.jpg";
            driver.FindElementByXPath("(//input[@type='file'])[1]").SendKeys(FrontOfCard_FullPath);

            // upload back of card
            string BackOfCard = AppDomain.CurrentDomain.BaseDirectory;
            string BackOfCard_FullPath = BackOfCard + "\\BackOfCard.jpg";
            driver.FindElementByXPath("(//input[@type='file'])[2]").SendKeys(BackOfCard_FullPath);

            //// front of card
            //driver.FindElementByXPath("//*[@id='identity_form']/fieldset[1]/div[2]/div[1]/label").Click();
            //Thread.Sleep(5000);
            //// click on ID card in file explorer
            //session.FindElementByXPath("//*[@Name='ID - front']").Click();
            //Thread.Sleep(2000);
            //InputSimulator sim = new InputSimulator();
            //sim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            //Thread.Sleep(2000);
            //// back of card
            //driver.FindElementByXPath("/html/body/div[8]/div[2]/div/form/fieldset[1]/div[2]/div[2]/label").Click();
            //Thread.Sleep(2000);
            //// click on ID card in file explorer - back of card
            //session.FindElementByXPath("//*[@Name='ID - back']").Click();
            //Thread.Sleep(2000);
            ////upload back card
            //sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            // Address drop down
            driver.FindElementByXPath("//*[@id='list_2-button']/span[2]").Click();
            Thread.Sleep(1000);
            // select bank statement
            driver.FindElementByXPath("//*[@id='ui-id-14']").Click();
            Thread.Sleep(1000);

            string Page1 = AppDomain.CurrentDomain.BaseDirectory;
            string Page1_FullPath = Page1 + "\\Page1.jpg";
            driver.FindElementByXPath("(//input[@type='file'])[3]").SendKeys(Page1_FullPath);

            // upload back of card
            string Page2 = AppDomain.CurrentDomain.BaseDirectory;
            string Page2_FullPath = Page2 + "\\Page2.jpg";
            driver.FindElementByXPath("(//input[@type='file'])[4]").SendKeys(Page2_FullPath);

            ////click to open Page 1 file explorer
            //driver.FindElementByXPath("//*[@id='identity_form']/fieldset[2]/div[2]/div[1]/label").Click();
            //Thread.Sleep(2000);
            //// click on Page 1 of bank statement
            //session.FindElementByXPath("//*[@Name='Bank_Statement_Page_1']").Click();
            //Thread.Sleep(2000);
            //sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            //// click on open page 2
            //driver.FindElementByXPath("//*[@id='identity_form']/fieldset[2]/div[2]/div[2]/label").Click();
            //Thread.Sleep(2000);
            ////click on page 2 of bank statement
            //session.FindElementByXPath("//*[@Name='Bank_Statement_Page_2']").Click();
            //Thread.Sleep(2000);
            //sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            driver.FindElementByXPath("//*[@id='identity_form']/p[1]/input").Click();
            Thread.Sleep(3000);
            var DSARStatus = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            DSARStatus.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='page']/h1")));
            driver.FindElementByXPath("//*[@id='page']/p[5]/a").Click();
            Thread.Sleep(2000);
            driver.FindElementByXPath("//*[@id='sar-status']/div[1]/div").Click();
            Thread.Sleep(1000);
            string strRequestSubmitted = driver.FindElementByXPath("/html/body/div[8]/div[2]/div/h2").Text.Trim();
            Assert.AreEqual(strRequestSubmitted, "Request submitted – Awaiting review");
            Thread.Sleep(1000);
            driver.FindElementByXPath("//*[@id='sar-status']/div[3]/div").Click();
            Thread.Sleep(1000);
            string strResponseIssued = driver.FindElementByXPath("/html/body/div[8]/div[2]/div/h2").Text.Trim();
            Assert.AreEqual(strResponseIssued, "Response Issued");


            driver.Quit();


            foreach (var process in Process.GetProcessesByName("chrome.exe"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("WinAppDriver.exe"))
            {
                process.Kill();
            }



        }


        [Test]
        public void DSARform_ManualUpload_DataDriven()
        {

            using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\DSARform_ManualUpload_DataDriven_DataSource.csv"))
            {
                int i = 0;
                List<string> listTitle = new List<string>();
                List<string> listFirstName = new List<string>();
                List<string> listMiddleName = new List<string>();
                List<string> listSurName = new List<string>();
                List<string> listBirthDay = new List<string>();
                List<string> listBirthMounth = new List<string>();
                List<string> listBirthYear = new List<string>();


                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listTitle.Add(values[0]);
                    listFirstName.Add(values[1]);
                    listMiddleName.Add(values[2]);
                    listSurName.Add(values[3]);
                    listBirthDay.Add(values[4]);
                    listBirthMounth.Add(values[5]);
                    listBirthYear.Add(values[6]);

                }

                foreach (var values in listTitle)
                {

                    i++;
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


                    driver.Navigate().GoToUrl("https://development.cifas.org.uk/");
                    //wait until DSAR Form ison is visible
                    Thread.Sleep(2000);
                    driver.FindElementByCssSelector("#saveCookieSetting").Click();
                    var ExplicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    ExplicitWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#shortcuts > div.social > div.sociallink.search > div.contracted")));
                    Thread.Sleep(3000);
                    //DSAR icon
                    driver.FindElementByCssSelector("#shortcuts > div.social > div.sociallink.identity_protection > div").Click();
                    Thread.Sleep(1000);
                    driver.FindElementByCssSelector("#shortcuts > div.social > div.sociallink.identity_protection.open > a").Click();
                    //Title
                    driver.FindElementByCssSelector("#strTitle").SendKeys(listTitle[i]);
                    driver.FindElementByCssSelector("#strFirstName").SendKeys(listFirstName[i]);
                    driver.FindElementByCssSelector("#strMiddleName").SendKeys(listMiddleName[i]);
                    driver.FindElementByCssSelector("#strSurname").SendKeys(listSurName[i]);
                    driver.FindElementByCssSelector("#blnSimilarName").Click();
                    driver.FindElementByCssSelector("#previousNames").Click();
                    var PreviousFirstName = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    PreviousFirstName.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#prevNameWrapper > div.previousname-form.previous-name1 > div:nth-child(1) > label")));
                    driver.FindElementByCssSelector("#strPrevFirstname1").SendKeys("PreviousFirstName");
                    driver.FindElementByCssSelector("#strPrevMiddle1").SendKeys("PreviousMiddleName");
                    driver.FindElementByCssSelector("#strPrevSurname1").SendKeys("PreviousSurName");
                    driver.FindElementByCssSelector("#dob_day").SendKeys(listBirthDay[i]);
                    driver.FindElementByCssSelector("#sar_form_0 > div.container > div:nth-child(7) > div > input[type=number]:nth-child(2)").SendKeys(listBirthMounth[i]);
                    driver.FindElementByCssSelector("#sar_form_0 > div.container > div:nth-child(7) > div > input[type=number]:nth-child(3)").SendKeys(listBirthYear[i]);
                    //Confirm
                    driver.FindElementByCssSelector("#submitBtn").Click();
                    Thread.Sleep(1000);
                    driver.FindElementByCssSelector("#strPostcode1").SendKeys("AL1 4RF");
                    driver.FindElementByCssSelector("#intYearsAtAddress1").Click();
                    Thread.Sleep(1000);
                    driver.FindElementByCssSelector("#addressLookup1Select-button > span.ui-selectmenu-text").Click();
                    Thread.Sleep(1000);
                    driver.FindElementByCssSelector("#ui-id-2").Click();
                    driver.FindElementByCssSelector("#intYearsAtAddress1").SendKeys("4");
                    Thread.Sleep(2000);
                    driver.FindElementByCssSelector("#idCountry1").Click();
                    Thread.Sleep(2000);
                    driver.FindElementByCssSelector("#idCountry1 > option:nth-child(233)").Click();
                    Thread.Sleep(1000);
                    //Previous Address
                    driver.FindElementByCssSelector("#previousAddresses").Click();
                    var PreviousPostCode = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    PreviousPostCode.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#prevAddressWrapper > div.previousaddress-form.previous-address2 > div:nth-child(2) > label")));
                    driver.FindElementByCssSelector("#strPostcode2").SendKeys("SE5 0YA");
                    driver.FindElementByCssSelector("#intYearsAtAddress2").Click();
                    driver.FindElementByCssSelector("#addressLookup2Select-button > span.ui-selectmenu-text").Click();
                    Thread.Sleep(2000);
                    driver.FindElementByCssSelector("#ui-id-8").Click();
                    Thread.Sleep(2000);
                    driver.FindElementByCssSelector("#intYearsAtAddress2").SendKeys("4");
                    Thread.Sleep(2000);
                    driver.FindElementByCssSelector("#idCountry2").Click();
                    Thread.Sleep(1000);
                    driver.FindElementByCssSelector("#idCountry2 > option:nth-child(233)").Click();
                    Thread.Sleep(3000);
                    driver.FindElementByCssSelector("#sar_form_1 > div.d-flex.justify-content-between > button").Click();
                    var ContactDetailsPage = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    ContactDetailsPage.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#sar_form_2 > div.container > div:nth-child(4) > label")));
                    driver.FindElementByCssSelector("#strHomeTel").SendKeys("0123456789");
                    driver.FindElementByCssSelector("#strMobileTel").SendKeys("078543334231");
                    driver.FindElementByCssSelector("#strEmailAddress").SendKeys("testautomation@selenium.com");
                    driver.FindElementByCssSelector("#strEmailAddressRepeat").SendKeys("testautomation@selenium.com");
                    driver.FindElementByCssSelector("#idBrought").Click();
                    Thread.Sleep(1000);
                    driver.FindElementByCssSelector("#sar_form_2 > div.d-flex.justify-content-between > button").Click();
                    Thread.Sleep(1000);
                    // Summary Page
                    bool strFirstName = driver.FindElementByXPath("//*[@id='strFirstName']").Displayed;
                    Assert.AreEqual(strFirstName, true);
                    bool strPreviousFirstName = driver.FindElementByCssSelector("#strPrevFirstname4").Displayed;
                    Assert.AreEqual(strPreviousFirstName, true);
                    bool strPostCode = driver.FindElementByCssSelector("#strPostcode1").Displayed;
                    Assert.AreEqual(strPostCode, true);
                    bool strPreviousPostCode = driver.FindElementByCssSelector("#strPostcode1").Displayed;
                    Assert.AreEqual(strPreviousPostCode, true);
                    bool strHomeTelephone = driver.FindElementByCssSelector("#strHomeTel").Displayed;
                    Assert.AreEqual(strHomeTelephone, true);
                    // Consent
                    driver.FindElementByCssSelector("#blnEquifaxOptOut").Click();
                    Thread.Sleep(1000);
                    driver.FindElementByCssSelector("#blnEquifaxOptOut > option:nth-child(2)").Click();
                    Thread.Sleep(1000);
                    // Submit
                    driver.FindElementByCssSelector("#sar_form_3 > div.d-flex.justify-content-between > button").Click();
                    Thread.Sleep(2000);
                    // Confirm my identity
                    var ConfirmMyIdentity = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    ConfirmMyIdentity.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#hooyu > b")));
                    //Reference Number
                    bool isRefNo = driver.FindElementByXPath("//*[@id='page_full']/div/p[1]").Displayed;
                    Assert.AreEqual(isRefNo, true);
                    driver.FindElementByCssSelector("#cifas_upload").Click();
                    Thread.Sleep(2000);
                    driver.FindElementByXPath("//*[@id='identity_options']/a[1]/b").Click();
                    Thread.Sleep(2000);
                    //Upload Identification Documents
                    driver.FindElementByXPath("//*[@id='list_1-button']/span[2]").Click();
                    Thread.Sleep(1000);
                    // UK driving licence
                    driver.FindElementByXPath("//*[@id='ui-id-3']").Click();
                    Thread.Sleep(2000);

                    // upload front of card
                    string FrontOfCard = AppDomain.CurrentDomain.BaseDirectory;
                    string FrontOfCard_FullPath = FrontOfCard + "\\FrontOfCard.jpg";
                    driver.FindElementByXPath("(//input[@type='file'])[1]").SendKeys(FrontOfCard_FullPath);

                    // upload back of card
                    string BackOfCard = AppDomain.CurrentDomain.BaseDirectory;
                    string BackOfCard_FullPath = BackOfCard + "\\BackOfCard.jpg";
                    driver.FindElementByXPath("(//input[@type='file'])[2]").SendKeys(BackOfCard_FullPath);


                    Thread.Sleep(2000);
                    // Address drop down
                    driver.FindElementByXPath("//*[@id='list_2-button']/span[2]").Click();
                    Thread.Sleep(1000);
                    // select bank statement
                    driver.FindElementByXPath("//*[@id='ui-id-14']").Click();
                    Thread.Sleep(1000);

                    string Page1 = AppDomain.CurrentDomain.BaseDirectory;
                    string Page1_FullPath = Page1 + "\\Page1.jpg";
                    driver.FindElementByXPath("(//input[@type='file'])[3]").SendKeys(Page1_FullPath);

                    // upload back of card
                    string Page2 = AppDomain.CurrentDomain.BaseDirectory;
                    string Page2_FullPath = Page2 + "\\Page2.jpg";
                    driver.FindElementByXPath("(//input[@type='file'])[4]").SendKeys(Page2_FullPath);


                    Thread.Sleep(2000);
                    driver.FindElementByXPath("//*[@id='identity_form']/p[1]/input").Click();
                    Thread.Sleep(3000);
                    var DSARStatus = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    DSARStatus.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='page']/h1")));
                    driver.FindElementByXPath("//*[@id='page']/p[5]/a").Click();
                    Thread.Sleep(2000);
                    driver.FindElementByXPath("//*[@id='sar-status']/div[1]/div").Click();
                    Thread.Sleep(1000);
                    string strRequestSubmitted = driver.FindElementByXPath("/html/body/div[8]/div[2]/div/h2").Text.Trim();
                    Assert.AreEqual(strRequestSubmitted, "Request submitted – Awaiting review");
                    Thread.Sleep(1000);
                    driver.FindElementByXPath("//*[@id='sar-status']/div[3]/div").Click();
                    Thread.Sleep(1000);
                    string strResponseIssued = driver.FindElementByXPath("/html/body/div[8]/div[2]/div/h2").Text.Trim();
                    Assert.AreEqual(strResponseIssued, "Response Issued");






                    driver.Quit();

                }

                foreach (var process in Process.GetProcessesByName("chrome.exe"))
                {
                    process.Kill();
                }
                foreach (var process in Process.GetProcessesByName("WinAppDriver.exe"))
                {
                    process.Kill();
                }


            }



        }


        [Test]
        public void PracticeForm_DataDriven()
        {

            using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\PracticeForm_DataDriven_DataSource.csv"))
            {
                int i = 0;
                List<string> Fname = new List<string>();
                List<string> Sname = new List<string>();
                List<string> DOB = new List<string>();



                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Fname.Add(values[0]);
                    Sname.Add(values[1]);
                    DOB.Add(values[2]);


                }

                foreach (var values in Fname)
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
                    driver = new ChromeDriver(options);

                    driver.Manage().Window.Maximize();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    driver.Navigate().GoToUrl("https://www.techlistic.com/p/selenium-practice-form.html");
                    driver.FindElementByXPath("//*[@id=\"post-body-3077692503353518311\"]/div[1]/div/div/h2[2]/div[1]/div/div[2]/input").SendKeys(Fname[i]);
                    driver.FindElementByXPath("//*[@id=\"post-body-3077692503353518311\"]/div[1]/div/div/h2[2]/div[1]/div/div[5]/input").SendKeys(Sname[i]);
                    driver.FindElementByXPath("//*[@id=\"datepicker\"]").SendKeys(DOB[i]);

                    bool displayed = driver.FindElementByXPath("//*[@id=\"post-body-3077692503353518311\"]/div[1]/div/div/h2[2]/div[1]/div/div[28]/div[2]/span").Displayed;
                    Assert.True(displayed);

                    driver.Quit();

                    //problems:
                    // index out of the range
                    // only last test case recorded because [Test] is not triggered each time.


                }


            }
        }



        [Test, TestCaseSource("GetTestData")]    // , TestCase(TestName = "Tname")
        public void PracticeForm_IndividualRecord(string Fname, string Sname, string DOB)
        {
            

            driver.Navigate().GoToUrl("https://www.techlistic.com/p/selenium-practice-form.html");
            driver.FindElementByXPath("//*[@id=\"post-body-3077692503353518311\"]/div[1]/div/div/h2[2]/div[1]/div/div[2]/input").SendKeys(Fname);
            driver.FindElementByXPath("//*[@id=\"post-body-3077692503353518311\"]/div[1]/div/div/h2[2]/div[1]/div/div[5]/input").SendKeys(Sname);
            driver.FindElementByXPath("//*[@id=\"datepicker\"]").SendKeys(DOB);

            bool displayed = driver.FindElementByXPath("//*[@id=\"post-body-3077692503353518311\"]/div[1]/div/div/h2[2]/div[1]/div/div[28]/div[2]/span").Displayed;
            Assert.True(displayed);

            Console.WriteLine("Test Case 01 out of 5," + Fname + ", executed successfully!");

            driver.Quit();


        }
        public static IEnumerable<string[]> GetTestData()
        {
            using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\PracticeForm_DataDriven_DataSource.csv"))
            {
                //int i = 0;
                //List<string> Fname = new List<string>();
                //List<string> Sname = new List<string>();
                //List<string> DOB = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    string Fname = values[0];
                    string Sname = values[1];
                    string DOB = values[2];
                    //string Tname = values[3];

                    yield return new[] { Fname, Sname, DOB };
                }
                
            }
        }








    }
}