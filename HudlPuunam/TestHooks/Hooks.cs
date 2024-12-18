using BoDi;
using HudlPuunam.BrowserManagement;
using HudlPuunam.PageObjects;
using OpenQA.Selenium;

namespace HudlPuunam.TestHooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private readonly BrowserFactory _browserFactory;
        private IWebDriver _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hooks"/> class.
        /// </summary>
        /// <param name="objectContainer">Object Container.</param>
        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _browserFactory = new BrowserFactory();
        }

        [BeforeTestRun]
        public static void SetUpBeforeTestRun()
        {
        }

        [AfterTestRun]
        public static void TearDownAfterTestRun()
        {
        }

        [BeforeFeature]

        public static void BeforeFeature(FeatureContext featureContext)
        {
            //feature = extent.CreateTest(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
        }

        [BeforeScenario]
        public void SetUp(ScenarioContext context)
        {
            _driver = _browserFactory.OpenBrowser();
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            _objectContainer.RegisterInstanceAs<CommonPageFunction>(new CommonPageFunction(_driver));
        }

        [AfterScenario]
        public void TearDown(ScenarioContext context)
        {
            _browserFactory.CloseBrowser(_driver);
        }

        [BeforeStep]
        public void BeforeSteps()
        {
        }

        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
        }
    }
}
