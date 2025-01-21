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
            // IWebElement element = _driver.FindElement(By.XPath("//div[@class='hui-globalusermenu']"));
            IWebElement element = _driver.FindElement(By.XPath("//div[contains(@class,'fanWebnav_globalUserItemDisplayName')]"));
            Actions actions = new Actions(_driver);
            actions.ClickAndHold(element).Build().Perform();            
            var isDisplayed = _driver.FindElement(By.XPath("//a/span[contains(.,'Log Out')]")).Displayed;
            return isDisplayed;
        }

        public void ClickLogoutButton()
        {            
            IWebElement loginLinkElement = _driver.FindElement(By.XPath("//div[contains(@class,'fanWebnav_globalUserItemDisplayName')]"));
            Actions actions = new Actions(_driver);
            actions.ClickAndHold(loginLinkElement).Build().Perform();            
            _driver.FindElement(By.XPath("//div//a[contains(.,'Log Out')]")).Click();
            WaitForPageLoad();
        }
    }
}
