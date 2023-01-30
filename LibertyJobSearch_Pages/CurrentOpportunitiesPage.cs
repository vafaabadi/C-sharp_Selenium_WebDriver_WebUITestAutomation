using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.LibertyJobSearch_Pages
{
    public class CurrentOpportunitiesMap
    {
        public IWebDriver driver;

        public CurrentOpportunitiesMap(IWebDriver driver)
        {
            this.driver = driver;
        }


        public IWebElement HowToApply => driver.FindElement(By.XPath("//*[@id=\"header-menu-expanded-nav\"]/div/div[1]/div[2]/div[1]/div[2]/div[1]/div/a/span"));

        public IWebElement OpenPositions => driver.FindElement(By.XPath("//*[@id=\"header-menu-expanded-nav\"]/div/div[1]/div[2]/div[1]/div[2]/div[2]/div/a/span"));

        //public IWebElement LearningAndDevelopment => driver.FindElement(By.XPath("//*[@id=\"header-menu-expanded-nav\"]/div/div[1]/div[2]/div[1]/div[2]/div/div[2]/div[2]/a/span"));

        //public IWebElement Benefits => driver.FindElement(By.XPath("//*[@id=\"header-menu-expanded-nav\"]/div/div[1]/div[2]/div[1]/div[2]/div/div[2]/div[3]/a/span"));



    }
}
