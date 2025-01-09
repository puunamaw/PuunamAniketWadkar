# Automated UI Test Suite for Login Functionality

## Project Description  
This project contains automated UI tests for Login Functionality. 
The tests validate core Login Scenarios for UI interactions using Selenium and C#.

## Tools and Frameworks Used  
- **Language**: C#
- **Automation Framework**: Selenium WebDriver
- **Testing Framework**: NUnit
- **Browser**: Chrome (for the purpose of this test)
- **IDE**: Visual Studio (version 17.12.0)
- **Build System**: .NET SDK
- **Built on Dotnet version**: 8.0
- **Package Manager**: NuGet

## Prerequisites  
1. Install **Visual Studio** with the following workloads:
   - .NET Desktop Development
2. Install the following tools:   
   - .NET SDK 6.0 or later
4. Install the browser (e.g., Google Chrome > version 131) that will be used for testing.

## Application configuration steps
1. **`AppSettingsScheme.json`**  
   - This is the **schema file** checked into the repository.  
   - It acts as a template for creating environment-specific settings.  
   - Blocks like `test`, `dev`, `uat`, and `preprod` demonstrate how to configure environment variables.

2. **`AppSettings.json`**  
   - This is the **working configuration file** that you create by copying `AppSettingsScheme.json`.  
   - Replace or override values in the `test` block as required for your test runs.

## **Instructions to Set Up AppSettings.json**

1. **Locate the Schema File**  
   The schema file `AppSettingsScheme.json` is located in the `Application Settings` FOLDER.

2. **Copy the Schema File**  
   - Make a copy of `AppSettingsScheme.json` in the same folder.  
   - Rename the file to `AppSettings.json`.

3. **Modify the `test` Block**  
   - Open the newly created `AppSettings.json` file.  
   - Update the values inside the **`test`** block as per your requirements.

   **Example**:
   ```json
   {      
      "Url": "Add your test env URL here.",
      
      "Username": "Provide your username.",
      
      "Password": "Provide your password."
    },
   ```
 
4. **Switch Environments**  
   To change the target environment, update the value of the key **`RunOnEnvironment`** in `AppSettings.json`.  
   This value determines which block (e.g., `test`, `dev`, `uat`, `preprod`) will be used.
   **`*Ensure*`** the keys **`ImplicitWait`** is set to **`30`** and **`PageLoadTimeout`** is set to **`10`** for the purpose of this test suite.

**Example**:
   ```json
   "RunOnEnvironment": "test"
   ```
   In this case, the settings inside the **`test`** block will override the default schema.

5. **Check for gitignore**
This file should be ignored as per the instructions in the .gitignore file, however please recheck.

## **Notes, Strategies and Best Practices**
#### Locator Strategy

In this test, you will see **XPath** being used in a **`parameterized/templated`** way, wrapped in a method.  

To maintain consistency, methods take **WebElements** as input instead of the `By` locator class. This approach provides flexibility to work directly with WebElements.  

Using **`templated XPath`** reduces repetitive coding and enables quicker fixes when necessary.
 
#### Code Example: Click Link by Link Name

Below is a method that clicks on a link by its display name using XPath:

```csharp
public void ClickOnLinkByLinkName(string linkname)
{
    try
    {
        IWebElement element = _driver.FindElement(By.XPath($"//a[contains(.,'{linkname}')]"));
        element.Click();
        Console.WriteLine($"Clicked link : {linkname}");
    }
    catch (Exception)
    {
        throw;
    }
}
```
#### How the Parameter Promotes Code Reuse

The `string linkname` parameter in `ClickOnLinkByLinkName` enhances code reuse by:

1. **Dynamic Behavior**  
   Allows the method to click **any link** based on the input.  
   Example:  
   ```csharp
   ClickOnLinkByLinkName("Home");
   ClickOnLinkByLinkName("Contact Us");

#### Dependency Injection with IObjectContainer

The framework follows **best practices** by implementing **Dependency Injection** using `IObjectContainer`.  

This approach ensures:  
- **Loose coupling**: Components are not tightly dependent on each other.  
- **Scalability**: Easily manage and inject dependencies.  
- **Maintainability**: Simplifies unit testing and future code updates.  

`IObjectContainer` facilitates injecting dependencies into steps and hooks, improving the overall flexibility and cleanliness of the framework design.

#### Extending Framework Capabilities

This framework can further be extended to include:  
- **Logs**: To capture detailed execution information.  
- **Reports**: For better test analysis and reporting.  
- **Screenshots**: To capture evidence for test failures or important steps.

These enhancements improve the framework's **usability**, **debugging**, and **reporting capabilities**.

### Steps to Run Tests

#### 1. **Run Tests from Command Line**
- Open the project in the terminal with **elevated rights**.
- Type the following command and press **Enter**:
  ```bash
  dotnet test

#### Run Tests from Test Explorer in Visual Studio

1. Navigate to **View > Test Explorer**.  
2. Build the project to **discover all tests**.  
3. Right-click on the desired test(s) to see **Run options**.  
