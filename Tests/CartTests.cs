using SeleniumTestSuite.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumTestSuite.Utils;

namespace SeleniumTestSuite.Tests
{
    [TestFixture]
    public class CartTests : TestBase
    {

        [Test]
        [Description("Verify complete cart flow with empty cart and multiple items")]
        public void VerifyFullCartFlowWithEmptyAndMultipleItems()
        {
            var homePage = new HomePage(driver);

            // Step 1: Verify empty cart
            driver.FindElement(By.Id("cartur")).Click();
            GetWait().Until(d => d.Url.Contains("cart.html"));
            var cartTableRows = driver.FindElements(By.CssSelector("#tbodyid > tr"));
            Assert.That(cartTableRows.Count, Is.EqualTo(0), "Cart should be initially empty.");

            // Try to place order with empty cart
            var placeOrderButton = driver.FindElement(By.CssSelector("#page-wrapper > div > div.col-lg-1 > button"));
            placeOrderButton.Click();

            GetWait().Until(ExpectedConditions.ElementIsVisible(By.Id("orderModal")));
            var orderModal = driver.FindElement(By.Id("orderModal"));
            Assert.That(orderModal.Displayed, Is.True, "Order modal should be visible even with empty cart.");
            driver.FindElement(By.CssSelector("#orderModal .btn.btn-secondary")).Click();

            // Step 2: Add two products to cart
            driver.Navigate().GoToUrl(BaseUrl);
            var selectedProducts = new List<(string name, int price)>();

            for (int i = 0; i < 2; i++)
            {
                homePage.SelectProduct(i);

                var productPage = new ProductPage(driver);
                var productInfo = productPage.GetProductInfo();
                selectedProducts.Add((productInfo.name, productInfo.price));

                productPage.AddToCart();
                driver.Navigate().GoToUrl(BaseUrl);
            }

            // Step 3: Verify cart has 2 items
            driver.Navigate().GoToUrl($"{BaseUrl}/cart.html");

            GetWait().Until(driver =>
            {
                var rows = driver.FindElements(By.CssSelector("#tbodyid > tr"));
                return rows.Count == 2;
            });

            var cartRows = driver.FindElements(By.CssSelector("#tbodyid > tr"));
            Assert.That(cartRows.Count, Is.EqualTo(2), $"Expected 2 items in the cart, but found {cartRows.Count}");

            // Verify cart contents
            var cartPage = new CartPage(driver);
            cartPage.VerifyCartContents(selectedProducts);

            // Step 4: Place order
            cartPage.PlaceOrder("Test User", "Testland", "Testopolis", "1234567890123456", "12", "2025");
            cartPage.VerifyOrderConfirmation();
        }
    }
}