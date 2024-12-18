using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HudlPuunam.PageObjects
{
    public class CommonPageFunction : IPage
    {
        public IWebDriver _driver;

        public CommonPageFunction(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickContinue()
        {
            IWebElement element = _driver.FindElement(By.XPath("//button[@type='submit' and @name='action']"));
            ClickWithJavaScript(element);
            Console.WriteLine("Clicked the Continue button using JavaScript executor");
        }

        public void AssertTitle(string title)
        {
            var v = _driver.Title;
            Assert.That(_driver.Title.Contains(title), $"Title not matched, you could be on the incorrect page. Epected {title} but found {_driver.Title}");
            Console.WriteLine($"Page title is '{title}'");
        }

        public void WaitForPageLoad()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(20.0)).Until((IWebDriver d) => ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete"));
            Console.WriteLine("Waiting for page load to complete.");
        }

        public void AssertH1Header(string expectedHeaderText)
        {
            try
            {
                IWebElement element = _driver.FindElement(By.XPath($"//h1[contains(.,'{expectedHeaderText}')]"));
                string actualHeader = element.Text;
                Assert.That(actualHeader == expectedHeaderText, $"Expected header '{expectedHeaderText}' \nActual header '{actualHeader}'");
                Console.WriteLine($"Expected header : '{expectedHeaderText}'  matched Actual header : '{actualHeader}'");
            }

            catch (Exception)
            {
                throw;
            }
        }

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

        public IWebElement WaitForElementToBeVisible(IWebElement element, int timeoutInSeconds = 10)
        {
            try
            {
                // Define Explicit Wait
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));

                // Wait until the element is visible
                wait.Until(driver =>
                {
                    try
                    {
                        return element.Displayed;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false; // If the element is stale, consider it not visible
                    }
                });

                return element; // Return the visible element
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Timeout: The provided element was not visible within {timeoutInSeconds} seconds.");
                throw; // Rethrow for further handling
            }
        }

        public void ClickWithJavaScript(IWebElement element)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].click();", element);
        }
    }
}
