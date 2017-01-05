using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace GoogleRecaptcha.AcceptanceTests.PageObjects
{
    public class ProtectedPage : BasePage
    {
        public ProtectedPage(IWebDriver driver) : base(driver)
        {
            Url = "http://localhost:64836/protected";
        }

        [FindsBy(How = How.Id, Using = "message")]
        public IWebElement Message { get; set; }

        public bool IsSuccess
        {
            get
            {
                return Message.Text.Equals("success", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}