using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.LibertyJobSearch_Pages
{
    public class PersonalInfoMap
    {
        public IWebDriver driver;

        public PersonalInfoMap(IWebDriver driver)
        {
            this.driver = driver;
        }


        public IWebElement FirstNameBox => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[1]/input"));

        public IWebElement LastNameBox => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[2]/input"));

        public IWebElement PhoneNumberBox => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[3]/input"));

        public IWebElement EmailBox => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[4]/input"));

        public IWebElement CountryDropDown => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[5]/select"));

        public IWebElement IndustryDropDown => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[6]/select"));

        public IWebElement CVchoosefile => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[7]/div"));

        public IWebElement TermsConditionsBox => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[9]/fieldset/div/input[1]"));

        public IWebElement NextBttn => driver.FindElement(By.XPath("/html/body/div/main/div/div/section/div/article/div/form/div/fieldset/div[11]/fieldset/div/button[2]"));

    }
}
