using OpenQA.Selenium;

namespace HudlPuunam.PageObjects
{
    public class LandingPage : CommonPageFunction
    {
        public const string Title = "Connected solutions for high-performance video and data analysis";
        public const string PaheHeader = "Change the way you see the game.";

        public LandingPage(IWebDriver driver) : base(driver)
        {
        }

        public void ClickLoginButton()
        {
            // Builds the locator with container scope 'mainnav__actions'
            IWebElement loginLinkElement = _driver.FindElement(By.XPath("//div[@class='mainnav__actions']//a[contains(.,'Log in')]"));
            var ariaExpanded = loginLinkElement.GetDomAttribute("aria-expanded");

            if (ariaExpanded.Equals("false"))
            {
                // Chains the locator to ensure 'Hudl' link is nested.
                loginLinkElement.Click();
                loginLinkElement.FindElement(By.XPath("./following-sibling::div//a[contains(.,'Hudl')]")).Click();
                Console.WriteLine("Clicked on the 'Login' button.");
            }
            WaitForPageLoad();
        }
    }
}
