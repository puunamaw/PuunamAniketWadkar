using HudlPuunam.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace HudlPuunam.BrowserManagement
{
    public class BrowserFactory
    {
        private IWebDriver WebDriver { get; set; }

        /// <summary>
        /// Performs the Opening of the Browser.
        /// </summary>
        public IWebDriver OpenBrowser()
        {
            var runParameters = ConfigManager.RunSettings();
            if (runParameters[ConfigManagerConstraints.RunOnBrowser].ToString().ToLower().Equals("chrome"))
            {
                ChromeOptions chromeOptions = new();
                chromeOptions.AddArgument("--incognito");
                chromeOptions.AddArgument("start-maximized");
                if (runParameters[ConfigManagerConstraints.HeadlessMode].Equals("true"))
                {
                    chromeOptions.AddArguments("--headless");
                }
                WebDriver = new ChromeDriver(chromeOptions);
            }
            else if (runParameters[ConfigManagerConstraints.RunOnBrowser].ToString().ToLower() == "Firefox")
            {
                FirefoxOptions options = new();
                FirefoxProfile profile = new();
                options.Profile = profile;
                options.AcceptInsecureCertificates = true;
                WebDriver = new FirefoxDriver();
            }

            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Convert.ToInt32(runParameters[ConfigManagerConstraints.ImplicitWait]));
            WebDriver.Navigate().GoToUrl(runParameters[ConfigManagerConstraints.Url].ToString());
            return WebDriver;
        }

        /// <summary>
        /// Performs the closing of the browser that is started. Quits the browser after closing.
        /// </summary>
        public void CloseBrowser(IWebDriver driver)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            WebDriver.Close();
            WebDriver.Quit();
        }
    }
}
