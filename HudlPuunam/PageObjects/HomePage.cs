using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace HudlPuunam.PageObjects
{
    public class HomePage : CommonPageFunction
    {
        public const string Title = "Home - Hudl";
        // No H1 Header on this page.

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsHomePageLoaded()
        {
            IWebElement element = _driver.FindElement(By.XPath("//div[@class='hui-globalusermenu']"));
            Actions actions = new Actions(_driver);
            actions.ClickAndHold(element).Build().Perform();
            var isDisplayed = element.FindElement(By.XPath(".//a[contains(.,'Log Out')]")).Displayed;
            return isDisplayed;
        }

        public void ClickLogoutButton()
        {
            // Builds the locator with container scope 'mainnav__actions'
            IWebElement loginLinkElement = _driver.FindElement(By.XPath("//div[@class='hui-globalusermenu']"));

            // Chains the locator to ensure 'Hudl' link is nested.
            loginLinkElement.Click();
            loginLinkElement.FindElement(By.XPath(".//div//a[contains(.,'Log Out')]")).Click();

            WaitForPageLoad();
        }
    }
}
