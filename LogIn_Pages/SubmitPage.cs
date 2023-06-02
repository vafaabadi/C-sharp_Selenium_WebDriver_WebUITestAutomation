using OpenQA.Selenium;

namespace UnitTestProject1.LogIn_Pages
{
    public class SubmitPage
    {
        public IWebDriver driver;

        public SubmitPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        public IWebElement ValueUsername => driver.FindElement(By.XPath("//*[@id=\"_valueusername\"]"));

        public IWebElement ValuePassword => driver.FindElement(By.XPath("//*[@id=\"_valuepassword\"]"));


    }
}
