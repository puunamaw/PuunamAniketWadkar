using HudlPuunam.Configuration;
using HudlPuunam.PageObjects;
using NUnit.Framework;

namespace HudlPuunam.StepDefinitions
{
    [Binding]
    public class LoginTestFeatureStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CommonPageFunction _commonPageFunction;
        private readonly LandingPage _landingPage;
        private readonly LoginPage _loginPage;
        private readonly HomePage _homePage;

        public LoginTestFeatureStepDefinitions(ScenarioContext scenarioContext, CommonPageFunction commonPageFunction, LandingPage landingPage, LoginPage loginPage, HomePage homePage)
        {
            _scenarioContext = scenarioContext;
            _commonPageFunction = commonPageFunction;
            _landingPage = landingPage;
            _loginPage = loginPage;
            _homePage = homePage;
        }

        [Given(@"I am on the landing page")]
        public void GivenIAmOnTheLandingPage()
        {
            _commonPageFunction.WaitForPageLoad();
            _landingPage.AssertTitle(LandingPage.Title);
            _scenarioContext.Add("Landing Page Title", LandingPage.Title);
        }

        [When(@"I click the login button and enter credentials")]
        public void WhenIClickTheLoginButtonAndEnterCredentials()
        {
            // Clicks the login button on the landing page.
            _landingPage.ClickLoginButton();
            _landingPage.WaitForPageLoad();

            // Assets the Login Page Title and Header.
            _loginPage.AssertTitle(LoginPage.Title);
            _loginPage.AssertH1Header(LoginPage.Header);

            // Gets the run settings for username and password.
            var runSettings = ConfigManager.RunSettings();
            var email = runSettings[ConfigManagerConstraints.Email].ToString();
            var password = runSettings[ConfigManagerConstraints.Password].ToString();
            _scenarioContext["username"] = email;
            _scenarioContext["password"] = password;
            _loginPage.PerformLogin(email, password);
        }

        [When(@"I perform login with invalid username ""([^""]*)""  and passwword ""([^""]*)""")]
        public void WhenIPerformLoginWithInvalidUsernameAndPasswword(string invalidUsername, string invalidPassword)
        {
            // Clicks the login button on the landing page.
            _landingPage.ClickLoginButton();
            _landingPage.WaitForPageLoad();

            // Assets the Login Page Title and Header.
            _loginPage.AssertTitle(LoginPage.Title);
            _loginPage.AssertH1Header(LoginPage.Header);

            _loginPage.PerformLogin(invalidUsername, invalidPassword);
        }

        [When(@"I perform login with invalid username ""([^""]*)""")]
        public void WhenIPerformLoginWithInvalidUsername(string invalidUsername)
        {
            // Clicks the login button on the landing page.
            _landingPage.ClickLoginButton();
            _landingPage.WaitForPageLoad();

            // Assets the Login Page Title and Header.
            _loginPage.AssertTitle(LoginPage.Title);
            _loginPage.AssertH1Header(LoginPage.Header);

            _loginPage.PerformLogin_InvalidUserName(invalidUsername);
            Assert.That(_loginPage.IsUsernameFieldErrorDisplayed(), "Expected : Invalid Username \nActual : Username valid.");
        }


        [When(@"I click the login button and enter invalid passwword as ""([^""]*)""")]
        public void WhenIClickTheLoginButtonAndEnterInvalidPasswwordAs(string testAutomationPassword)
        {
            // Clicks the login button on the landing page.
            _landingPage.ClickLoginButton();
            _landingPage.WaitForPageLoad();

            // Assets the Login Page Title and Header.
            _loginPage.AssertTitle(LoginPage.Title);
            _loginPage.AssertH1Header(LoginPage.Header);

            // Gets the run settings for username and password.
            var runSettings = ConfigManager.RunSettings();
            var email = runSettings[ConfigManagerConstraints.Email].ToString();
            var password = testAutomationPassword;

            _loginPage.PerformLogin(email, password);
        }



        [Then(@"I must be logged in to the application")]
        public void ThenIMustBeLoggedInToTheApplication()
        {
            _homePage.AssertTitle(HomePage.Title);
            Assert.That(_homePage.IsHomePageLoaded(), "Home Page is not loaded");
        }

        [Then(@"I must not be logged in to the application or prevented to login")]
        public void ThenIMustNotBeLoggedInToTheApplicationOrPreventedToLogin()
        {
            Assert.That(_loginPage.IsPasswordFieldErrorDisplayed(), "Expecting : Error to be displayed. \nActual : No error dispplayed.");
        }

        [Then(@"I must see error displayed for the email field")]
        public void ThenIMustSeeErrorDisplayedForTheEmailField()
        {
            Assert.That(_loginPage.IsUsernameFieldErrorDisplayed());
        }

        [When(@"I click the logout button then I must be logged out")]
        public void WhenIClickTheLogoutButtonThenIMustBeLoggedOut()
        {
            _homePage.ClickLogoutButton();
            _commonPageFunction.WaitForPageLoad();
            _commonPageFunction.ClickOnLinkByLinkName("Log in");
        }

        [When(@"I click the login button and enter email address to be edited and reenter valid email and proceed")]
        public void WhenIClickTheLoginButtonAndEnterEmailAddressToBeEditedAndReenterValidEmailAndProceed()
        {
            // Clicks the login button on the landing page.
            _landingPage.ClickLoginButton();
            _landingPage.WaitForPageLoad();

            // Assets the Login Page Title and Header.
            _loginPage.AssertTitle(LoginPage.Title);
            _loginPage.AssertH1Header(LoginPage.Header);

            // Gets the run settings for username and password.
            var runSettings = ConfigManager.RunSettings();
            var email = runSettings[ConfigManagerConstraints.Email].ToString();
            var password = runSettings[ConfigManagerConstraints.Password].ToString();

            _loginPage.PerformLogin_EmailEdit();
        }

        [When(@"I am on the login page and click forgot password")]
        public void WhenIAmOnTheLoginPageAndClickForgotPassword()
        {
            // Clicks the login button on the landing page.
            _landingPage.ClickLoginButton();
            _landingPage.WaitForPageLoad();

            // Assets the Login Page Title and Header.
            _loginPage.AssertTitle(LoginPage.Title);
            _loginPage.AssertH1Header(LoginPage.Header);

            // Gets the run settings for username and password.
            var runSettings = ConfigManager.RunSettings();
            var email = runSettings[ConfigManagerConstraints.Email].ToString();
            var password = runSettings[ConfigManagerConstraints.Password].ToString();

            _loginPage.PerformLogin_ForgotPassword();
        }

        [Then(@"I must be redirected to Rest Password Page and be able to request a reset password link to my email")]
        public void ThenIMustBeRedirectedToRestPasswordPageAndBeAbleToRequestAResetPasswordLinkToMyEmail()
        {
            _loginPage.ResetPassword();
        }

    }
}
