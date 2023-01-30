using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.LibertyJobSearch_Pages
{
    public class SearchMap
    {
        public IWebDriver driver;

        public SearchMap(IWebDriver driver)
        {
            this.driver = driver;
        }


        public IWebElement KeywordsBox => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div[2]/article/div/form/div/fieldset/div[1]/input"));

        public IWebElement LocationDropDown => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div[2]/article/div/form/div/fieldset/div[2]/select"));

        public IWebElement DepartmentDropBox => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div[2]/article/div/form/div/fieldset/div[3]/select"));

        public IWebElement SearchBttn => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div[2]/article/div/form/div/fieldset/div[4]/fieldset/div/button"));



    }
}
