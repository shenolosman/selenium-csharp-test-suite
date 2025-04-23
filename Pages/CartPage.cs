using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTestSuite.Pages
{
    public class CartPage
    {
        private readonly IWebDriver driver;
        private WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        private By CartItems => By.CssSelector("#tbodyid > tr");
        private By TotalPrice => By.Id("totalp");
        private By PlaceOrderButton => By.CssSelector("#page-wrapper > div > div.col-lg-1 > button");
        private By OrderModal => By.Id("orderModal");
        private By NameField => By.Id("name");
        private By CountryField => By.Id("country");
        private By CityField => By.Id("city");
        private By CardField => By.Id("card");
        private By MonthField => By.Id("month");
        private By YearField => By.Id("year");
        private By PurchaseButton => By.CssSelector("#orderModal .btn.btn-primary");
        private By ConfirmationModal => By.ClassName("sweet-alert");

        public void VerifyCartContents(List<(string name, int price)> expectedProducts)
        {
            var cartRows = driver.FindElements(CartItems).ToList();
            int cartTotal = 0;

            foreach (var product in expectedProducts)
            {
                var row = cartRows.FirstOrDefault(r => r.Text.Contains(product.name));
                Assert.IsNotNull(row, $"Product {product.name} not found in cart.");

                int price = int.Parse(row.FindElements(By.TagName("td"))[2].Text);
                Assert.That(price, Is.EqualTo(product.price), $"Price for {product.name} does not match.");
                cartTotal += price;
            }

            string totalPriceText = driver.FindElement(TotalPrice).Text;
            Assert.That(int.TryParse(totalPriceText, out var total), Is.True, "Total price is not a valid number.");
            Assert.That(total, Is.EqualTo(cartTotal), "Total price in cart does not match sum of item prices.");
        }

        public void PlaceOrder(string name, string country, string city, string card, string month, string year)
        {
            driver.FindElement(PlaceOrderButton).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(OrderModal));

            driver.FindElement(NameField).SendKeys(name);
            driver.FindElement(CountryField).SendKeys(country);
            driver.FindElement(CityField).SendKeys(city);
            driver.FindElement(CardField).SendKeys(card);
            driver.FindElement(MonthField).SendKeys(month);
            driver.FindElement(YearField).SendKeys(year);

            driver.FindElement(PurchaseButton).Click();
        }

        public void VerifyOrderConfirmation()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(ConfirmationModal));
            var confirmation = driver.FindElement(ConfirmationModal).Text;
            Assert.That(confirmation, Does.Contain("Thank you"), "Confirmation dialog not shown after placing order.");
        }
    }
}