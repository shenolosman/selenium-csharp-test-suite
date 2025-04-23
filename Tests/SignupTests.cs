using SeleniumTestSuite.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using SeleniumTestSuite.Utils;

namespace SeleniumTestSuite.Tests
{
    [TestFixture]
    public class SignupTests : TestBase
    {
        [Test]
        [Description("Verify complete signup, login and logout flow")]
        public void VerifySignupLoginAndLogoutFlow()
        {
            var homePage = new HomePage(driver);
            var signupPage = new SignupPage(driver);
            var loginPage = new LoginPage(driver);

            // Generate test credentials
            string generatedUsername = string.Concat("user_", Guid.NewGuid().ToString("N").AsSpan(0, 8));
            const string password = "Test1234!";

            // === SIGN UP ===
            homePage.ClickSignUp();
            signupPage.CompleteSignup(generatedUsername, password);
            HandleAlert();

            // === LOGIN ===
            homePage.ClickLogin();
            loginPage.CompleteLogin(generatedUsername, password);
            homePage.VerifyLoggedIn();

            // === LOGOUT ===
            driver.FindElement(By.Id("logout2")).Click();
            homePage.VerifyLoggedOut();
        }

        private void HandleAlert()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
        }
    }
}