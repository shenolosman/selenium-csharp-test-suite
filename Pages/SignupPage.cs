using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTestSuite.Pages
{
    public class SignupPage
    {
        private readonly IWebDriver driver;
        private WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        public SignupPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Locators
        private By UsernameField => By.Id("sign-username");
        private By PasswordField => By.Id("sign-password");
        private By SignupButton => By.CssSelector("#signInModal button.btn.btn-primary");

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

        public void ClickSignup()
        {
            driver.FindElement(SignupButton).Click();
        }

        public void CompleteSignup(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickSignup();
        }
    }
}