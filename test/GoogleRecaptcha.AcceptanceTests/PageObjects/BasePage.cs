using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace GoogleRecaptcha.AcceptanceTests.PageObjects
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        public string Url { get; set; }

        public virtual IWait<IWebDriver> EnsurePageLoaded()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(webDriver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            return wait;
        }

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);
        }
        public virtual BasePage Navigate()
        {
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 0, 5));
            driver.Navigate().GoToUrl(this.Url);
            return this;
        }
    }
}