using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;


namespace SeleniumTestSuite.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver driver;
        private WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        private By ProductName => By.ClassName("name");
        private By ProductPrice => By.CssSelector(".price-container");
        private By AddToCartButton => By.CssSelector("a.btn.btn-success.btn-lg");

        public string GetProductName()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(ProductName));
            return driver.FindElement(ProductName).Text;
        }

        public (string name, int price) GetProductInfo()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(ProductName));
            var name = driver.FindElement(ProductName).Text;
            var priceText = driver.FindElement(ProductPrice).Text.Split(' ')[0].Replace("$", "");
            var price = int.Parse(priceText);
            return (name, price);
        }

        public void AddToCart()
        {
            driver.FindElement(AddToCartButton).Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
        }
    }
}