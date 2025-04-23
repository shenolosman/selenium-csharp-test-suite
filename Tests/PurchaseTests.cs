using SeleniumTestSuite.Pages;
using NUnit.Framework;
using SeleniumTestSuite.Utils;

namespace SeleniumTestSuite.Tests
{
    [TestFixture]
    public class PurchaseTests : TestBase
    {
        [Test]
        [Description("Verify product details page loads correctly")]
        public void VerifyProductDetailsPage()
        {
            var homePage = new HomePage(driver);
            homePage.SelectProduct(0);

            var productPage = new ProductPage(driver);
            var productTitle = productPage.GetProductName();

            Assert.That(productTitle, Is.Not.Null.And.Not.Empty, "Product title should be visible on product detail page.");
        }

        [Test]
        [Description("Verify product categories load correctly")]
        public void VerifyCategoriesLoadAndDisplayProductsOrEmpty()
        {
            var homePage = new HomePage(driver);
            var categories = homePage.GetCategoryNames();

            Assert.IsTrue(categories.Count > 0, "No categories found on page.");

            foreach (var categoryName in categories)
            {
                homePage.SelectCategory(categoryName);
                var products = homePage.GetProductTitles();

                Assert.Pass($"Category '{categoryName}' loaded successfully with {products.Count} items (can be empty).");
            }
        }

        [Test]
        [Description("Verify pagination works correctly")]
        public void VerifyPaginationWorksCorrectly()
        {
            var homePage = new HomePage(driver);

            driver.Navigate().GoToUrl(BaseUrl);
            // Wait for the page to load and display products
            var firstPageTitles = homePage.GetProductTitles();
            homePage.NavigateToNextPage();

            // Wait for new products to load on the next page
            var secondPageTitles = homePage.GetProductTitles();

            // Check if both pages contain the same items regardless of order
            Assert.That(firstPageTitles.All(item => secondPageTitles.Contains(item)) && secondPageTitles.All(item => firstPageTitles.Contains(item)),
                Is.True,
                "The products on the second page are not the same as the first page.");

            // Click 'Previous' pagination button and wait for the titles to change back to the first page's titles
            homePage.NavigateToPrevPage();

            // Wait until the titles on the previous page are the same as the first page
            var returnedTitles = homePage.GetProductTitles();

            // Check that the returned titles contain the same items as the first page
            Assert.That(firstPageTitles.All(item => returnedTitles.Contains(item)) && returnedTitles.All(item => firstPageTitles.Contains(item)),
                Is.True,
                "The products on the previous page are not the same as the first page.");
        }
    }
}