using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.LibertyJobSearch_Pages
{
    public class WorkingForUsMap
    {
        public IWebDriver driver;

        public WorkingForUsMap(IWebDriver driver)
        {
            this.driver = driver;
        }

        
        public IWebElement WorkingForUsOverview => driver.FindElement(By.XPath("//*[@id=\"header-menu-expanded-nav\"]/div/div[1]/div[2]/div[1]/div[2]/div/div[1]/p/a"));

        public IWebElement LifeAtLibertySpecialtyMarkets => driver.FindElement(By.XPath("//*[@id=\"header-menu-expanded-nav\"]/div/div[1]/div[2]/div[1]/div[2]/div/div[2]/div[1]/a/span"));

        public IWebElement LearningAndDevelopment => driver.FindElement(By.XPath("//*[@id=\"header-menu-expanded-nav\"]/div/div[1]/div[2]/div[1]/div[2]/div/div[2]/div[2]/a/span"));

        public IWebElement Benefits => driver.FindElement(By.XPath("//*[@id=\"header-menu-expanded-nav\"]/div/div[1]/div[2]/div[1]/div[2]/div/div[2]/div[3]/a/span"));



    }
}
