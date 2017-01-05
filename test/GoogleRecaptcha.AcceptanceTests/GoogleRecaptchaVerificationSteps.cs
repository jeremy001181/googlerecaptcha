using System.IO;
using System.Linq;
using System.Reflection;
using GoogleRecaptcha.AcceptanceTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace GoogleRecaptcha.AcceptanceTests
{
    [Binding]
    public class GoogleRecaptchaVerificationSteps
    {
        private RecaptchaTestPage _testPage;
        private ProtectedPage _protectedPage;

        public GoogleRecaptchaVerificationSteps(IWebDriver driver/*RecaptchaTestPage testPage, ProtectedPage protectedPage*/)
        {
            _testPage = new RecaptchaTestPage(driver);
            _protectedPage = new ProtectedPage(driver);
        }

        [Given(@"I am on google recaptcha test page")]
        public void GivenIAmOnGoogleRecaptchaTestPage()
        {
            _testPage.Navigate();
            _testPage.EnsurePageLoaded();
        }

        [When(@"I click on submit")]
        public void WhenIClickOnSubmit()
        {
            _testPage.Submit.Click();
        }

        [Given(@"I receive recaptcha token by clicking tickbox")]
        public void GivenIReceiveRecaptchaTokenByClickingTickbox()
        {
            _testPage.TickRecaptchaCheckBox();
        }


        [Then(@"I should be redirected to protected page with success")]
        public void ThenIShouldBeRedirectedToProtectedPageWithSuccess()
        {
            _protectedPage.EnsurePageLoaded();

            Assert.IsTrue(_protectedPage.IsSuccess);
        }
    }
}
