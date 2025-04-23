using SeleniumTestSuite.Pages;
using SeleniumTestSuite.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTestSuite.Tests
{
    [TestFixture]
    public class HomePageTests : TestBase
    {

        [Test]
        [Description("Verify home page title and elements are visible")]
        public void VerifyPageTitleAndElementsVisible()
        {
            var homePage = new HomePage(driver);

            Assert.That(driver.Title, Is.EqualTo("STORE"), "Page title should be 'STORE'.");
            homePage.VerifyPageElements();
        }

        [Test]
        [Description("Verify about us modal appears")]
        public void VerifyAboutUsModal()
        {
            var homePage = new HomePage(driver);
            homePage.OpenAboutUsModal();

            var aboutModal = driver.FindElement(By.Id("videoModal"));
            Assert.That(aboutModal.Displayed, Is.True, "About us modal should be visible.");

            var closeButton = driver.FindElement(By.CssSelector("#videoModal > div > div > div.modal-footer > button"));
            closeButton.Click();

            GetWait().Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("videoModal")));
        }

        [Test]
        [Description("Verify contact modal appears and can send message")]
        public void VerifyContactModalAndSendMessage()
        {
            var homePage = new HomePage(driver);
            homePage.OpenContactModal();

            var contactModal = driver.FindElement(By.Id("exampleModal"));
            Assert.That(contactModal.Displayed, Is.True, "Contact modal should be visible.");

            driver.FindElement(By.Id("recipient-email")).SendKeys("test@example.com");
            driver.FindElement(By.Id("recipient-name")).SendKeys("Test User");
            driver.FindElement(By.Id("message-text")).SendKeys("This is a test message from automated test.");

            driver.FindElement(By.CssSelector("#exampleModal > div > div > div.modal-footer > button.btn.btn-primary")).Click();

            GetWait().Until(ExpectedConditions.AlertIsPresent());
            var alert = driver.SwitchTo().Alert();
            string? alertText = alert?.Text;
            alert?.Accept();

            Assert.That(alertText, Does.Contain("Thanks").Or.Contain("successful").Or.Contain("Message"),
                "Expected confirmation alert after sending message.");
        }
    }
}