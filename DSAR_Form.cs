using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using WebUITestAutomation.Pages;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Windows;
using WebDriverManager.Helpers;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using WindowsInput;
using WindowsInput.Native;

namespace WebUITestAutomation
{
    [TestClass]
    public class DSAR_Form
    {


        String WinDriver = "C:\\Program Files\\Windows Application Driver\\WinAppDriver.exe";




        [TestMethod]
        public void DSARform_UpToHooyu()
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
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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



        }


        [TestMethod]
        public void DSARform_UploadLater()
        {
            //to run WinAppDriver
            Process.Start(@WinDriver);
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");   // Added Path to environment variables???????????
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);


            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");


            //starting browser
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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



        }



        [TestMethod]
        public void DSARform_ManualUpload()
        {
            //to run WinAppDriver
            Process.Start(@WinDriver);
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");   // Added Path to environment variables???????????
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);


            //to kick start Selenium ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");


            //starting browser
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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
            // front of card
            driver.FindElementByXPath("//*[@id='identity_form']/fieldset[1]/div[2]/div[1]/label").Click();
            Thread.Sleep(5000);
            // click on ID card in file explorer
            session.FindElementByXPath("//*[@Name='ID - front']").Click();
            Thread.Sleep(2000);
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            // back of card
            driver.FindElementByXPath("/html/body/div[8]/div[2]/div/form/fieldset[1]/div[2]/div[2]/label").Click();
            Thread.Sleep(2000);
            // click on ID card in file explorer - back of card
            session.FindElementByXPath("//*[@Name='ID - back']").Click();
            Thread.Sleep(2000);
            //upload back card
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            // Address drop down
            driver.FindElementByXPath("//*[@id='list_2-button']/span[2]").Click();
            Thread.Sleep(1000);
            // select bank statement
            driver.FindElementByXPath("//*[@id='ui-id-14']").Click();
            Thread.Sleep(1000);
            //click to open Page 1 file explorer
            driver.FindElementByXPath("//*[@id='identity_form']/fieldset[2]/div[2]/div[1]/label").Click();
            Thread.Sleep(2000);
            // click on Page 1 of bank statement
            session.FindElementByXPath("//*[@Name='Bank_Statement_Page_1']").Click();
            Thread.Sleep(2000);
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            // click on open page 2
            driver.FindElementByXPath("//*[@id='identity_form']/fieldset[2]/div[2]/div[2]/label").Click();
            Thread.Sleep(2000);
            //click on page 2 of bank statement
            session.FindElementByXPath("//*[@Name='Bank_Statement_Page_2']").Click();
            Thread.Sleep(2000);
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
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










    }
}