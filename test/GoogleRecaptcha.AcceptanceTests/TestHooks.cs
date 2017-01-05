using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace GoogleRecaptcha.AcceptanceTests
{
    class TestHooks
    {
        private readonly IWebDriver _driver;

        public TestHooks(IWebDriver driver)
        {
            _driver = driver;
        }

        /*[BeforeTestRun()]
        public static void BeforeFeature()
        {
            FeatureContext.Current["driver"] = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }*/


        [AfterFeature()]
        public static void QuitDriver()
        {
            //var driver = FeatureContext.Current["driver"] as IWebDriver;

            //_driver.Quit();
        }
    }
}
