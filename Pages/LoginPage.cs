using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTestSuite.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        // Locators
        private By UsernameField => By.Id("loginusername");
        private By PasswordField => By.Id("loginpassword");
        private By LoginButton => By.CssSelector("#logInModal button.btn.btn-primary");

        // Methods
        public void EnterUsername(string username)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(UsernameField));
            driver.FindElement(UsernameField).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(PasswordField).SendKeys(password);
        }

        public void ClickLogin()
        {
            driver.FindElement(LoginButton).Click();
        }

        public void CompleteLogin(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }
    }
}