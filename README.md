
# SeleniumTestSuite (C# with Selenium WebDriver)

## 📄 Test Automation Approach for Demoblaze

The purpose of this automation suite is to test key functionalities of the [Demoblaze demo web shop](https://www.demoblaze.com/) including login, signup, product navigation, cart functionality and contact form submission for assignment 3 (Testing business-facing qualities).

## ✅ Requirements

- .NET SDK (6.0+)
- Chrome browser
- ChromeDriver (should match your Chrome version)
- NUnit Framework

## 🔧 Tools Used

- Selenium WebDriver: For browser interaction
- NUnit: For test framework
- ChromeDriver: To run tests in Chrome
- C#: As the test scripting language
- VS Code / Visual Studio: (for vs code Install C# extension)

## 📦 How to Run

1. Open project folder in Visual Studio or VS Code
2. Run the following in terminal to restore packages:

   ```bash
   dotnet restore
   ```

3. Execute tests:

   ```bash
   dotnet test
   ```

   or Run tests from Test Explorer or via terminal

## 💡 What is Tested

This suite includes 10 tests targeting https://demoblaze.com, an e-commerce demo site. Tests cover login, sign up, cart operations, product browsing, purchase products, page navigation,category list, about us modal  and contact modal using:

- Page Object Model for maintainability and modularity
- Explicit waits for synchronization
- Clean, readable naming and structure

## 📁 Folder Structure

```bash
SeleniumTestSuite/
│
├── Pages/
│   ├── CartPage.cs
│   ├── LoginPage.cs
│   ├── HomePage.cs
│   ├── ProductPage.cs
│   └── SignupPage.cs
│
├── Tests/
│   ├── HomePageTests.cs
│   ├── LoginTests.cs
│   ├── PurchaseTests.cs
│   ├── CartTests.cs
│   └── SignupTests.cs
│
├── Utils/
│   └── TestBase.cs
│ 
├── SeleniumTestSuite.csproj
└── README.md
```
