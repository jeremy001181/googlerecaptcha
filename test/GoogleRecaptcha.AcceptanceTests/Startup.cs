using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace GoogleRecaptcha.AcceptanceTests
{
    [Binding]
    public class Startup
    {
        private readonly IObjectContainer _objectContainer;

        public Startup(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void RegisterDependencies()
        {
            var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario()]
        public void Cleanup()
        {
            var driver = _objectContainer.Resolve<IWebDriver>();
            driver.Quit();
        }
    }
}
