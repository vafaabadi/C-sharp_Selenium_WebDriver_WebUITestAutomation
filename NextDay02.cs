using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Drawing;
using System.Threading;
using UnitTestProject1.LibertyJobSearch_Pages;
using UnitTestProject1.LogIn_Pages;
using ImageMagick;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace WebUITestAutomation
{


    [TestFixture]
    [Category("NextDay02")]
    public class NextDay02 : Base
    {



        public static string DecodePassword(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public static string ConfigManager(string key)
        {

            string typeEnv = Environment.GetEnvironmentVariable(key);
            if (Environment.GetEnvironmentVariable(key) == null)
            {
                return ConfigurationManager.AppSettings[key];
            }
            else
            {
                return Environment.GetEnvironmentVariable(key);
            }

        }



        [Test]
        public void LogIn_Chrome()
        {
            /*
            Go to www.base64encode.org. 
            Paste your username in the top box and then click on 'encode it' to base64 format. 
            Copy the encoded string and paste it for the value of key="ThisIsEncodedPassWord" under AppSettings in app.config file.
            Method DecodePassword(string encodedData) decodes the encryoted data
            Method ConfigManager(string key) reads the keys locally or from pipeline
             */

            driver.Navigate().GoToUrl("https://testpages.herokuapp.com/basic_html_form.html");
            driver.FindElementByXPath("//input[@name=\"username\"]").Click();
            driver.FindElementByXPath("//input[@name=\"username\"]").SendKeys(DecodePassword(ConfigManager("ThisIsEncodedUserName")));
            driver.FindElementByXPath("//input[@name=\"password\"]").Click();
            driver.FindElementByXPath("//input[@name=\"password\"]").SendKeys(DecodePassword(ConfigManager("ThisIsEncodedPassWord")));
            driver.FindElementByXPath("//input[@type='submit']").Click();
            Thread.Sleep(2000);

            driver.Quit();

        }



        [Test]
        public void JustWindowsDriver()
        {
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);

            session.FindElementByXPath("//*[@Name='Reload']").Click();
        }










    }
}
