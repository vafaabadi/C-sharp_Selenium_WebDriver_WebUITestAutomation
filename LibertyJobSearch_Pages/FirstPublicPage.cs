using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace UnitTestProject1.LibertyJobSearch_Pages
{
    public class FirstPublicMap
    {
        public IWebDriver driver;

        public FirstPublicMap(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement AboutUs => driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[1]/header/nav/div[4]/div/nav/ol/li[1]/div/a/span"));

        public IWebElement Products => driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[1]/header/nav/div[4]/div/nav/ol/li[2]/div/a/span"));

        public IWebElement Careers => driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[1]/header/nav/div[4]/div/nav/ol/li[5]/div/a/span"));


    }
}
