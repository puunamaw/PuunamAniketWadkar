using HudlPuunam.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HudlPuunam.PageObjects
{
    public class LoginPage : CommonPageFunction
    {
        public const string Title = "Log In";
        public const string Header = "Log In";

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void PerformLogin()
        {
            var runSettings = ConfigManager.RunSettings();
            var email = runSettings[ConfigManagerConstraints.Email];
            var password = runSettings[ConfigManagerConstraints.Password];

            // Enters the username. 
            _driver.FindElement(By.Id("username")).SendKeys(email.ToString());

            // Clicks the Conitue Button 
            _driver.FindElement(By.XPath("//form//button[text()='Continue']")).Click();
            WaitForPageLoad();

            // enter the password.
            _driver.FindElement(By.Id("password")).SendKeys(password.ToString());

            // Clicks the Conitue Button 
            _driver.FindElement(By.XPath("//form//button[text()='Continue']")).Click();
            WaitForPageLoad();
        }

        public void PerformLogin(string userName, string password)
        {
            // Enters the username. 
            _driver.FindElement(By.Id("username")).SendKeys(userName);

            // Clicks the Conitue Button 
            _driver.FindElement(By.XPath("//form//button[text()='Continue']")).Click();
            WaitForPageLoad();

            // enter the password.
            _driver.FindElement(By.Id("password")).SendKeys(password);

            // Clicks the Conitue Button
            ClickContinue();
            WaitForPageLoad();
        }

        public void PerformLogin_InvalidUserName(string userName)
        {
            // Enters the username. 
            _driver.FindElement(By.Id("username")).SendKeys(userName);

            // Clicks the Conitue Button 
            _driver.FindElement(By.XPath("//form//button[text()='Continue']")).Click();
            WaitForPageLoad();
        }

        public void PerformLogin_EmailEdit()
        {
            var runSettings = ConfigManager.RunSettings();
            var email = runSettings[ConfigManagerConstraints.Email];
            var password = runSettings[ConfigManagerConstraints.Password];

            // Enters the username to be edited further. 
            _driver.FindElement(By.Id("username")).SendKeys("test@test.com");

            // Clicks the Conitue Button 
            ClickContinue();
            WaitForPageLoad();

            // Click the Edit link. 
            ClickOnLinkByLinkName("Edit");

            // Clear and Enter the username. 
            _driver.FindElement(By.Id("username")).Clear();
            _driver.FindElement(By.Id("username")).SendKeys(email.ToString());

            // Clicks the Conitue Button 
            ClickContinue();
            WaitForPageLoad();

            // enter the password.
            _driver.FindElement(By.Id("password")).SendKeys(password.ToString());

            // Clicks the Conitue Button 
            ClickContinue();
            WaitForPageLoad();
        }

        public void PerformLogin_ForgotPassword()
        {
            var runSettings = ConfigManager.RunSettings();
            var email = runSettings[ConfigManagerConstraints.Email];
            var password = runSettings[ConfigManagerConstraints.Password];

            // Enters the username. 
            _driver.FindElement(By.Id("username")).SendKeys(email.ToString());

            // Clicks the Continue Button 
            ClickContinue();
            WaitForPageLoad();

            // Click the forgot password link
            ClickOnLinkByLinkName("Forgot Password");
        }

        public void ResetPassword()
        {
            var runSettings = ConfigManager.RunSettings();
            var email = runSettings[ConfigManagerConstraints.Email];
            AssertH1Header("Reset Password");

            // Get the email address from the input email field.
            IWebElement element = _driver.FindElement(By.XPath("//input[@name='email']"));
            string extactedEmailfromField = element.GetDomAttribute("value");

            Assert.That(extactedEmailfromField.Equals(email));

            // Clicks the Conitue Button 
            ClickContinue();
            WaitForPageLoad();

            //Check the model header
            AssertH1Header("Check Your Email");

            // Assert the Resend Email Button is present
            IWebElement resentEmailButtonElement = _driver.FindElement(By.XPath("//button[contains(.,'Resend Email')]"));
            Assert.That(resentEmailButtonElement.Displayed);
        }

        public bool IsPasswordFieldErrorDisplayed()
        {
            try
            {
                // Observation: Noticed that running the test multiple times, the erorr text changes and is not consistent.
                IWebElement element = _driver.FindElement(By.XPath("//span[@id='error-element-password']"));
                WaitForElementToBeVisible(element, 10);
                return element.Displayed;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsUsernameFieldErrorDisplayed()
        {
            try
            {
                // Observation: Noticed that running the test multiple times, the erorr text changes and is not consistent.
                IWebElement element = _driver.FindElement(By.XPath("//span[@id='error-element-username']"));
                return WaitForElementToBeVisible(element, 10).Displayed;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
