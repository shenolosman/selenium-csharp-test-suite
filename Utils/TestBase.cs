using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTestSuite.Utils
{
    public class TestBase
    {
        protected IWebDriver driver;
        protected const string BaseUrl = "https://www.demoblaze.com/";


        [SetUp]
        public void SetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            // driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }

        protected WebDriverWait GetWait()
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
    }
}