using SeleniumTestSuite.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumTestSuite.Utils;

namespace SeleniumTestSuite.Tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        [Description("Verify login modal appears when clicking login link")]
        public void VerifyLoginModalNavigation()
        {
            var homePage = new HomePage(driver);
            homePage.ClickLogin();

            var loginModal = driver.FindElement(By.Id("logInModal"));
            Assert.That(loginModal.Displayed, Is.True, "Login modal should be visible.");

            var loginConfirmButton = driver.FindElement(By.CssSelector("#logInModal .btn.btn-primary"));
            var closeButton = driver.FindElement(By.CssSelector("#logInModal .btn.btn-secondary"));

            Assert.Multiple(() =>
            {
                Assert.That(loginConfirmButton.Displayed, Is.True, "Login button should be visible.");
                Assert.That(closeButton.Displayed, Is.True, "Close button should be visible.");
            });
        }

        [Test]
        [Description("Verify login fails with invalid credentials")]
        public void VerifyLoginWithInvalidCredentials()
        {
            var homePage = new HomePage(driver);
            var loginPage = new LoginPage(driver);
            homePage.ClickLogin();

            string invalidUsername = string.Concat("nonexistent_user_", Guid.NewGuid().ToString("N").AsSpan(0, 6));
            string invalidPassword = "WrongPassword123";

            loginPage.CompleteLogin(invalidUsername, invalidPassword);

            GetWait().Until(ExpectedConditions.AlertIsPresent());
            var alert = driver.SwitchTo().Alert();
            string? alertText = alert?.Text;
            alert?.Accept();

            Assert.That(alertText, Does.Contain("User does not exist")
                                    .Or.Contain("Wrong password")
                                    .Or.Contain("error"),
                                    "Expected an error message for invalid login.");
        }

    }
}