using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTestSuite.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        // Locators
        private By Navbar => By.Id("narvbarx");
        private By Categories => By.Id("cat");
        private By ProductGrid => By.Id("tbodyid");
        private By SignUpLink => By.LinkText("Sign up");
        private By LoginLink => By.LinkText("Log in");
        private By LogoutLink => By.Id("logout2");
        private By ProductLinks => By.CssSelector(".card-title a");
        private By Products => By.CssSelector(".card-title");
        private By CategoryLinks => By.Id("itemc");
        private By AboutUsLink => By.LinkText("About us");
        private By ContactLink => By.LinkText("Contact");
        private By NextPageButton => By.Id("next2");
        private By PrevPageButton => By.Id("prev2");


        // Methods
        public void VerifyPageElements()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState")?.ToString() == "complete");

            Assert.Multiple(() =>
            {
                Assert.That(driver.FindElement(Navbar).Displayed, Is.True, "Navbar should be visible.");
                Assert.That(driver.FindElement(Categories).Displayed, Is.True, "Categories section should be visible.");
                Assert.That(driver.FindElement(ProductGrid).Displayed, Is.True, "Product grid should be visible.");
            });
        }

        public void ClickSignUp()
        {
            driver.FindElement(SignUpLink).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("signInModal")));
        }

        public void ClickLogin()
        {
            driver.FindElement(LoginLink).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("logInModal")));
        }

        public void VerifyLoggedIn()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(LogoutLink));
        }

        public void VerifyLoggedOut()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(LoginLink));
        }

        public void SelectProduct(int index)
        {
            wait.Until(d => d.FindElements(ProductLinks).Count > index);
            driver.FindElements(ProductLinks)[index].Click();
        }

        public void SelectCategory(string categoryName)
        {
            var categories = driver.FindElements(CategoryLinks);
            var category = categories.First(c => c.Text.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            category.Click();
            wait.Until(d => d.FindElements(ProductLinks).Count >= 0);
        }
        public List<string> GetCategoryNames()
        {
            return driver.FindElements(CategoryLinks)
                        .Select(e => e.Text)
                        .ToList();
        }

        public List<string> GetProductTitles()
        {
            return wait.Until(d => d.FindElements(Products)
                                    .Select(p => p.Text.Trim())
                                    .ToList());
        }

        public void NavigateToNextPage()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(NextPageButton)).Click();
        }

        public void NavigateToPrevPage()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(PrevPageButton)).Click();
        }

        public void OpenAboutUsModal()
        {
            driver.FindElement(AboutUsLink).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("videoModal")));
        }

        public void OpenContactModal()
        {
            driver.FindElement(ContactLink).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("exampleModal")));
        }
    }
}