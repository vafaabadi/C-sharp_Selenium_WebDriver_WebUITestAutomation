using OpenQA.Selenium;

namespace UnitTestProject1.LogIn_Pages
{
    public class LogInPage
    {
        public IWebDriver driver;

        public LogInPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        public IWebElement Username => driver.FindElement(By.XPath("//input[@name=\"username\"]"));

        public IWebElement Password => driver.FindElement(By.XPath("//input[@name=\"password\"]"));

        public IWebElement Submit => driver.FindElement(By.XPath("//input[@type='submit']"));

    }
}
